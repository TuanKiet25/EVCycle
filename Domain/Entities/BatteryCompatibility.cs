using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BatteryCompatibility : BaseEntity
    {
        public Guid BatteryId { get; set; }
        public Battery? Battery { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
