﻿using FluentValidation;
using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Application.Enums;
using OnlineStore.Application.Resources;

namespace OnlineStore.Application.Validations.ProductDTOsValidators;

public class ProductDetailDtoValidator : AbstractValidator<ProductDetailDto>
{
    public ProductDetailDtoValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage(ErrorMessage.NullId).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage(ErrorMessage.EmptyField).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.Name)
            .MaximumLength(100).WithMessage(ErrorMessage.LongName).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.Price)
            .GreaterThanOrEqualTo(0).WithMessage(ErrorMessage.NegPrice).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage(ErrorMessage.LongDesc).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.CategoryId)
            .NotNull().WithMessage(ErrorMessage.NullId).WithErrorCode(ErrorCodes.ValidationError.ToString());
    }
}
