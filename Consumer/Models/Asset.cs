using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public Unit? Unit { get; set; } 
        public string AssetSerial { get; set; } = string.Empty;
        public AssetType AssetType { get; set; } = AssetType.GenericAsset;
        public AssetLiveStatus? AssetLiveStatus {get; set;} = null;
    }
}