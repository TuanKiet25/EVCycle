using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Listing : BaseEntity
    {
        public string? Title { get; set; }  
        public string? Description { get; set; }  
        public string? Address { get; set; }
        public ItemType ItemType { get; set; }
        public Guid UserId { get; set; }  
        public User? User { get; set; }
        public ICollection<ListingBattery>? ListingBatteries { get; set; }
        public ICollection<ListingVehicle>? ListingVehicles { get; set; }
    }
}
