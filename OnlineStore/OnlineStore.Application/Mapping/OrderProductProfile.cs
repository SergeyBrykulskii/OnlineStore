using AutoMapper;
using OnlineStore.Application.DTOs.OrderProductDTOs;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping;
public class OrderProductProfile: Profile
{
    public OrderProductProfile()
    {
        CreateMap<OrderProduct, OrderProductDto>().ReverseMap();
        CreateMap<OrderProduct, CreateOrderProductDto>().ReverseMap();
        CreateMap<OrderProduct, UpdateOrderProductDto>().ReverseMap();
    } 
}
