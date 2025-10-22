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
        public double Price { get; set; }  
        public double SuggestedPrice { get; set; }
        public string? Address { get; set; }
        public ItemType ItemType { get; set; }
        public string? Imgs { get; set; }
        public Guid UserId { get; set; }  
        public Guid? VehicleId { get; set; }
        public Guid? BatteryId { get; set; }
        public Battery? Battery { get; set; }
        public Vehicle? Vehicle { get; set; }
        public User? User { get; set; }
        
    }
}
