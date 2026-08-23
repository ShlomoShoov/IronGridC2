using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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