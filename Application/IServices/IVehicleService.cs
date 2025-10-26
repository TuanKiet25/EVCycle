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
        Task<APIResponse> AdminGetAllVehiclesAsync();
        Task<APIResponse> GetVehicleByIdAsync(Guid id);
        Task<APIResponse> CreateVehicleAsync(List<VehicleRequest> vehicleRequests);
        Task<APIResponse> UpdateVehicleAsync(Guid id, VehicleRequest vehicleRequest);
        Task<APIResponse> DeleteVehicleAsync(Guid id);
        Task<APIResponse> ApprovedVehicleAsync(Guid id);
    }
}
