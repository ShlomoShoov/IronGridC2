using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Models.DTOs.AssetsDTOs
{
    public class UpdateAssetDTO
    {
        public int? UnitId { get; set; }
        public string AssetSerial { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AssetType AssetType { get; set; } = AssetType.GenericAsset;
    }
}