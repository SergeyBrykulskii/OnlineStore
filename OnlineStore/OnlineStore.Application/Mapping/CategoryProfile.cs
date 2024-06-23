using AutoMapper;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Product, UpdateCategoryDto>().ReverseMap();
    }
}