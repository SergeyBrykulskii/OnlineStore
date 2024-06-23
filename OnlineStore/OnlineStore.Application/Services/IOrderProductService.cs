using OnlineStore.Application.DTOs.OrderProductDTOs;
using OnlineStore.Application.Result;

namespace OnlineStore.Application.Services;

public interface IOrderProductService
{
    public Task<BaseResult<CreateOrderProductDto>> AddProductToOrderAsync(CreateOrderProductDto orderProductItem);
    public Task<BaseResult<UpdateOrderProductDto>> UpdateProductQuantityInOrderAsync(UpdateOrderProductDto orderProductItem);
    public Task<BaseResult<long>> RemoveProductFromOrderAsync(long Id);
    public Task<CollectionResult<OrderProductDto>> GetProductsByOrder(long OrderId);
}
