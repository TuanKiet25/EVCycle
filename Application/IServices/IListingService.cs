using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IListingService
    {
        Task<APIResponse> CreateListingAsync(ListingRequest newListing, ItemType itemType, Guid userId);
        Task<APIResponse> GetAllListingsAsync(Guid UserId);
        Task<APIResponse> GetListingByIdAsync(Guid listingId);
        Task<APIResponse> UpdateListingAsync(ListingRequest updatedListing, Guid listingId, ItemType itemType);
        Task<APIResponse> DeleteListingAsync(Guid listingId);
    }
}
