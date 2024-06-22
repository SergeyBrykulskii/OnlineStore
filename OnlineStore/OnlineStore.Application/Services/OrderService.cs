using AutoMapper;
using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Application.Result;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

	public OrderService(IOrderRepository orderRepository, IMapper mapper)
	{
		_orderRepository = orderRepository;
		_mapper = mapper;
	}

	public async Task<CollectionResult<OrderDto>> GetAllOrdersAsync()
	{
		var orders = await _orderRepository.GetAllAsync();
		var orderDtos = _mapper.Map<List<OrderDto>>(orders);
		return new CollectionResult<OrderDto> { Data = orderDtos };
	}

	public async Task<CollectionResult<OrderDto>> GetAllOrdersByUserAsync(User user)
	{
		var orders = await _orderRepository.GetAllByUser(user);
		var orderDtos = _mapper.Map<List<OrderDto>>(orders);
		return new CollectionResult<OrderDto> { Data = orderDtos };
	}

	public async Task<BaseResult<OrderDetailDto>> GetOrderByIdAsync(long id)
    {
		var order = await _orderRepository.GetByIdAsync(id);
        if(order == null)
        {
            return new BaseResult<OrderDetailDto> { ErrorMessage = "Order not found" };
        }
		var orderDto = _mapper.Map<OrderDetailDto>(order);
        return new BaseResult<OrderDetailDto> { Data = orderDto };
	}

	public async Task<BaseResult<CreateOrderDto>> CreateOrderAsync(CreateOrderDto orderDto)
    {
		var order = _mapper.Map<Order>(orderDto);
		await _orderRepository.CreateAsync(order);
		return new BaseResult<CreateOrderDto> { Data = orderDto };
	}

	public async Task<BaseResult<OrderDto>> UpdateOrderAsync(OrderDto orderDto)
    {
        var orderById = await _orderRepository.GetByIdAsync(orderDto.Id);
        if(orderById == null)
        {
            return new BaseResult<OrderDto> { ErrorMessage = "Order not found" };
        }
		var order = _mapper.Map<Order>(orderDto);
		await _orderRepository.UpdateAsync(order);
        return new BaseResult<OrderDto> { Data = orderDto };
	}

	public async Task<BaseResult<long>> DeleteOrderAsync(long id)
    {
		var order = await _orderRepository.GetByIdAsync(id);
		if (order == null)
		{
			return new BaseResult<long> { ErrorMessage = "Order not found" };
		}
		await _orderRepository.DeleteAsync(order);
		return new BaseResult<long> { Data = id };
	}
}
