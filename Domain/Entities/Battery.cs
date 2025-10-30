using Domain.Enums;
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
        public string? Model { get; set; }
        public double Capacity { get; set; } 
        public string? Voltage { get; set; }
        public bool IsAproved { get; set; } = false;
        public Chemistry Chemistry { get; set; }
        public string? imgs { get; set; }
        public ICollection<ListingBattery>? ListingBatteries { get; set; }
        public ICollection<BatteryCompatibility>? BatteryCompatibilities { get; set; }
    }
}
