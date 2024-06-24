using AutoMapper;
using FluentValidation;
using OnlineStore.Application.DTOs.OrderProductDTOs;
using OnlineStore.Application.Resources;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services.Implementations;

public class OrderProductService : IOrderProductService
{
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateOrderProductDto> _createOrderProductDtoValidator;
    private readonly IValidator<UpdateOrderProductDto> _updateOrderProductDtoValidator;

    public OrderProductService(
        IOrderProductRepository orderProductRepository,
        IOrderRepository orderRepository, IMapper mapper,
        IValidator<UpdateOrderProductDto> updateOrderProductDtoValidator,
        IValidator<CreateOrderProductDto> createOrderProductDtoValidator)
    {
        _orderProductRepository = orderProductRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
        _createOrderProductDtoValidator = createOrderProductDtoValidator;
        _updateOrderProductDtoValidator = updateOrderProductDtoValidator;
    }

    public async Task<BaseResult<CreateOrderProductDto>> AddProductToOrderAsync(CreateOrderProductDto orderProductItem)
    {
        var validationResult = await _createOrderProductDtoValidator.ValidateAsync(orderProductItem);

        if (validationResult.Errors.Count != 0)
        {
            return new BaseResult<CreateOrderProductDto> { ErrorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage };
        }

        var orderProduct = _mapper.Map<OrderProduct>(orderProductItem);
        await _orderProductRepository.CreateAsync(orderProduct);

        return new BaseResult<CreateOrderProductDto> { Data = orderProductItem };
    }

    public async Task<BaseResult<UpdateOrderProductDto>> UpdateProductQuantityInOrderAsync(UpdateOrderProductDto orderProductItem)
    {
        var validationResult = await _updateOrderProductDtoValidator.ValidateAsync(orderProductItem);

        if (validationResult.Errors.Count != 0)
        {
            return new BaseResult<UpdateOrderProductDto> { ErrorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage };
        }

        var orderProductById = await _orderProductRepository.GetByIdAsync(orderProductItem.Id);

        if (orderProductById == null)
        {
            return new BaseResult<UpdateOrderProductDto> { ErrorMessage = ErrorMessage.OrderProductNotFound };
        }

        var orderProductUpdated = _mapper.Map<OrderProduct>(orderProductItem);
        await _orderProductRepository.UpdateAsync(orderProductUpdated);

        return new BaseResult<UpdateOrderProductDto> { Data = orderProductItem };
    }

    public async Task<BaseResult<long>> RemoveProductFromOrderAsync(long Id)
    {
        var orderProduct = await _orderProductRepository.GetByIdAsync(Id);

        if (orderProduct == null)
        {
            return new BaseResult<long> { ErrorMessage = ErrorMessage.OrderProductNotFound };
        }

        await _orderProductRepository.DeleteAsync(orderProduct);

        return new BaseResult<long> { Data = Id };
    }

    public async Task<CollectionResult<OrderProductDto>> GetProductsByOrder(long OrderId)
    {
        var order = await _orderRepository.GetByIdAsync(OrderId);

        if (order == null)
        {
            return new CollectionResult<OrderProductDto> { ErrorMessage = ErrorMessage.OrderNotFound };
        }

        var productsInOrderResult = await _orderProductRepository.GetByOrderAsync(order);
        var productsInOrder = _mapper.Map<List<OrderProductDto>>(productsInOrderResult.ToList());

        return new CollectionResult<OrderProductDto> { Data = productsInOrder };
    }
}
