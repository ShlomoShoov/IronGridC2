using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Producer.Models
{
    public class Report
    {
        public int AssetId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AssetType AssetType { get; set; }
        public string RawValue { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}