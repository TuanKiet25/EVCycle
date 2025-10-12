using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AutoMapperConfigurations
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            // Add your mappings here
            // CreateMap<Source, Destination>();
            CreateMap<RegisterRequestViewModel, User>()
               .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
               .ForMember(dest => dest.Role, opt => opt.Ignore())
               .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
               .ForMember(dest => dest.VerificationOtp, opt => opt.Ignore())
               .ForMember(dest => dest.OtpExpiryTime, opt => opt.Ignore());
            CreateMap<BatteryRequest, Battery>().ReverseMap();
            CreateMap<Battery,BatteryResponse>().ReverseMap();
            CreateMap<VehicleRequest, Vehicle>().ReverseMap();

        }
    }
}
