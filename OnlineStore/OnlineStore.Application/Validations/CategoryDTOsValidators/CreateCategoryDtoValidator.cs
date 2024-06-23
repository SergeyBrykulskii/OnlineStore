using FluentValidation;
using OnlineStore.Application.DTOs.CategoryDTOs;

namespace OnlineStore.Application.Validations.CategoryDTOsValidators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Category name should not be empty");
        RuleFor(c => c.Name)
            .MaximumLength(100).WithMessage("Category name is too long");
    }
}