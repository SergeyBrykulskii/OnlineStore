using AutoMapper;
using IdentityServer.Application.Models.DTOs.UserDTOs;
using IdentityServer.DAL.Models;

namespace IdentityServer.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserRegistrationDto>().ReverseMap();
        CreateMap<User, UserAuthenticationDto>().ReverseMap();
    }
}
