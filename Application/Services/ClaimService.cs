using Application.IServices;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ClaimResponse getUserClaim()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity!.IsAuthenticated)
            {
                // Nếu người dùng không được xác thực, không thể lấy claims
                throw new UnauthorizedAccessException("User is not authenticated or HttpContext is unavailable.");
            }

            var userIdClaim = user.FindFirst("UserId"); 
            var userRoleClaim = user.FindFirst(ClaimTypes.Role);
            var userNameClaim = user.FindFirst(JwtRegisteredClaimNames.Sub); 

            // 2. Xử lý lỗi Claim bắt buộc (UserId)
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                // Lỗi nghiêm trọng, token không chứa UserId hợp lệ
                throw new InvalidOperationException("Invalid or missing User ID claim in authenticated context.");
            }

            // 3. Xử lý Claim không bắt buộc/Chuyển đổi kiểu dữ liệu
            Role userRole = Role.User; // Thiết lập giá trị mặc định an toàn

            if (userRoleClaim != null && Enum.TryParse(userRoleClaim.Value, true, out Role parsedRole))
            {
                userRole = parsedRole;
            }

            var fullName = userNameClaim?.Value;

            // 4. Trả về DTO an toàn
            var userClaim = new ClaimResponse
            {
                Role = userRole,
                UserId = userId, 
                Username = fullName ?? "Anonymous"
            };

            return userClaim;
        }
        public class ClaimResponse
        {
            public Guid UserId { get; set; }
            public string Username { get; set; }
            public Role Role { get; set; }
        }
    }
}
