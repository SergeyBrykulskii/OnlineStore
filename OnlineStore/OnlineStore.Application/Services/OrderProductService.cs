using AutoMapper;
using OnlineStore.Application.DTOs.OrderProductDTOs;
using OnlineStore.Application.Result;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;
public class OrderProductService: IOrderProductService
{
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    public OrderProductService(IOrderProductRepository orderProductRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        _orderProductRepository = orderProductRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<BaseResult<OrderProductDto>> AddProductToOrderAsync(OrderProductDto orderProductItem)
    {
        var orderProduct = _mapper.Map<OrderProduct>(orderProductItem);
        await _orderProductRepository.CreateAsync(orderProduct);
        return new BaseResult<OrderProductDto> { Data = orderProductItem };
    }
    public async Task<BaseResult<OrderProductDto>> UpdateProductQuantityInOrderAsync(OrderProductDto orderProductItem)
    {
        var orderProductById = await _orderProductRepository.GetByIdAsync(orderProductItem.Id);
        if(orderProductById == null)
        {
            return new BaseResult<OrderProductDto> { ErrorMessage = "Order product not found" };
        }
        var orderProductUpdated = _mapper.Map<OrderProduct>(orderProductItem);
        await _orderProductRepository.UpdateAsync(orderProductUpdated);
        return new BaseResult<OrderProductDto> { Data = orderProductItem };
    }

    public async Task<BaseResult<long>> RemoveProductFromOrderAsync(long Id)
    {
        var orderProduct = await _orderProductRepository.GetByIdAsync(Id);
        if (orderProduct == null)
        {
            return new BaseResult<long> { ErrorMessage = "Order product not found" };
        }
        await _orderProductRepository.DeleteAsync(orderProduct);
        return new BaseResult<long> { Data = Id };
    }

    public async Task<CollectionResult<OrderProductDto>> GetProductsByOrder(long OrderId)
    {
        var order = await _orderRepository.GetByIdAsync(OrderId);
        if (order == null)
        {
            return new CollectionResult<OrderProductDto> { ErrorMessage = "Order not found" };
        }
        var productsInOrderResult = await _orderProductRepository.GetByOrder(order);
        var productsInOrder = _mapper.Map<List<OrderProductDto>>(productsInOrderResult.ToList());
        return new CollectionResult<OrderProductDto> { Data = productsInOrder };
    }
}
