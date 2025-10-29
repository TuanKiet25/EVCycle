using Application.IServices;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly APIResponse _apiResponse;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly IEmailService _emailService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService, IEmailService emailService)           
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _apiResponse = new APIResponse();
            _claimService = claimService;
            _emailService = emailService;
        }
        public async Task<APIResponse> GetAllUserAsync()
        {
            try
            {
                var users = await _unitOfWork.userRepository.GetAllAsync(null);
                if (users == null || !users.Any())
                {
                    return _apiResponse.SetNotFound(message: "No users found.");
                }
                var userResponses = _mapper.Map<List<UserResponse>>(users);
                return _apiResponse.SetOk(userResponses);
            }
            catch(Exception ex)
            {
                return _apiResponse.SetBadRequest(message: ex.Message);
            }
        }

        public async Task<APIResponse> GetUserAsync()
        {
            try
            {
                var claim = _claimService.getUserClaim();
                var user = await _unitOfWork.userRepository.GetAsync(u => u.Id == claim.UserId);
                if (user == null)
                {
                    return _apiResponse.SetNotFound(message: "User not found.");
                }
                var userResponse = _mapper.Map<UserResponse>(user);
                return _apiResponse.SetOk(userResponse);
            }
            catch(Exception ex)
            {
                return _apiResponse.SetBadRequest(message: ex.Message);
            }
        }

        public async Task<APIResponse> SentOtpUpdateEmailAsync(UpdateEmailRequest updateEmailRequest)
        {
            try
            {
                var claim = _claimService.getUserClaim();
                var user = await _unitOfWork.userRepository.GetAsync(u => u.Id == claim.UserId);
                if (user == null)
                {
                    return _apiResponse.SetNotFound(message: "User not found.");
                }
                var otp = new Random().Next(100000, 999999).ToString();
                user.VerificationOtp = otp;
                user.OtpExpiryTime = DateTime.UtcNow.AddMinutes(5);
                await _unitOfWork.SaveChangesAsync();
                var emailBody = $"Your verification code is: {otp}. This code will expire in 5 minutes.";
                await _emailService.SendEmailAsync(updateEmailRequest.NewEmail, "New Email Confirmation", emailBody);
                return _apiResponse.SetOk("OTP sent to new email successfully!");

            }
            catch (Exception ex)
            {
                return _apiResponse.SetBadRequest(message: ex.Message);
            }
        }

        public async Task<APIResponse> UpdateUserAsync(UserRequest userRequest)
        {
            try
            {
                var claim = _claimService.getUserClaim();
                var user = await _unitOfWork.userRepository.GetAsync(u => u.Id == claim.UserId);
                if (user == null)
                {
                    return _apiResponse.SetNotFound(message: "User not found.");
                }
                _mapper.Map(userRequest, user);
                await _unitOfWork.SaveChangesAsync();
                return _apiResponse.SetOk("Updated successfully!");
            }
            catch(Exception ex)
            {
                return _apiResponse.SetBadRequest(message: ex.Message);
            }
        }
        public async Task<APIResponse> VerifyNewEmailAsync(VerifyOtpRequest verifyOtpRequest)
        {
            try
            {
                var claim = _claimService.getUserClaim();
                var user = await _unitOfWork.userRepository.GetAsync(u => u.Id == claim.UserId);
                if (user == null)
                {
                    return _apiResponse.SetNotFound(message: "User not found.");
                }
                if (user.VerificationOtp != verifyOtpRequest.Otp)
                {
                    return _apiResponse.SetBadRequest(message: "Invalid OTP code.");
                }
                user.Email = verifyOtpRequest.Email;
                user.VerificationOtp = null;
                user.OtpExpiryTime = null;
                await _unitOfWork.SaveChangesAsync();
                return _apiResponse.SetOk("Email updated successfully!");
            }
            catch(Exception ex)
            {
                return _apiResponse.SetBadRequest(message: ex.Message);
            }
        }
       
    }
}
