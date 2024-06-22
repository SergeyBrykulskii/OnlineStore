using OnlineStore.Application.DTOs.OrderProductDTOs;
using OnlineStore.Application.Result;

namespace OnlineStore.Application.Services;
public interface IOrderProductService
{
    public Task<BaseResult<OrderProductDto>> AddProductToOrderAsync(OrderProductDto orderProductItem);
    public Task<BaseResult<OrderProductDto>> UpdateProductQuantityInOrderAsync(OrderProductDto orderProductItem);
    public Task<BaseResult<long>> RemoveProductFromOrderAsync(long Id);
    public Task<CollectionResult<OrderProductDto>> GetProductsByOrder(long OrderId);
}
