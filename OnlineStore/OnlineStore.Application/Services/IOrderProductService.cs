using OnlineStore.Application.DTOs.OrderProductDTOs;

namespace OnlineStore.Application.Services;
public interface IOrderProductService
{
    public Task AddProductToOrderAsync(OrderProductDto orderProductItem);
    public Task UpdateProductQuantityInOrderAsync(OrderProductDto orderProductItem);
    public Task RemoveProductFromOrderAsync(long Id);
    public Task<List<OrderProductDto>> GetProductsByOrder(long OrderId);
}
