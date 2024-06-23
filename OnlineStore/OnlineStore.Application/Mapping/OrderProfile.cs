using AutoMapper;
using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, OrderDetailDto>().ReverseMap();
        CreateMap<Order, CreateOrderDto>().ReverseMap();
    }
}
