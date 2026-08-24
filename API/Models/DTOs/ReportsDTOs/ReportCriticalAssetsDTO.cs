using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models.DTOs.ReportsDTOs
{
    public class ReportCriticalAssetsDTO
    {
        public int AssetId { get; set; }

        public string AssetSerial { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AssetType AssetType { get; set; } = AssetType.GenericAsset;
        public string UnitName { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProcessedStatus ProcessedStatus { get; set; }
        public bool IsVerified { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}