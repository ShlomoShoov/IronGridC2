using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace Consumer.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string UnitName { get; set; } = "Unknown Unit";
        public string Sector { get; set; } = "General";

        public ICollection<Asset> Assets { get; set; } = [];
    }
}