using Application.IServices;
using Application.Services;
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
    }
}
