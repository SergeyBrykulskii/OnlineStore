using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Application.Result;

namespace OnlineStore.Application.Services.Interfaces;

public interface IProductService
{
    public Task<BaseResult<CreateProductDto>> CreateProductAsync(CreateProductDto productDto);
    public Task<CollectionResult<ProductDto>> GetAllProductsAsync();
    public Task<BaseResult<ProductDetailDto>> GetProductByIdAsync(long id);
    public Task<CollectionResult<ProductDto>> GetProductsByCategoryAsync(long categoryId);
    public Task<BaseResult<UpdateProductDto>> UpdateProductAsync(UpdateProductDto productDto);
    public Task<BaseResult<long>> DeleteProductAsync(long id);
}
