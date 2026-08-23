using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Consumer.Models;

namespace Consumer.Services
{
    public class KafkaConsumerService : IDisposable
    {
        private IConsumer<Null, string> _consumer;
        private IIRonGridLogger _logger;
        private string _serviceName = "Kafka Consumer Service";

        public KafkaConsumerService(KafkaConsumerSetting settings, IIRonGridLogger logger)
        {
            _logger = logger;
            ConsumerConfig consumerConfigs = new ConsumerConfig()
            {
                BootstrapServers = settings.BootstrapServers,
                GroupId = settings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(consumerConfigs).Build();
            _consumer.Subscribe([settings.PerimeterSensorTopicName, settings.UAVTopicName]);
        }

        public ConsumeResult<Null, string> Consume()
        {
            return _consumer.Consume();
        }

        public void Dispose()
        {
            _consumer.Unsubscribe();
            _consumer.Dispose();
        }
    }
}