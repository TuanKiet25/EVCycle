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
        public int Helth { get; set; }
        public string? Voltage { get; set; }
        public double Kwh { get; set; }
        public string? CompatibleVehicles { get; set; }
        public string? ImageUrl { get; set; }
        public Guid ListingId { get; set; }
        public Listing? Listing { get; set; }
    }
}
