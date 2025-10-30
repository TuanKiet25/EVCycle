using Application.IServices;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
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
        private async Task<int> CountListingByVehicleIdAsync(Guid vehicleId)
        {
            var listing = await _unitOfWork.listingVehicleRepository.GetAllAsync(l => l.VehicleId == vehicleId);
            var count = listing.Count;
            return count;
        }
        public async Task<APIResponse> AdminGetAllVehiclesAsync()
        {
            try
            {
                var vehicles = await _unitOfWork.vehicleRepository.AdminGetAllVehiclesWithCompatibilitiesAsync();
                var vehicleResponses = _mapper.Map<List<VehicleResponse>>(vehicles);
                if (!vehicleResponses.Any())
                {
                    return _response.SetNotFound(null, "No vehicles found");
                }
                foreach (var vehicle in vehicleResponses)
                {
                    vehicle.ListingCount = await CountListingByVehicleIdAsync(vehicle.Id);
                    var originalVehicle = vehicles.First(v => v.Id == vehicle.Id);
                    var compatibilities = originalVehicle.BatteryCompatibilities;
                    if (compatibilities != null)
                    {
                        vehicle.BatteryModels = compatibilities
                        .Select(bc => bc.Battery.Model)
                        .ToList();
                    }
                    else
                    {
                        vehicle.BatteryModels = new List<string>();
                    }
                }
                return _response.SetOk(vehicleResponses);
            }
            catch (Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> ApprovedVehicleAsync(Guid id)
        {
            try
            {
                var vehicle = await _unitOfWork.vehicleRepository.GetAsync(v => v.Id == id && !v.isDeleted);
                if (vehicle == null)
                {
                    return _response.SetNotFound(null, "Vehicle not found");
                }
                vehicle.IsAproved = true;
                await _unitOfWork.SaveChangesAsync();
                return _response.SetOk($"Vehicle {vehicle.Model} approved successfully");
            }
            catch(Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> CreateVehicleAsync(List<VehicleRequest> vehicleRequests)
        {
            try
            {   
                if (!vehicleRequests.Any())
                {
                    return _response.SetBadRequest(null, "Invalid vehicle data");
                }
                var allBatteryIds = vehicleRequests
                    .Where(v => v.CompatibleBatteryIds != null)
                    .SelectMany(v => v.CompatibleBatteryIds)
                    .Distinct()
                    .ToList();
                if (allBatteryIds.Any())
                {
                    var existingBatteryCount = await _unitOfWork.batteryRepository
                        .GetAllAsync(b => allBatteryIds.Contains(b.Id) && !b.isDeleted);

                    if (existingBatteryCount.Count != allBatteryIds.Count)
                    {
                        return _response.SetNotFound(null, "One or more provided Battery IDs are invalid or non-existent.");
                    }
                }

                foreach (var vehical in vehicleRequests)
                {
                    var checkModel = await _unitOfWork.vehicleRepository.GetAsync(v => v.Model.ToLower() == vehical.Model.ToLower() && !v.isDeleted && !v.IsAproved);
                    if (checkModel != null)
                    {
                        return _response.SetBadRequest(null, $"Vehicle model {vehical.Model} already exists");
                    }
                    var vehicleResult = _mapper.Map<Vehicle>(vehical);
                    await _unitOfWork.vehicleRepository.AddAsync(vehicleResult);
                    foreach (var batteryId in vehical.CompatibleBatteryIds ?? new List<Guid>()) 
                    {
                        var batteryCompatibility = new BatteryCompatibility
                        {
                            VehicleId = vehicleResult.Id,
                            BatteryId = batteryId
                        };
                        await _unitOfWork.batteryCompatibilityRepository.AddAsync(batteryCompatibility);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                return _response.SetOk("Vehicles created successfully");
            }
            catch(Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
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
               var vehicles = await _unitOfWork.vehicleRepository.GetAllVehiclesWithCompatibilitiesAsync();
                var vehicleResponses = _mapper.Map<List<VehicleResponse>>(vehicles);
                if (!vehicleResponses.Any())
                {
                    return _response.SetNotFound(null, "No vehicles found");
                }
                foreach (var vehicle in vehicleResponses)
                {
                    vehicle.ListingCount = await CountListingByVehicleIdAsync(vehicle.Id);
                    var originalVehicle = vehicles.First(v => v.Id == vehicle.Id);
                    var compatibilities = originalVehicle.BatteryCompatibilities;
                    if (compatibilities != null)
                    {
                        vehicle.BatteryModels = compatibilities
                        .Select(bc => bc.Battery.Model)
                        .ToList();
                    }
                    else {
                        vehicle.BatteryModels = new List<string>();
                    }
                }
                return _response.SetOk(vehicleResponses);
            }
            catch(Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> GetVehicleByIdAsync(Guid id)
        {
            try { 
                var vehicle = await _unitOfWork.vehicleRepository.GetVehicleWithCompatibilitiesAsync(id);
                if(vehicle == null)
                {
                    return _response.SetNotFound(null, "Vehicle not found");
                }
                var vehicleResponse = _mapper.Map<VehicleResponse>(vehicle);
                vehicleResponse.ListingCount = await CountListingByVehicleIdAsync(vehicle.Id);
                vehicleResponse.BatteryModels = vehicle.BatteryCompatibilities
                    .Select(bc => bc.Battery.Model)
                    .ToList() ?? new List<string>();
                return _response.SetOk(vehicleResponse);
            }
            catch(Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> UpdateVehicleAsync(Guid id, VehicleRequest vehicleRequest)
        {
            try
            {
                var vehicle = await _unitOfWork.vehicleRepository.GetVehicleWithCompatibilitiesAsync(id);
                if (vehicle == null)
                {
                    return _response.SetNotFound(null, "Vehicle not found");
                }
                else if (!vehicle.IsAproved)
                {
                    return _response.SetBadRequest(null, "Approved vehicle cannot be updated");
                }
                    //list pin mới, không có -> list null
                    var NewBatteriesId = vehicleRequest.CompatibleBatteryIds ?? new List<Guid>();
                //Kiểm tra xe batteries này có hợp lệ không
                if (NewBatteriesId.Any())
                {
                    var existingBatteryCount = await _unitOfWork.batteryRepository
                        .GetAllAsync(b => NewBatteriesId.Contains(b.Id) && !b.isDeleted);

                    if (existingBatteryCount.Count != NewBatteriesId.Count)
                    {
                        return _response.SetBadRequest(null, "One or more provided Battery IDs are invalid or non-existent.");
                    }
                }
                _mapper.Map(vehicleRequest, vehicle);
                //list pin hiện tại của xe, không có -> list null
                var currentCompatibilities = vehicle.BatteryCompatibilities ?? new List<BatteryCompatibility>();
                //loại những pin cũ không có trong list request pin mới
                var toRemove = currentCompatibilities
                .Where(bc => !NewBatteriesId.Contains(bc.BatteryId)) 
                .ToList();
                _unitOfWork.batteryCompatibilityRepository.RemoveRange(toRemove);
                //pin cần thêm pin mới vào list pin cũ đã dọn dẹp
                var currentCompatibilityIds = currentCompatibilities.Select(bc => bc.BatteryId).ToList();
                var toAddIds = NewBatteriesId
                    .Except(currentCompatibilityIds) 
                    .ToList();
                var newCompatibilities = toAddIds.Select(batteryId => new BatteryCompatibility
                {
                    VehicleId = vehicle.Id,
                    BatteryId = batteryId
                }).ToList();

                await _unitOfWork.batteryCompatibilityRepository.AddRangeAsync(newCompatibilities);
                await _unitOfWork.SaveChangesAsync();
                return _response.SetOk($"Vehicle {vehicle.Model} updated successfully");
            }
            catch(Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }
    }
}
