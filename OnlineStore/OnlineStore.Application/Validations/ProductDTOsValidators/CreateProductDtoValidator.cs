using FluentValidation;
using OnlineStore.Application.DTOs.ProductDTOs;

namespace OnlineStore.Application.Validations.ProductDTOsValidators
{
	public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
	{
		public CreateProductDtoValidator()
		{
			RuleFor(c => c.Name)
				.NotEmpty().WithMessage("Product name should not be empty");
			RuleFor(c => c.Name)
				.MaximumLength(100).WithMessage("Product name is too long");
			RuleFor(c => c.Price)
				.GreaterThanOrEqualTo(0).WithMessage("Price should not be negative");
			RuleFor(c => c.Description)
				.MaximumLength(500).WithMessage("Description is too long");
			RuleFor(c => c.CategoryId)
				.NotEmpty().WithMessage("Category id shuld not be empty");
		}
	}
}
