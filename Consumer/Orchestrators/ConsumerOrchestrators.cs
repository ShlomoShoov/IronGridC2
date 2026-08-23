using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Consumer.DAL;
using Consumer.Repositories;
using Consumer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Orchestrators
{
    public class ConsumerOrchestrators
    {
        private IServiceProvider _serviceProvider;
        private IIRonGridLogger _logger;
        private string _serviceName = "Consumer Orchestrators";
        public ConsumerOrchestrators(IServiceProvider serviceProvider, IIRonGridLogger logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task InitAsync()
        {
            string? rawSql = GetSeedData();
            if (!string.IsNullOrEmpty(rawSql))
            {
                using var scope = _serviceProvider.CreateScope();
                AssetLiveStatusRepository repository = scope.ServiceProvider.GetRequiredService<AssetLiveStatusRepository>();
                await repository.InitAsync(rawSql);
            }
        }

        public async Task Run()
        {
            using var kafkaScope = _serviceProvider.CreateScope();
            KafkaConsumerService kafka = kafkaScope.ServiceProvider.GetRequiredService<KafkaConsumerService>();
            
            while (true)
            {
                ConsumeResult<Null, string> result = kafka.Consume();
                if (result != null && result.Message.Value != null)
                {
                    using var processingScope = _serviceProvider.CreateScope();
                    ReportsProcessorService processorService = processingScope.ServiceProvider.GetRequiredService<ReportsProcessorService>();
                    AssetLiveStatusRepository repository = processingScope.ServiceProvider.GetRequiredService<AssetLiveStatusRepository>();
                    await repository.AddAssetLiveStatus(processorService.ProcessReport(result));
                    processingScope.Dispose();
                }
            }
        }

        private string? GetSeedData()
        {
            if (!Path.Exists(Path.Combine("./Data", "seed_database.sql")))
            {
                _logger.Error(_serviceName, $"Unable to find the seed file!");
                return null;
            }
            else
            {
                string rawSeed = File.ReadAllText(Path.Combine("./Data", "seed_database.sql"));
                _logger.Debug(_serviceName, $"Load the seed data:\n {rawSeed}");
                return rawSeed;
            }
        }
    }
}