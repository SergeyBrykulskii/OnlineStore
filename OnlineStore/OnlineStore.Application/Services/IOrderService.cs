using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken);
    Task<List<OrderDto>> GetAllOrdersByUserAsync(User user);
    Task<OrderDetailDto> GetOrderByIdAsync(long id);
    Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto);
    Task<bool> UpdateOrderAsync(OrderDto orderDto);
    Task<bool> DeleteOrderAsync(long id);
}