using FluentValidation;
using OnlineStore.Application.DTOs.CategoryDTOs;

namespace OnlineStore.Application.Validations.CategoryDTOsValidators;

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
	public UpdateCategoryDtoValidator()
	{
		RuleFor(c => c.Id)
			.NotEmpty().WithMessage("Category id should not be empty");
		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("Category name should not be empty");
		RuleFor(c => c.Name)
			.MaximumLength(100).WithMessage("Category name is too long");
	}
}