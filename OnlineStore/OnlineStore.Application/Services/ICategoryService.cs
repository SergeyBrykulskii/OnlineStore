using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.Result;

namespace OnlineStore.Application.Services;

public interface ICategoryService
{
    public Task<CollectionResult<CategoryDto>> GetAllCategoriesAsync();
    public Task<BaseResult<CategoryDto>> GetCategoryByIdAsync(long id);
    public Task<BaseResult<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto);
    public Task<BaseResult<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    public Task<BaseResult<long>> DeleteCategoryAsync(long id);
}