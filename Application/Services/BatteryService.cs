using Application.IServices;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BatteryService : IBatteryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BatteryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<APIResponse> AdminGetAllBatteriesAsync()
        {
            APIResponse response = new APIResponse();
            try
            {
                var batteries = await _unitOfWork.batteryRepository.GetAllAsync(b => !b.isDeleted);
                var batteryResponses = _mapper.Map<List<BatteryResponse>>(batteries);
                if (!batteryResponses.Any())
                {
                    return response.SetNotFound(null, "No batteries found");
                }
                return response.SetOk(batteryResponses);
            }
            catch(Exception ex)
            {
                return response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> BatteryAprovedAsync(Guid id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var battery = await _unitOfWork.batteryRepository.GetByIdAsync(id);
                if(battery == null)
                {
                    return response.SetNotFound(null, "Battery not found");
                }
                battery.IsAproved = true;
                await _unitOfWork.SaveChangesAsync();
                return response.SetOk($"Battery {battery.Model} approved successfully");
            }
            catch(Exception ex)
            {
                return response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> CreateBatteries(List<BatteryRequest> batteryRequests)
        {
           APIResponse response = new APIResponse();
            try
            {
                if(batteryRequests == null || !batteryRequests.Any())
                {
                    return response.SetBadRequest(null, "Battery requests cannot be null or empty");
                }
                foreach (var batteryRequest in batteryRequests)
                {
                    var checkBrand = await _unitOfWork.batteryRepository.GetAsync(b => b.Model.ToLower() == batteryRequest.Model.ToLower() && !b.isDeleted);
                    if (checkBrand != null)
                    {
                        return response.SetBadRequest(null, $"Battery with Model {batteryRequest.Model} already exists");
                    }
                    var battery = _mapper.Map<Battery>(batteryRequest);
                    await _unitOfWork.batteryRepository.AddAsync(battery);
                }
                await _unitOfWork.SaveChangesAsync();
                return response.SetOk("Batteries created successfully");
            }
            catch (Exception ex)
            {
               return response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> DeleteBatteryAsync(Guid id)
        {
           APIResponse response = new APIResponse();
            try
            {
                var battery = await _unitOfWork.batteryRepository.GetByIdAsync(id);
                if(battery == null)
                {
                    return response.SetNotFound(null, "Battery not found");
                }
                battery.isDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return response.SetOk($"Battery {battery.Model} deleted successfully");
            }
            catch (Exception ex)
            {
               return response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> GetAllBatteriesAsync()
        {
            APIResponse response = new APIResponse();
            try
            {
                var batteries = await _unitOfWork.batteryRepository.GetAllAsync(b => !b.isDeleted && b.IsAproved == true);
                var batteryResponses = _mapper.Map<List<BatteryResponse>>(batteries);
                if (!batteryResponses.Any())
                {
                    return response.SetNotFound(null, "No batteries found");
                }
                return response.SetOk(batteryResponses);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> GetBatteryByIdAsync(Guid id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var battery = await _unitOfWork.batteryRepository.GetByIdAsync(id);
                if(battery == null || battery.isDeleted)
                {
                    return response.SetNotFound(null, "Battery not found");
                }
                var batteryResponse = _mapper.Map<BatteryResponse>(battery);
                return response.SetOk(batteryResponse);
            }
            catch (Exception ex)
            {
                return response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> UpdateBatteryAsync(Guid id, BatteryRequest batteryRequest)
        {
            APIResponse response = new APIResponse();
            try
            {
                var battery = await _unitOfWork.batteryRepository.GetAsync(b => b.Id == id && !b.isDeleted);
                if (battery == null)
                {
                    return response.SetNotFound(null, "Battery not found");
                }
                else if (battery.IsAproved == true)
                {
                    return response.SetBadRequest(null, "Approved battery cannot be updated");
                }
                var checkBrand = await _unitOfWork.batteryRepository.GetAsync(b => b.Model.ToLower() == batteryRequest.Model.ToLower() && !b.isDeleted);
                if (checkBrand != null)
                {
                    return response.SetBadRequest(null, "Battery with the same model already exists");
                }
                _mapper.Map(batteryRequest, battery);
                await _unitOfWork.SaveChangesAsync();
                return response.SetOk($"Battery {battery.Model} updated successfully");
            }
            catch (Exception ex)
            {
                return response.SetBadRequest(null, ex.Message);
            }
        }
    }
}
    