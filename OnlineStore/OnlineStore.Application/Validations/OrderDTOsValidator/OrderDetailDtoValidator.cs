using FluentValidation;
using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Application.Enums;
using OnlineStore.Application.Resources;

namespace OnlineStore.Application.Validations.OrderDTOsValidator;

public class OrderDetailDtoValidator : AbstractValidator<OrderDetailDto>
{
    public OrderDetailDtoValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage(ErrorMessage.NullId).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.CreatedAt)
            .NotEmpty().WithMessage(ErrorMessage.EmptyField).WithErrorCode(ErrorCodes.ValidationError.ToString());
        RuleFor(c => c.UserId)
            .NotNull().WithMessage(ErrorMessage.NullId).WithErrorCode(ErrorCodes.ValidationError.ToString());
    }
}
