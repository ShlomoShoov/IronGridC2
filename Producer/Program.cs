using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Producer.Models;
using Producer.Orchestrators;
using Producer.Services;

var configs = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

KafkaProducerSetting kafkaSettings = new KafkaProducerSetting();
configs.GetSection("kafka").Bind(kafkaSettings);

IIRonGridLogger logger = new ConsoleLogger();
IReportLoader loader = new JsonReportLoader(Path.Combine("./Data","field_reports.json"), logger);
KafkaProducerService kafka = new KafkaProducerService(kafkaSettings, logger);

ProducingOrchestrator orchestrator = new ProducingOrchestrator(kafka, loader, logger);

await orchestrator.Run();

