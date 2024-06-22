using FluentValidation;
using OnlineStore.Application.DTOs.ProductDTOs;

namespace OnlineStore.Application.Validations.ProductDTOsValidators
{
	public class ProductDtoValidator : AbstractValidator<ProductDto>
	{
		public ProductDtoValidator()
		{
			RuleFor(c => c.Id)
				.NotEmpty().WithMessage("Order id should not be empty");
			RuleFor(c => c.Name)
				.NotEmpty().WithMessage("Product name should not be empty");
			RuleFor(c => c.Name)
				.MaximumLength(100).WithMessage("Product name is too long");
			RuleFor(c => c.Price)
				.GreaterThanOrEqualTo(0).WithMessage("Price should not be negative");
		}
	}
}
