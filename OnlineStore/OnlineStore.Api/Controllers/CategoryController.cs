using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;

namespace OnlineStore.API.Controllers;

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
        var categoryServiceResponse = await _categoryService.GetAllCategoriesAsync();

        if (categoryServiceResponse.IsSuccess)
        {
            return Ok(categoryServiceResponse);
        }

        return BadRequest(categoryServiceResponse);
    }

    [HttpGet(nameof(id))]
    public async Task<ActionResult<BaseResult<CategoryDto>>> GetCategory(long id)
    {
        var categoryServiceResponse = await _categoryService.GetCategoryByIdAsync(id);

        if (categoryServiceResponse.IsSuccess)
        {
            return Ok(categoryServiceResponse);
        }

        return BadRequest(categoryServiceResponse);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult<CreateCategoryDto>>> CreateCategory(
        [FromBody] CreateCategoryDto categoryDto)
    {
        var categoryServiceResponse = await _categoryService.CreateCategoryAsync(categoryDto);

        if (categoryServiceResponse.IsSuccess)
        {
            return CreatedAtAction(nameof(GetCategory),
                new { name = categoryDto.Name },
                categoryServiceResponse.Data);
        }

        return BadRequest(categoryServiceResponse);
    }

    [HttpPut]
    public async Task<ActionResult<BaseResult<UpdateCategoryDto>>> UpdateCategory(
        [FromBody] UpdateCategoryDto categoryDto)
    {
        var categoryServiceResponse = await _categoryService.UpdateCategoryAsync(categoryDto);

        if (categoryServiceResponse.IsSuccess)
        {
            return Ok(categoryServiceResponse);
        }

        return BadRequest(categoryServiceResponse);
    }

    [HttpDelete(nameof(id))]
    public async Task<ActionResult<BaseResult<long>>> DeleteCategory(long id)
    {
        var categoryServiceResponse = await _categoryService.DeleteCategoryAsync(id);

        if (categoryServiceResponse.IsSuccess)
        {
            return Ok(categoryServiceResponse);
        }

        return BadRequest(categoryServiceResponse);
    }
}
