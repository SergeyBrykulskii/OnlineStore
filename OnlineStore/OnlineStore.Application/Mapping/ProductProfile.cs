using AutoMapper;
using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductDetailDto>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();    
    }
}
