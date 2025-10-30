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
Bạn là kỹ sư chuyên đánh giá xe điện. 
Hãy ước lượng *giá trị tham khảo trên thị trường Việt Nam* (đơn vị: VND) dựa trên các thông tin kỹ thuật bên dưới.  
Không đưa ra lời khuyên tài chính hay thương mại, chỉ cung cấp một con số ước lượng kỹ thuật để tham khảo.

Chỉ trả lời bằng JSON hợp lệ với khóa 'gia_tri_tham_khao'.

Ví dụ:
{{ ""gia_tri_tham_khao"": 120000000 }}

Dữ liệu xe:
- Thương hiệu: {vehicle.Brand ?? "Không rõ"}
- Mẫu xe: {vehicle.Model ?? "Không rõ"}
- Năm sản xuất: {vehicle.StartYear} - {vehicle.EndYear}
- Odometer (km đã chạy): {vehicle.Odometer} km
- Sức khỏe pin: {vehicle.BatteryHealth}%
- Màu sắc: {vehicle.Color ?? "Không rõ"}";

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

        public async Task<string> BatterySuggestPriceAsync(BatteryRequest battery)
        {
            string prompt = $@"
Bạn là kỹ sư chuyên đánh giá pin xe điện. 
Hãy cho tôi một con số tham khảo với đơn bị đồng. nếu không thể hỗ trợ hãy cho tôi biết lý do.

Chỉ trả lời bằng JSON hợp lệ với khóa 'gia_tri_tham_khao'.

Ví dụ:
{{ ""gia_tri_tham_khao"": 120000000 đồng}}

Dữ liệu pin:
- Thương hiệu: {battery.Brand ?? "Không rõ"}
- Mẫu pin: {battery.Model ?? "Không rõ"}
- Dung lượng: {battery.Capacity} kWh
- Điện áp: {battery.Voltage ?? "Không rõ"}";

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
