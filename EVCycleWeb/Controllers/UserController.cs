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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _userService.GetAllUserAsync();
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var result = await _userService.GetUserAsync();
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserRequest request)
        {
            var result = await _userService.UpdateUserAsync(request);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail([FromBody] VerifyOtpRequest verifyOtpRequest)
        {
            var result = await _userService.VerifyNewEmailAsync(verifyOtpRequest);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost("SendOtpChangeEmail")]
        public async Task<IActionResult> SendOtpChangeEmail([FromBody] UpdateEmailRequest updateEmailRequest)
        {
            var result = await _userService.SentOtpUpdateEmailAsync(updateEmailRequest);
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
