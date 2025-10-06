using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Responses
{
    public class VehicleResponse
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public int Odometer { get; set; }
        public int BatteryHealth { get; set; }
        public string? VinNumber { get; set; }
        public string? licensePlate { get; set; }
        public string? ImageUrl { get; set; }
    }
}
