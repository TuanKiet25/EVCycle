using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Battery : BaseEntity
    {
        public string? Brand { get; set; }
        public double Capacity { get; set; } 
        public int health { get; set; }
        public string? Voltage { get; set; }
        public string? CompatibleVehicles { get; set; }
        public string? Imgs { get; set; }
        public Guid ListingId { get; set; }
        public Listing? Listing { get; set; }
    }
}
