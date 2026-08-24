using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models.DTOs.AssetsStatusDTOs
{
    public class AssetStatusDTO
    {
        public int Id { get; set; }
        public int? UnitId { get; set; }
        public string AssetSerial { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AssetType AssetType { get; set; } = AssetType.GenericAsset;
        public AssetStatusLiveDTO? AssetStatusLive { get; set; } 

    }
}