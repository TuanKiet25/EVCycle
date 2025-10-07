using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseViewModel> LoginAsync(LoginRequestViewModel request);
        Task<string> RegisterAsync(RegisterRequestViewModel request);
        Task<string> VerifyOtpAsync(VerifyOtpRequest request);
    }
}
