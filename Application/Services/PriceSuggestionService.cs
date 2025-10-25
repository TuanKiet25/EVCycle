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

        public async Task<string> SuggestPriceAsync(VehicleRequest vehicle)
        {
            string prompt = $@"
Bạn là chuyên gia định giá xe điện cũ. 
Chỉ trả lời một chuỗi JSON hợp lệ với khóa 'giá' (đơn vị VND), không thêm giải thích.

Ví dụ:
{{ ""giá"": 850000000 }}

Dữ liệu xe:
- Thương hiệu: {vehicle.Brand ?? "Không rõ"}
- Mẫu xe: {vehicle.Model ?? "Không rõ"}
- Năm sản xuất: {vehicle.StartYear} - {vehicle.EndYear}
- Odometer (km đã chạy): {vehicle.Odometer} km
- Sức khỏe pin: {vehicle.BatteryHealth}%
- Màu sắc: {vehicle.Color ?? "Không rõ"}
- VIN: {vehicle.VIN ?? "Không rõ"}
- Biển số: {vehicle.licensePlate ?? "Không rõ"}";

            var requestBody = new
            {
                model = "meta-llama/llama-3.1-8b-instruct", // 🧠 Model miễn phí & ổn định
                messages = new[] { new { role = "user", content = prompt } },
                max_tokens = 300
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
