using FluentValidation;
using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Application.Enums;
using OnlineStore.Application.Resources;

namespace OnlineStore.Application.Validations.OrderDTOsValidator;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull().WithMessage(ErrorMessage.NullId).WithErrorCode(ErrorCodes.ValidationError.ToString());
    }
}
