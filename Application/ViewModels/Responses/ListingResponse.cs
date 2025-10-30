using Domain.Entities;
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
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public ItemType ItemType { get; set; }
        public Guid UserId { get; set; }
        public ICollection<ListingBatteryResponse>? ListingBatteries { get; set; }
        public ICollection<ListingVehicleResponse>? ListingVehicles { get; set; }
    }

    public class ListingBatteryResponse
    {
        public Guid Id { get; set; }
        public Guid BatteryId { get; set; }
        public int Health { get; set; }
        public double Price { get; set; }
        public double SuggestedPrice { get; set; }
        public string? Imgs { get; set; }
    }

    public class ListingVehicleResponse
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
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
