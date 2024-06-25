using AutoMapper;
using OnlineStore.IdentityServer.Models;
using OnlineStore.IdentityServer.Models.DTOs;

namespace OnlineStore.IdentityServer.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserRegistrationDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
