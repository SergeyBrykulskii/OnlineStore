using AutoMapper;
using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Application.Result;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

public class ProductService: IProductService
{
	private readonly IProductRepository _productRepository;
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper _mapper;

	public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository, IMapper mapper)
	{
		_productRepository = productRepository;
		_categoryRepository = categoryRepository;
		_mapper = mapper;
	}

	public async Task<BaseResult<CreateProductDto>> CreateProductAsync(CreateProductDto productDto) {
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
			return new BaseResult<ProductDetailDto> { ErrorMessage = "Product not found" };
		}
		var productDetailDto = _mapper.Map<ProductDetailDto>(product);
		return new BaseResult<ProductDetailDto> { Data = productDetailDto };
	}

	public async Task<CollectionResult<ProductDto>> GetProductsByCategoryAsync(long categoryId)
	{
		var category = await _categoryRepository.GetByIdAsync(categoryId);
		if (category == null)
		{
			return new CollectionResult<ProductDto> { ErrorMessage = "Category not found" };
		}
		var products = await _productRepository.GetByCategoryAsync(category);
		var productDtos = _mapper.Map<List<ProductDto>>(products);
		return new CollectionResult<ProductDto> { Data = productDtos };
	}
	public async Task<BaseResult<UpdateProductDto>> UpdateProductAsync(UpdateProductDto productDto)
	{
		var productById = await _productRepository.GetByIdAsync(productDto.Id);
		if (productById == null)
		{
			return new BaseResult<UpdateProductDto> { ErrorMessage = "Product not found" };
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
			return new BaseResult<long> { ErrorMessage = "Product not found" };
		}
		await _productRepository.DeleteAsync(product);
		return new BaseResult<long> { Data = id };
	}

}
