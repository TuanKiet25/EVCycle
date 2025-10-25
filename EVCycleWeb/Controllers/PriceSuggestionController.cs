using Application.Services;
using Application.ViewModels.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EVCycleWeb.Controllers
{
    public class PriceSuggestionController : ControllerBase
    {
        private readonly PriceSuggestionService _service;

        public PriceSuggestionController(PriceSuggestionService service)
        {
            _service = service;
        }

        [HttpPost("Price_Suggestion")]
        public async Task<IActionResult> SuggestPrice([FromBody] VehicleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var suggestion = await _service.SuggestPriceAsync(request);

            return Ok(suggestion); // Trả về JSON từ OpenAI
        }
    }
}

