using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<APIResponse> GetAllUserAsync();
        Task<APIResponse> GetUserAsync();
        Task<APIResponse> UpdateUserAsync(UserRequest userRequest);
        Task<APIResponse> SentOtpUpdateEmailAsync(UpdateEmailRequest updateEmailRequest);
        Task<APIResponse> VerifyNewEmailAsync(VerifyOtpRequest verifyOtpRequest);

    }
}
