using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Producer.Models;

namespace Producer.Services
{
    public class KafkaProducerService
    {
        private IProducer<Null, string> _producer;
        private KafkaProducerSetting _settings;
        private IIRonGridLogger _logger;
        private string serviceName = "kafka";

        public KafkaProducerService(KafkaProducerSetting settings, IIRonGridLogger logger)
        {
            ProducerConfig producerConfigs = new ProducerConfig
            {
                BootstrapServers = settings.BootstrapServers  
            };
            
            _producer = new ProducerBuilder<Null, string>(producerConfigs).Build();
            _settings = settings;
            _logger = logger;
        }

        public void  Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }

        public async Task ProduceReport(Report report)
        {
            string TopicName = "";
            if (report.AssetType == AssetType.UAV)
            {
                TopicName = _settings.UAVTopicName;
            }
            else if (report.AssetType == AssetType.PerimeterSensor)
            {
                TopicName = _settings.PerimeterSensorTopicName;
            }

            string rawReport = JsonSerializer.Serialize(report);

            Message<Null, string> message = new Message<Null, string>
            {
                Value = rawReport
            };
            DeliveryResult<Null, string> result = await _producer.ProduceAsync(TopicName, message);
            _logger.Debug(serviceName,$"Produce Message To Topic {result.Topic} | offset: {result.Offset} | raw data = {result.Message.Value} ");

        }
    }
}