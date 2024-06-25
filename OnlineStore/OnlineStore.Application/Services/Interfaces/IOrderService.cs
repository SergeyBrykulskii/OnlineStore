using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Application.Result;

namespace OnlineStore.Application.Services.Interfaces;

public interface IOrderService
{
    public Task<CollectionResult<OrderDto>> GetAllOrdersAsync();
    public Task<CollectionResult<OrderDto>> GetAllOrdersByUserAsync(Guid userId);
    public Task<BaseResult<OrderDetailDto>> GetOrderByIdAsync(long id);
    public Task<BaseResult<CreateOrderDto>> CreateOrderAsync(CreateOrderDto orderDto);
    public Task<BaseResult<OrderDto>> UpdateOrderAsync(OrderDto orderDto);
    public Task<BaseResult<long>> DeleteOrderAsync(long id);
}