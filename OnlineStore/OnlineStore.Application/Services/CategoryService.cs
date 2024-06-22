using AutoMapper;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.Result;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper; 

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CollectionResult<CategoryDto>> GetAllCategoriesAsync()
    {
		var categories = await _categoryRepository.GetAllAsync();
		var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return new CollectionResult<CategoryDto> { Data = categoryDtos };
	}

    public async Task<BaseResult<CategoryDto>> GetCategoryByIdAsync(long id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if(category == null)
        {
            return new BaseResult<CategoryDto> { ErrorMessage = "Category not found" };
        }
		var categoryDto = _mapper.Map<CategoryDto>(category);
        return new BaseResult<CategoryDto> { Data = categoryDto };
    }

    public async Task<BaseResult<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.CreateAsync(category);
        return new BaseResult<CreateCategoryDto> { Data = categoryDto };
    }

    public async Task<BaseResult<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var categoryById = await _categoryRepository.GetByIdAsync(categoryDto.Id);
        if (categoryById == null)
        {
            return new BaseResult<UpdateCategoryDto> { ErrorMessage = "Category not found" };
        }
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.UpdateAsync(category);
        return new BaseResult<UpdateCategoryDto> { Data = categoryDto };
    }

    public async Task<BaseResult<long>> DeleteCategoryAsync(long id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return new BaseResult<long> { ErrorMessage = "Category not found" };
        }
        await _categoryRepository.DeleteAsync(category);
        return new BaseResult<long> { Data = id };
    }
}