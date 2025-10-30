using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Requests
{
    public class ListingRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public ItemType ItemType { get; set; }
        public Guid UserId { get; set; }
        public ICollection<ListingBatteryRequest>? ListingBatteries { get; set; }
        public ICollection<ListingVehicleRequest>? ListingVehicles { get; set; }
    }
}
