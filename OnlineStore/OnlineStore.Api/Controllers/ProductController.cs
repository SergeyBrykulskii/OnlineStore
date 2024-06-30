using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;

namespace OnlineStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService service)
    {
        _productService = service;
    }

    [HttpGet]
    public async Task<ActionResult<CollectionResult<ProductDto>>> GetProducts()
    {
        var productServiceResponse = await _productService.GetAllProductsAsync();

        if (productServiceResponse.IsSuccess)
        {
            return Ok(productServiceResponse);
        }

        return BadRequest(productServiceResponse);
    }

    [HttpGet(nameof(id))]
    public async Task<ActionResult<BaseResult<CategoryDto>>> GetProduct(long id)
    {
        var productServiceResponse = await _productService.GetProductByIdAsync(id);

        if (productServiceResponse.IsSuccess)
        {
            return Ok(productServiceResponse);
        }

        return BadRequest(productServiceResponse);
    }

    [HttpGet]
    public async Task<ActionResult<BaseResult<CategoryDto>>> GetProductsByCategory(long categoryId)
    {
        var productServiceResponse = await _productService.GetProductsByCategoryAsync(categoryId);

        if (productServiceResponse.IsSuccess)
        {
            return Ok(productServiceResponse);
        }

        return BadRequest(productServiceResponse);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult<CreateProductDto>>> CreateProduct(
        [FromBody] CreateProductDto productDto)
    {
        var productServiceResponse = await _productService.CreateProductAsync(productDto);

        if (productServiceResponse.IsSuccess)
        {
            return CreatedAtAction(
                nameof(CreateProduct),
                new { name = productDto.Name },
                productServiceResponse.Data);
        }

        return BadRequest(productServiceResponse);
    }

    [HttpPut]
    public async Task<ActionResult<BaseResult<UpdateProductDto>>> UpdateProduct(
        [FromBody] UpdateProductDto productDto)
    {
        var productServiceResponse = await _productService.UpdateProductAsync(productDto);

        if (productServiceResponse.IsSuccess)
        {
            return Ok(productServiceResponse);
        }

        return BadRequest(productServiceResponse);
    }

    [HttpDelete(nameof(id))]
    public async Task<ActionResult<BaseResult<long>>> DeleteProduct(long id)
    {
        var productServiceResponse = await _productService.DeleteProductAsync(id);

        if (productServiceResponse.IsSuccess)
        {
            return Ok(productServiceResponse);
        }

        return BadRequest(productServiceResponse);
    }
}
