﻿using Application.IServices;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ListingService : IListingService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;
        public ListingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new APIResponse();
        }

        public async Task<APIResponse> CreateListingAsync(ListingRequest newListing, ItemType itemType, Guid userId)
        {
            try
            {
                if (newListing == null)
                {
                    return _response.SetBadRequest(null, "Listing data is null.");
                }
                if(newListing.ListingVehicles == null && newListing.ListingBatteries == null)
                {
                    return _response.SetBadRequest(null, "some Listing data is null");
                }
                if (itemType == ItemType.Battery && newListing.ListingBatteries != null)
                {
                    newListing.ListingVehicles = null;
                }
                else if (itemType == ItemType.Vehicle && newListing.ListingVehicles != null)
                {
                    newListing.ListingBatteries = null;
                }
                else if (itemType == ItemType.FullSet && newListing.ListingBatteries != null && newListing.ListingVehicles != null)
                {

                }
                else
                {
                    return _response.SetBadRequest(null, "some Listing data is null");
                }

                var listing = _mapper.Map<Listing>(newListing);
                
                listing.UserId = userId;
                listing.ItemType = itemType;
                await _unitOfWork.listingRepository.AddAsync(listing);
                if(await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return _response.SetOk(listing);
                }
                else
                {
                    return _response.SetBadRequest(null, "Failed to create listing.");
                }
            }
            catch (Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> DeleteListingAsync(Guid listingId)
        {
            try
            {
                var existingListing = await _unitOfWork.listingRepository.GetAsync(l => l.Id == listingId && l.isDeleted == false);
                if (existingListing == null)
                {
                    return _response.SetNotFound(null, "Listing not found.");
                }
                existingListing.isDeleted = true;
                existingListing.UpdateTime = DateTime.UtcNow;
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return _response.SetOk(null);
                }
                else
                {
                    return _response.SetBadRequest(null, "Failed to delete listing.");
                }
            }
            catch (Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> GetAllListingsAsync(Guid UserId)
        {
            try
            {
                var checkId = await _unitOfWork.userRepository.GetAsync(u => u.Id == UserId);
                if (checkId == null)
                {
                    return _response.SetBadRequest(null, "Can not find User");
                }
                var rawList = await _unitOfWork.listingRepository.GetAllAsync(
                    filter: l => l.UserId == UserId && l.isDeleted == false,
                    include: i => i
                        .Include(l => l.ListingBatteries)
                        .Include(l => l.ListingVehicles)
                    );
                var list = rawList.OrderByDescending(l => l.UpdateTime).ToList();
                List<ListingResponse> listingResponse = new List<ListingResponse>();
                var listListings = _mapper.Map(list, listingResponse);
                if (listListings == null || !listListings.Any())
                {
                    return _response.SetNotFound(null, "No listings found for the user.");
                }
                return _response.SetOk(listListings);
            }
            catch (Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> GetListingByIdAsync(Guid listingId)
        {
            try
            {
                var rawListing = await _unitOfWork.listingRepository.GetAsync(
                    filter: l => l.Id == listingId && l.isDeleted == false,
                    include: i => i
                        .Include(l => l.ListingBatteries)
                        .Include(l => l.ListingVehicles)
                    );
                ListingResponse listingResponse = new ListingResponse();
                var listing = _mapper.Map(rawListing, listingResponse);
                if (listing == null) { 
                    return _response.SetNotFound(null, "Listing not found.");
                }
                return _response.SetOk(listing);
            }
            catch (Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }

        public async Task<APIResponse> UpdateListingAsync(ListingRequest updatedListing, Guid listingId, ItemType itemType)
        {
            try
            {
                var existingListing = await _unitOfWork.listingRepository.GetAsync(
                    l => l.Id == listingId && !l.isDeleted,
                    include: q => q.Include(l => l.ListingBatteries)
                                   .Include(l => l.ListingVehicles)
                );
                if (existingListing == null)
                {
                    return _response.SetNotFound(null, "Listing not found.");
                }

                if (itemType == ItemType.Battery && updatedListing.ListingBatteries != null)
                {
                    existingListing.ItemType = ItemType.Battery;
                    if(existingListing.ListingVehicles != null)
                        existingListing.ListingVehicles.Clear();
                    updatedListing.ListingVehicles = null;
                }
                else if (itemType == ItemType.Vehicle && updatedListing.ListingVehicles != null)
                {
                    existingListing.ItemType = ItemType.Vehicle;
                    if (existingListing.ListingBatteries != null)
                        existingListing.ListingBatteries.Clear();
                    updatedListing.ListingBatteries = null;
                }
                else if (itemType == ItemType.FullSet && updatedListing.ListingBatteries != null && updatedListing.ListingVehicles != null)
                {
                    existingListing.ItemType = ItemType.FullSet;
                }
                else
                {
                    return _response.SetBadRequest(null, "some Listing data is null");
                }

                _mapper.Map(updatedListing, existingListing);

                existingListing.ListingBatteries?.ToList().ForEach(b => b.Id = Guid.NewGuid());
                existingListing.UpdateTime = DateTime.UtcNow;

                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return _response.SetOk(existingListing);
                }
                else
                {
                    return _response.SetBadRequest(null, "Failed to update listing.");
                }
            }
            catch (Exception ex)
            {
                return _response.SetBadRequest(null, ex.Message);
            }
        }
    }
}
