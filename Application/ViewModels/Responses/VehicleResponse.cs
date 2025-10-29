using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Responses
{
    public class VehicleResponse
    {
        public Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int Odometer { get; set; }
        public int BatteryHealth { get; set; }
        public string? Color { get; set; }
        public string? VIN { get; set; }
        public string? licensePlate { get; set; }
        public int ListingCount { get; set; }
        public List<string>? BatteryModels { get; set; }
    }
}
