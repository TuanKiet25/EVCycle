using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ListingBattery : BaseEntity
    {
        public Guid ListingId { get; set; }
        public Listing? Listing { get; set; }
        public Guid BatteryId { get; set; }
        public Battery? Battery { get; set; }
        public int Health { get; set; }
        public double Price { get; set; }
        public double SuggestedPrice { get; set; }
        public string? Imgs { get; set; }
    }
}
