using FluentValidation;
using OnlineStore.Application.DTOs.OrderDTOs;

namespace OnlineStore.Application.Validations.OrderDTOsValidator
{
	public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
	{
		public CreateOrderDtoValidator()
		{
			RuleFor(c => c.UserId)
				.NotEmpty().WithMessage("User id should not be empty");
		}
	}
}
