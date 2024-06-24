using AutoMapper;
using FluentValidation;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.Resources;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCategoryDto> _createCategoryDtoValidator;
    private readonly IValidator<UpdateCategoryDto> _updateCategoryDtoValidator;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IMapper mapper,
        IValidator<CreateCategoryDto> createCategoryDtoValidator,
        IValidator<UpdateCategoryDto> updateCategoryDtoValidator)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _createCategoryDtoValidator = createCategoryDtoValidator;
        _updateCategoryDtoValidator = updateCategoryDtoValidator;
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

        if (category == null)
        {
            return new BaseResult<CategoryDto> { ErrorMessage = ErrorMessage.CategoryNotFound };
        }
        var categoryDto = _mapper.Map<CategoryDto>(category);

        return new BaseResult<CategoryDto> { Data = categoryDto };
    }

    public async Task<BaseResult<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var validationResult = await _createCategoryDtoValidator.ValidateAsync(categoryDto);

        if (validationResult.Errors.Count != 0)
        {
            return new BaseResult<CreateCategoryDto> { ErrorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage };
        }
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.CreateAsync(category);

        return new BaseResult<CreateCategoryDto> { Data = categoryDto };
    }

    public async Task<BaseResult<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        var validationResult = await _updateCategoryDtoValidator.ValidateAsync(categoryDto);

        if (validationResult.Errors.Count != 0)
        {
            return new BaseResult<UpdateCategoryDto> { ErrorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage };
        }
        var categoryById = await _categoryRepository.GetByIdAsync(categoryDto.Id);

        if (categoryById == null)
        {
            return new BaseResult<UpdateCategoryDto> { ErrorMessage = ErrorMessage.CategoryNotFound };
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
            return new BaseResult<long> { ErrorMessage = ErrorMessage.CategoryNotFound };
        }
        await _categoryRepository.DeleteAsync(category);

        return new BaseResult<long> { Data = id };
    }
}