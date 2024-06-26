using AutoMapper;
using FluentValidation;
using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Application.Enums;
using OnlineStore.Application.Resources;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateOrderDto> _createOrderDtoValidator;
    private readonly IValidator<OrderDto> _orderDtoValidator;

    public OrderService(
        IOrderRepository orderRepository,
        IMapper mapper,
        IValidator<CreateOrderDto> createOrderDtoValidator,
        IValidator<OrderDto> orderDtoValidator)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _orderDtoValidator = orderDtoValidator;
        _createOrderDtoValidator = createOrderDtoValidator;
    }

    public async Task<CollectionResult<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        var orderDtos = _mapper.Map<List<OrderDto>>(orders);

        return new CollectionResult<OrderDto> { Data = orderDtos };
    }

    public async Task<CollectionResult<OrderDto>> GetAllOrdersByUserAsync(Guid userId)
    {
        var orders = await _orderRepository.GetAllByUserAsync(userId);
        var orderDtos = _mapper.Map<List<OrderDto>>(orders);

        return new CollectionResult<OrderDto> { Data = orderDtos };
    }

    public async Task<BaseResult<OrderDetailDto>> GetOrderByIdAsync(long id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
        {
            return new BaseResult<OrderDetailDto>
            {
                ErrorMessage = ErrorMessage.OrderNotFound,
                ErrorCode = (int)ErrorCodes.OrderNotFound
            };
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

        if (orderById == null)
        {
            return new BaseResult<OrderDto>
            {
                ErrorMessage = ErrorMessage.OrderNotFound,
                ErrorCode = (int)ErrorCodes.OrderNotFound
            };
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
            return new BaseResult<long>
            {
                ErrorMessage = ErrorMessage.OrderNotFound,
                ErrorCode = (int)ErrorCodes.OrderNotFound
            };
        }

        await _orderRepository.DeleteAsync(order);

        return new BaseResult<long> { Data = id };
    }
}
