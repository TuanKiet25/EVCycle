using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Requests
{
    public class VehicleSuggestionRequest
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int Odometer { get; set; }
        public int BatteryHealth { get; set; }
        public string? Color { get; set; }
    }
}
