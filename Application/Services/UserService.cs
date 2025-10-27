using Application.IServices;
using Application.ViewModels.Responses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly APIResponse _apiResponse;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _apiResponse = new APIResponse();
            _claimService = claimService;
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

        public Task<APIResponse> UpdateUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
