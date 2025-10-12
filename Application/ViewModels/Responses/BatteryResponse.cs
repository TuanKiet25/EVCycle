using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Responses
{
    public class BatteryResponse
    {
        public Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public double Capacity { get; set; }
        public int Health { get; set; }
        public string? Voltage { get; set; }
    }
}
