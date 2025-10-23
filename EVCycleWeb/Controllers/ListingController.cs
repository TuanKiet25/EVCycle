using Application.IServices;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllListings()
        {
            var user = HttpContext.User;
            if(user == null)
            {
                return Unauthorized();
            }
            var userId = Guid.Parse(user.FindFirst("UserId")?.Value);
            var result = await _listingService.GetAllListingsAsync(userId);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateListing([FromBody] List<ListingRequest> newListing, [FromQuery] ItemType itemType)
        {
            var user = HttpContext.User;
            if (user == null)
            {
                return Unauthorized();
            }
            var userId = Guid.Parse(user.FindFirst("UserId")?.Value);
            List<ListingRequest> listing = new List<ListingRequest>();
            foreach (var item in newListing)
            {
                var result = await _listingService.CreateListingAsync(item, itemType, userId);
                if(result.IsSuccess == true)
                {
                    listing.Add(item);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return Ok(listing);
        }

        [Authorize]
        [HttpPut("update/{listingId}")]
        public async Task<IActionResult> UpdateListing([FromBody] ListingRequest updatedListing, [FromRoute] Guid listingId, [FromRoute] ItemType itemType)
        {
            var result = await _listingService.UpdateListingAsync(updatedListing, listingId, itemType);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize]
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
