using FluentValidation;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.Enums;
using OnlineStore.Application.Resources;

namespace OnlineStore.Application.Validations.CategoryDTOsValidators;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage(ErrorMessage.NullId).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage(ErrorMessage.EmptyField).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.Name)
            .MaximumLength(100).WithMessage(ErrorMessage.LongName).WithErrorCode(ErrorCodes.ValidationError.ToString());
    }
}
