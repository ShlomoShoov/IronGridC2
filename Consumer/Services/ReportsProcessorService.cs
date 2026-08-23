using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Consumer.Models;

namespace Consumer.Services
{
    public class ReportsProcessorService
    {
        private KafkaConsumerSetting _settings;

        public ReportsProcessorService(KafkaConsumerSetting setting)
        {
            _settings = setting;
        }
        public AssetLiveStatus ProcessReport(ConsumeResult<Null, string> ReportEvent)
        {
            Report report = JsonSerializer.Deserialize<Report>(ReportEvent.Message.Value)!;
            
            AssetLiveStatus liveStatus = new AssetLiveStatus
            {
                AssetId = report.AssetId,
                AssetType = report.AssetType,
                Timestamp = report.Timestamp,
            };
            

            if (ReportEvent.Topic == _settings.PerimeterSensorTopicName)
            {
                _CalculatePerimeterSensor(report, liveStatus);
            }

            else if (ReportEvent.Topic == _settings.UAVTopicName)
            {
                _CalculateUAV(report, liveStatus);
            }

            return liveStatus;


        }

        private void _CalculatePerimeterSensor(Report report, AssetLiveStatus liveStatus)
        {
            string rawValue = report.RawValue.Trim();
            liveStatus.RawValue = rawValue;
            if (!int.TryParse(rawValue, out int rawAsInt) || rawAsInt< 0 || rawAsInt > 100)
            {
                liveStatus.ProcessedStatus = ProcessedStatus.Warning;
                liveStatus.IsVerified = false;
            }
            else if (rawAsInt < 20)
            {
                liveStatus.ProcessedStatus = ProcessedStatus.Warning;
                liveStatus.IsVerified = true;
            }
            else
            {
                liveStatus.ProcessedStatus = ProcessedStatus.Stable;
                liveStatus.IsVerified = true;
            }
        }
        private void _CalculateUAV(Report report, AssetLiveStatus liveStatus)
        {
            List<string> StableStatuses = ["Good", "GOOD", "good", "gud"];
            
            List<string> WarningStatuses = ["Bad", "BAD", "bad", "bed"];
            string rawValue = report.RawValue.Trim();
            if (StableStatuses.Contains(rawValue))
            {
                liveStatus.RawValue = "Good";
                liveStatus.ProcessedStatus = ProcessedStatus.Stable;
                liveStatus.IsVerified = true;
            }
            else if (WarningStatuses.Contains(rawValue))
            {
                liveStatus.RawValue = "Bad";
                liveStatus.ProcessedStatus = ProcessedStatus.Warning;
                liveStatus.IsVerified = true;
            }
            else
            {
                liveStatus.ProcessedStatus = ProcessedStatus.Warning;
                liveStatus.IsVerified = false;
                liveStatus.RawValue = rawValue;
            }
            
        }




    }
}