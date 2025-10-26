using Application.IServices;
using Application.Services;
using Application.ViewModels.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EVCycleWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateVehicles([FromBody] List<VehicleRequest> vehicleRequests)
        {
            var result = await _vehicleService.CreateVehicleAsync(vehicleRequests);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] Guid id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("AdminGetAll")]
        public async Task<IActionResult> AdminGetAllVehicles()
        {
            var result = await _vehicleService.AdminGetAllVehiclesAsync();
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllVehicles()
        {
            var result = await _vehicleService.GetAllVehiclesAsync();
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetVehicleById([FromRoute] Guid id)
        {
            var result = await _vehicleService.GetVehicleByIdAsync(id);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateVehicle([FromRoute] Guid id, [FromBody] VehicleRequest vehicleRequest)
        {
            var result = await _vehicleService.UpdateVehicleAsync(id, vehicleRequest);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveVehicle([FromRoute] Guid id)
        {
            var result = await _vehicleService.ApprovedVehicleAsync(id);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
