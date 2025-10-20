using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IVehicleService
    {
        Task<APIResponse> GetAllVehiclesAsync();
        Task<APIResponse> GetVehicleByIdAsync(Guid id);
        Task<APIResponse> CreateVehicleAsync(VehicleRequest vehicleRequest, List<Guid> BatteryId);
        Task<APIResponse> UpdateVehicleAsync(Guid id, VehicleRequest vehicleRequest);
        Task<APIResponse> DeleteVehicleAsync(Guid id);
    }
}
