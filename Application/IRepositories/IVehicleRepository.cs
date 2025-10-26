using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {
        Task<List<Vehicle>> GetAllVehiclesWithCompatibilitiesAsync();
        Task<List<Vehicle>> AdminGetAllVehiclesWithCompatibilitiesAsync();
        Task<Vehicle> GetVehicleWithCompatibilitiesAsync(Guid vehicleId);
    }
}
