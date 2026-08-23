using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Models
{
    public class KafkaConsumerSetting
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string PerimeterSensorTopicName { get; set; } = string.Empty;
        public string UAVTopicName { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
    }
}