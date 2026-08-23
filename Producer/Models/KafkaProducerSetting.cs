using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.Models
{
    public class KafkaProducerSetting
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string PerimeterSensorTopicName { get; set; } = string.Empty;
        public string UAVTopicName { get; set; } = string.Empty;

    }
}