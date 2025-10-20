using Application.IServices;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new APIResponse();
        }
        public Task<APIResponse> CreateVehicleAsync(VehicleRequest vehicleRequest, List<Guid> BatteryId)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> DeleteVehicleAsync(Guid id)
        {
            try
            {
                var vehicle = await _unitOfWork.vehicleRepository.GetByIdAsync(id);
                if(vehicle == null)
                {
                    return _response.SetNotFound(null, "Vehicle not found");
                }
                vehicle.isDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return _response.SetOk($"Vehicle {vehicle.Model} deleted successfully");
            }
            catch(Exception ex)
            {
                return _response.SetBadRequest(null,ex.Message);
            }
        }

        public async Task<APIResponse> GetAllVehiclesAsync()
        {
            try
            {
                return _response.SetOk();
            }
            catch(Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
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
