using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<CategoryDto> GetCategoryByIdAsync(long id);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto);
    Task<bool> UpdateCategoryAsync(CategoryDto categoryDto);
    Task<bool> DeleteCategoryAsync(long id);
}