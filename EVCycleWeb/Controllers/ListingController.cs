using Application.IServices;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVCycleWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;
        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [HttpGet("all/{userId}")]
        public async Task<IActionResult> GetAllListings(Guid userId)
        {
            //var currentUser = HttpContext.User;
            //var userId = currentUser.FindFirst("Id")?.Value;
            var result = await _listingService.GetAllListingsAsync(userId);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetByListingById")]
        public async Task<IActionResult> GetListingById([FromQuery] Guid listingId)
        {
            var result = await _listingService.GetListingByIdAsync(listingId);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateListing([FromBody] ListingRequest newListing, [FromQuery] ItemType itemType)
        {
            var result = await _listingService.CreateListingAsync(newListing, itemType);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update/{listingId}")]
        public async Task<IActionResult> UpdateListing([FromBody] ListingRequest updatedListing, [FromRoute] Guid listingId)
        {
            var result = await _listingService.UpdateListingAsync(updatedListing, listingId);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete/{listingId}")]
        public async Task<IActionResult> DeleteListing([FromRoute] Guid listingId)
        {
            var result = await _listingService.DeleteListingAsync(listingId);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
