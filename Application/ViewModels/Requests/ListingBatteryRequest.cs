using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Requests
{
    public class ListingBatteryRequest
    {
        public Guid BatteryId { get; set; }
        public int Health { get; set; }
        public double Price { get; set; }
        public double SuggestedPrice { get; set; }
        public string? Imgs { get; set; }
    }
}
