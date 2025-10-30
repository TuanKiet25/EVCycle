using Application.ViewModels.Requests;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace Application.Services
{
    public class PriceSuggestionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public PriceSuggestionService(IConfiguration configuration)
        {
            // 🔑 Lấy API key từ appsettings.json hoặc biến môi trường
            _apiKey = configuration["OpenRouter:ApiKey"];

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://openrouter.ai/api/v1/")
            };

            // 🧩 Các header cần thiết cho OpenRouter
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://localhost"); // hoặc domain app của bạn
            _httpClient.DefaultRequestHeaders.Add("X-Title", "EVCycle");
        }

        public async Task<string> VehicleSuggestPriceAsync(VehicleSuggestionRequest vehicle)
        {
            string prompt = $@"
Từ '[Vehicle Data]', gợi ý cho tôi giá bán 2nd hand của chiếc xe. Chỉ trả lời bằng 1 con số (ví dụ: 1500000000VND), không giải thích gì thêm. 

[Vehicle Data]
- Brand: {vehicle.Brand ?? "Unknown"}
- Model: {vehicle.Model ?? "Unknown"}
- Production Year: {vehicle.StartYear} - {vehicle.EndYear}
- Odometer: {vehicle.Odometer} km
- Battery Health: {vehicle.BatteryHealth}%
- Color: {vehicle.Color ?? "Unknown"}";

            var requestBody = new
            {
                model = "minimax/minimax-m2:free", // 🧠 Model miễn phí & ổn định
                messages = new[] { new { role = "user", content = prompt } },
                max_tokens = 2000
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("chat/completions", content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"OpenRouter error {(int)response.StatusCode}: {body}");
            }

            return body; // JSON string trả về từ OpenRouter
        }

        public async Task<string> BatterySuggestPriceAsync(BatteryRequest battery)
        {
            string prompt = $@"
Bạn là kỹ sư chuyên đánh giá pin xe điện. 
gợi ý cho tôi giá bán 2nd hand của pin xe điện này. Chỉ trả lời bằng 1 con số (ví dụ: 1500000000VND), không giải thích gì thêm.

Dữ liệu pin:
- Thương hiệu: {battery.Brand ?? "Không rõ"}
- Mẫu pin: {battery.Model ?? "Không rõ"}
- Dung lượng: {battery.Capacity} kWh
- Điện áp: {battery.Voltage ?? "Không rõ"}";

            var requestBody = new
            {
                model = "minimax/minimax-m2:free", // 🧠 Model miễn phí & ổn định
                messages = new[] { new { role = "user", content = prompt } },
                max_tokens = 2000
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("chat/completions", content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"OpenRouter error {(int)response.StatusCode}: {body}");
            }

            return body; // JSON string trả về từ OpenRouter
        }
    }
    }
