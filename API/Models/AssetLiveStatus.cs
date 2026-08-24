using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AssetLiveStatus
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; } = null!;
        public AssetType AssetType { get; set; }
        public string RawValue { get; set; } = string.Empty;
        public ProcessedStatus ProcessedStatus { get; set; }
        public bool IsVerified { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}