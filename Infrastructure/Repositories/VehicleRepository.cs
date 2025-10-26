using Application.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Vehicle>> GetAllVehiclesWithCompatibilitiesAsync()
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.BatteryCompatibilities)        // Tải bảng trung gian BatteryCompatibilities
                .ThenInclude(bc => bc.Battery)      // Tải chi tiết Entity Battery Standard
                .Where(v => !v.isDeleted)
                .ToListAsync();
            return vehicles;
        }

        public async Task<Vehicle> GetVehicleWithCompatibilitiesAsync(Guid vehicleId)
        {
            var vehicle = await _context.Vehicles
                .Include(v => v.BatteryCompatibilities)       
                .ThenInclude(bc => bc.Battery)     
                .FirstOrDefaultAsync(v => v.Id == vehicleId && !v.isDeleted);
            return vehicle;
        }
    }
}
