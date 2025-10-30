using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ListingVehicle : BaseEntity
    {
        public Guid ListingId { get; set; }
        public Listing? Listing { get; set; } 
        public Guid VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        public int Odometer { get; set; }
        public int BatteryHealth { get; set; }
        public string? Color { get; set; }
        public string? VIN { get; set; }
        public string? licensePlate { get; set; }
        public double Price { get; set; }
        public double SuggestedPrice { get; set; }
        public string? imgs { get; set; }
    }
}
