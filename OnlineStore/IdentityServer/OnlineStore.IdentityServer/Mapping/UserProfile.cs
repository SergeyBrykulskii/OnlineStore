using AutoMapper;
using IdentityServer.Models;
using IdentityServer.Models.DTOs;

namespace IdentityServer.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserRegistrationDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
