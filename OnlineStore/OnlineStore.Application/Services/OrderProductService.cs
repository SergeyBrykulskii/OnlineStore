using AutoMapper;
using OnlineStore.Application.DTOs.OrderProductDTOs;
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
    public async Task AddProductToOrderAsync(OrderProductDto orderProductItem)
    {
        var orderProduct = _mapper.Map<OrderProduct>(orderProductItem);
        try
        {
            await _orderProductRepository.CreateAsync(orderProduct);
        }
        catch (Exception ex)
        {
            throw new Exception($"Something went wrong during adding product to order: {ex.Message}");
        }
    }
    public async Task UpdateProductQuantityInOrderAsync(OrderProductDto orderProductItem)
    {
        var orderProductUpdated = _mapper.Map<OrderProduct>(orderProductItem);
        await _orderProductRepository.UpdateAsync(orderProductUpdated);
    }

    public async Task RemoveProductFromOrderAsync(long Id)
    {
        var orderProduct = await _orderProductRepository.GetByIdAsync(Id);
        if (orderProduct == null)
            throw new Exception($"OrderProduct with id = {Id} was not found");
        await _orderProductRepository.DeleteAsync(orderProduct);
    }

    public async Task<List<OrderProductDto>> GetProductsByOrder(long OrderId)
    {
        var order = await _orderRepository.GetByIdAsync(OrderId);
        if (order == null)
            throw new Exception($"Order with id = {OrderId} was not found");
        var productsInOrderResult = await _orderProductRepository.GetByOrder(order);
        var productsInOrder = _mapper.Map<List<OrderProductDto>>(productsInOrderResult.ToList());
        return productsInOrder;
    }
}
