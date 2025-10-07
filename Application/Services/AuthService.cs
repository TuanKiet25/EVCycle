using Application.IRepositories;
using Application.IServices;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public AuthService(ITokenService tokenService, IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<LoginResponseViewModel> LoginAsync(LoginRequestViewModel request)
        {
            var user = await _unitOfWork.userRepository.GetAsync(u => u.Username== request.Username);

            if (user == null || user.IsActive || !user.IsVerified)
            {
                throw new Exception("Incorrect user name or password!");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("Incorrect user name or password!");
            }

            var tokenString = _tokenService.GenerateToken(user);
            return new LoginResponseViewModel { Token = tokenString };
        }

        public async Task<string> RegisterAsync(RegisterRequestViewModel request)
        {
            var existingUser = await _unitOfWork.userRepository.GetAsync(u => u.Username == request.Username || u.Email == request.Email);
            if (existingUser != null)
            {
                throw new Exception("Username or Email already existed.");
            }

            // 2. Băm mật khẩu
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // 3. Tạo mã OTP (ví dụ 6 chữ số)
            var otp = new Random().Next(100000, 999999).ToString();

            var user = _mapper.Map<User>(request);
            user.PasswordHash = hashedPassword;
            user.Role = Role.User;
            user.IsVerified = false;
            user.VerificationOtp = otp;
            user.OtpExpiryTime = DateTime.UtcNow.AddMinutes(5); 


            // 4. Lưu người dùng vào DB
            await _unitOfWork.userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // 5. Gửi email chứa OTP
            var emailBody = $"Mã xác thực của bạn là: {otp}. Mã này sẽ hết hạn sau 5 phút.";
            await _emailService.SendEmailAsync(user.Email, "Xác nhận đăng ký tài khoản", emailBody);

            return "Đăng ký khởi tạo thành công. Vui lòng kiểm tra email để lấy mã OTP.";
        }

        public async Task<string> VerifyOtpAsync(VerifyOtpRequest request)
        {
            var user = await _unitOfWork.userRepository.GetAsync(u => u.Email == request.Email);
            if (user == null)
            {
                throw new Exception("Account does not exist!");
            }

            // 2. Kiểm tra OTP
            if (user.VerificationOtp != request.Otp)
            {
                throw new Exception("Invalid OTP");
            }

            // 3. Kiểm tra OTP đã hết hạn chưa
            if (user.OtpExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("OTP Expired");
            }

            // 4. Cập nhật trạng thái người dùng
            user.IsVerified = true;
            user.VerificationOtp = null; // Xóa OTP sau khi đã xác thực
            user.OtpExpiryTime = null;
            await _unitOfWork.SaveChangesAsync();

            return "Xác thực tài khoản thành công. Bây giờ bạn có thể đăng nhập.";
        }
    }
}
