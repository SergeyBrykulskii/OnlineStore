using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Application.Result;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace OnlineStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<CollectionResult<CategoryDto>>> GetCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();

            if (result == null || result.Data == null)
            {
                return NotFound(new { Message = "Categories not found." });
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResult<CategoryDto>>> GetCategory(long id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);

            if (result.Data == null)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResult<CreateCategoryDto>>> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryDto);

            if (result.Data == null)
            {
                return BadRequest(new { result.ErrorMessage });
            }

            return CreatedAtAction(nameof(GetCategory), new { name = categoryDto.Name }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResult<UpdateCategoryDto>>> UpdateCategory(long id, [FromBody] UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest(new { Message = "ID mismatch." });
            }

            var result = await _categoryService.UpdateCategoryAsync(categoryDto);

            if (result.Data == null)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResult<long>>> DeleteCategory(long id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            if (result.Data == 0)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }
    }
}
