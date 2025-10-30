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

        [HttpPost("Vehicle_Price_Suggestion")]
        public async Task<IActionResult> VehicleSuggestPrice([FromBody] VehicleSuggestionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var suggestion = await _service.VehicleSuggestPriceAsync(request);

            return Ok(suggestion); // Trả về JSON từ OpenAI
        }

        [HttpPost("Battery_Price_Suggestion")]
        public async Task<IActionResult> BatterySuggestPrice([FromBody] BatteryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var suggestion = await _service.BatterySuggestPriceAsync(request);

            return Ok(suggestion); // Trả về JSON từ OpenAI
        }
    }
}

