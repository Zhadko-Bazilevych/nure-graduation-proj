using AutoMapper;
using recipes_backend.Models;
using recipes_backend.Operations.OAuth.AuthByCode;
using recipes_backend.Services.GoogleOAuthServiceModels;
using System.Runtime;

namespace SOAPZ.Common
{
    public class MyAutoMapper : Profile
    {
        public MyAutoMapper()
        {
            CreateMap<UserProfile, User>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Mail, act => act.MapFrom(src => src.Mail));
        }
    }
}