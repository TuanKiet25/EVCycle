using Application.ViewModels.Requests;
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
        }
    }
}
