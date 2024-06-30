using FluentValidation;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.Enums;
using OnlineStore.Application.Resources;

namespace OnlineStore.Application.Validations.CategoryDTOsValidators;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage(ErrorMessage.EmptyField).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.Name)
            .MaximumLength(100).WithMessage(ErrorMessage.LongName).WithErrorCode(ErrorCodes.ValidationError.ToString());
    }
}