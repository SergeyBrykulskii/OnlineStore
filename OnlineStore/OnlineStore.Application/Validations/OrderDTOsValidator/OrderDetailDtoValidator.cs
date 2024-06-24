using FluentValidation;
using OnlineStore.Application.DTOs.OrderDTOs;

namespace OnlineStore.Application.Validations.OrderDTOsValidator;

public class OrderDetailDtoValidator : AbstractValidator<OrderDetailDto>
{
    public OrderDetailDtoValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Order id should not be empty");
        RuleFor(c => c.CreatedAt)
            .NotEmpty().WithMessage("Creation date not set");
        RuleFor(c => c.UserId)
            .NotEmpty().WithMessage("User id should not be empty");
    }
}
