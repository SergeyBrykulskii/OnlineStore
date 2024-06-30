using FluentValidation;
using OnlineStore.Application.DTOs.OrderProductDTOs;
using OnlineStore.Application.Enums;
using OnlineStore.Application.Resources;

namespace OnlineStore.Application.Validations.OrderProductsDTOsValidators;

public class OrderProductDtoValidator : AbstractValidator<OrderProductDto>
{
    public OrderProductDtoValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage(ErrorMessage.NullId).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.OrderId)
            .NotNull().WithMessage(ErrorMessage.NullId).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.ProductId)
            .NotNull().WithMessage(ErrorMessage.NullId).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage(ErrorMessage.NegQuantity).WithErrorCode(ErrorCodes.ValidationError.ToString());
    }
}
