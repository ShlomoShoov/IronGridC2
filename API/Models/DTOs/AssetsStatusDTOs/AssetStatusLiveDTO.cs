using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models.DTOs.AssetsStatusDTOs
{
    public class AssetStatusLiveDTO
    {
        public int AssetId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AssetType AssetType { get; set; }
        public string RawValue { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProcessedStatus ProcessedStatus { get; set; }
        public bool IsVerified { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}