using Application.IServices;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VehicleService : IVehicleService

    {
        public Task<APIResponse> CreateVehicleAsync(VehicleRequest vehicleRequest)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse> DeleteVehicleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse> GetAllVehiclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse> GetVehicleByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse> UpdateVehicleAsync(Guid id, VehicleRequest vehicleRequest)
        {
            throw new NotImplementedException();
        }
    }
}
