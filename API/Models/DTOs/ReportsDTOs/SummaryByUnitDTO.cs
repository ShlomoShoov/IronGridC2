using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models.DTOs.ReportsDTOs
{
    public class SummaryByUnitDTO
    {
        [JsonPropertyName("unitId")]
        public int UnitId { get; set; }
        [JsonPropertyName("unitName")]
        public string UnitName { get; set; } = string.Empty;
        [JsonPropertyName("sector")]
        public string Sector { get; set; } = string.Empty;
        [JsonPropertyName("totalAssets")]
        public int TotalAssets { get; set; }
        [JsonPropertyName("stableAssets")]
        public int StableAssets { get; set; }
        [JsonPropertyName("warningAssets")]
        public int WarningAssets { get; set; }
        [JsonPropertyName("unverifiedAssets")]
        public int UnverifiedAssets { get; set; }
    }
}