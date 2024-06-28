using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;

namespace OnlineStore.Api.Controllers
{
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
            var result = await _productService.GetAllProductsAsync();

            if (result == null || result.Data == null)
            {
                return NotFound(new { Message = "Products not found." });
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResult<CategoryDto>>> GetProduct(long id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            if (result.Data == null)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult<BaseResult<CategoryDto>>> GetProductsByCategory(long categoryId)
        {
            var result = await _productService.GetProductsByCategoryAsync(categoryId);

            if (result.Data == null)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResult<CreateProductDto>>> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var result = await _productService.CreateProductAsync(productDto);

            if (result.Data == null)
            {
                return BadRequest(new { result.ErrorMessage });
            }

            return CreatedAtAction(nameof(CreateProduct), new { name = productDto.Name }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResult<UpdateProductDto>>> UpdateProduct(long id, [FromBody] UpdateProductDto productDto, CancellationToken cancellationToken)
        {
            if (id != productDto.Id)
            {
                return BadRequest(new { Message = "ID mismatch." });
            }

            var result = await _productService.UpdateProductAsync(productDto);

            if (result.Data == null)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResult<long>>> DeleteProduct(long id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (result.Data == 0)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }
    }
}
