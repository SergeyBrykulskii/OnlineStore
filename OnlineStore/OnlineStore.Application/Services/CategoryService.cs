using AutoMapper;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper; 

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(long id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.CreateAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> UpdateCategoryAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        var updated = await _categoryRepository.UpdateAsync(category);
        if (updated)
        {
            await _categoryRepository.SaveChangesAsync();
        }
        return updated;
    }

    public async Task<bool> DeleteCategoryAsync(long id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return false;
        }

        var deleted = await _categoryRepository.DeleteAsync(category);
        if (deleted)
        {
            await _categoryRepository.SaveChangesAsync();
        }
        return deleted;
    }
}