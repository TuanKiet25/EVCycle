using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Responses
{
    public class ListingResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double SuggestedPrice { get; set; }
        public string? Address { get; set; }
        public ItemType ItemType { get; set; }
        public string? Imgs { get; set; }
        public Guid BatteryId { get; set; }
        public Guid VehicleId { get; set; }
    }
}
