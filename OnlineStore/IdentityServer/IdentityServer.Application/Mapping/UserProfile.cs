using AutoMapper;
using IdentityServer.Application.Models.DTOs;
using IdentityServer.DAL.Models;

namespace IdentityServer.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserRegistrationDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}
