using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API.Models.DTOs.AssetsDTOs
{
    public class CreateUnitDto
    {
        [StringLength(255)]
        
        public string UnitName { get; set; } = "Unknown Unit";
        [StringLength(255)]
        public string Sector { get; set; } = "General";
    }
}