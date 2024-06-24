using AutoMapper;
using FluentValidation;
using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Application.Resources;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateProductDto> _createProductDtoValidator;
    private readonly IValidator<UpdateProductDto> _updateProductDtoValidator;

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper,
        IValidator<UpdateProductDto> updateProductDtoValidator,
        IValidator<CreateProductDto> createProductDtoValidator)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _createProductDtoValidator = createProductDtoValidator;
        _updateProductDtoValidator = updateProductDtoValidator;
    }

    public async Task<BaseResult<CreateProductDto>> CreateProductAsync(CreateProductDto productDto)
    {
        var validationResult = await _createProductDtoValidator.ValidateAsync(productDto);

        if (validationResult.Errors.Count != 0)
        {
            return new BaseResult<CreateProductDto> { ErrorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage };
        }
        var product = _mapper.Map<Product>(productDto);
        await _productRepository.CreateAsync(product);

        return new BaseResult<CreateProductDto> { Data = productDto };
    }

    public async Task<CollectionResult<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var productDtos = _mapper.Map<List<ProductDto>>(products);

        return new CollectionResult<ProductDto> { Data = productDtos };
    }

    public async Task<BaseResult<ProductDetailDto>> GetProductByIdAsync(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return new BaseResult<ProductDetailDto> { ErrorMessage = ErrorMessage.ProductNotFound };
        }
        var productDetailDto = _mapper.Map<ProductDetailDto>(product);

        return new BaseResult<ProductDetailDto> { Data = productDetailDto };
    }

    public async Task<CollectionResult<ProductDto>> GetProductsByCategoryAsync(long categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category == null)
        {
            return new CollectionResult<ProductDto> { ErrorMessage = ErrorMessage.CategoryNotFound };
        }
        var products = await _productRepository.GetByCategoryAsync(category);
        var productDtos = _mapper.Map<List<ProductDto>>(products);

        return new CollectionResult<ProductDto> { Data = productDtos };
    }
    public async Task<BaseResult<UpdateProductDto>> UpdateProductAsync(UpdateProductDto productDto)
    {
        var validationResult = await _updateProductDtoValidator.ValidateAsync(productDto);

        if (validationResult.Errors.Count != 0)
        {
            return new BaseResult<UpdateProductDto> { ErrorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage };
        }
        var productById = await _productRepository.GetByIdAsync(productDto.Id);

        if (productById == null)
        {
            return new BaseResult<UpdateProductDto> { ErrorMessage = ErrorMessage.ProductNotFound };
        }
        var product = _mapper.Map<Product>(productDto);
        await _productRepository.UpdateAsync(product);

        return new BaseResult<UpdateProductDto> { Data = productDto };

    }
    public async Task<BaseResult<long>> DeleteProductAsync(long id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return new BaseResult<long> { ErrorMessage = ErrorMessage.ProductNotFound };
        }
        await _productRepository.DeleteAsync(product);

        return new BaseResult<long> { Data = id };
    }
}
