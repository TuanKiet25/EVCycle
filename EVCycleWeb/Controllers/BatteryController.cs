using Application.IServices;
using Application.ViewModels.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EVCycleWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatteryController : ControllerBase
    {
        private readonly IBatteryService _batteryService;
        public BatteryController(IBatteryService batteryService)
        {
            _batteryService = batteryService;
        }
        
        [HttpGet("admin/all")]
        public async Task<IActionResult> AdminGetAllBatteries()
        {
            var result = await _batteryService.AdminGetAllBatteriesAsync();
            if(result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveBattery(Guid id)
        {
            var result = await _batteryService.BatteryAprovedAsync(id);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBatteries([FromBody] List<BatteryRequest> batteryRequests)
        {
            var result = await _batteryService.CreateBatteries(batteryRequests);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBattery(Guid id)
        {
            var result = await _batteryService.DeleteBatteryAsync(id);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBatteries()
        {
            var result = await _batteryService.GetAllBatteriesAsync();
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBatteryById(Guid id)
        {
            var result = await _batteryService.GetBatteryByIdAsync(id);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBattery(Guid id, [FromBody] BatteryRequest batteryRequest)
        {
            var result = await _batteryService.UpdateBatteryAsync(id, batteryRequest);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
