using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Requests
{
    public class VehicleRequest
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string? imgs { get; set; }
        public List<Guid>? CompatibleBatteryIds { get; set; } = new List<Guid>();
    }
}
