using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IBatteryService
    {
        Task<APIResponse> GetAllBatteriesAsync();
        Task<APIResponse> GetBatteryByIdAsync(Guid id);
        Task<APIResponse> CreateBatteries(List<BatteryRequest> batteryRequests);
        Task<APIResponse> UpdateBatteryAsync(Guid id, BatteryRequest batteryRequest);
        Task<APIResponse> DeleteBatteryAsync(Guid id);
        Task<APIResponse> AdminGetAllBatteriesAsync();
        Task<APIResponse> BatteryAprovedAsync(Guid id);

    }
}
