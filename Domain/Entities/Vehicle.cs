using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int Odometer { get; set; }
        public int BatteryHealth { get; set; }
        public string? Color { get; set; }
        public string? VIN { get; set; }
        public string? licensePlate { get; set; }
        public bool IsAproved { get; set; } = false;
        public ICollection<Listing>? Listings { get; set; }
        public ICollection<BatteryCompatibility>? BatteryCompatibilities { get; set; }
    }
}
