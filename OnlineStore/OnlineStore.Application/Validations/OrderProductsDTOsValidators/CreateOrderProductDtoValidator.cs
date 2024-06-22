﻿using FluentValidation;
using OnlineStore.Application.DTOs.OrderProductDTOs;

namespace OnlineStore.Application.Validations.OrderProductsDTOsValidators
{
	public class CreateOrderProductDtoValidator : AbstractValidator<CreateOrderProductDto>
	{
		public CreateOrderProductDtoValidator()
		{
			RuleFor(c => c.OrderId)
				.NotEmpty().WithMessage("Order id should not be empty");
			RuleFor(c => c.ProductId)
				.NotEmpty().WithMessage("Product id should not be empty");
			RuleFor(c => c.Quantity)
				.GreaterThanOrEqualTo(0).WithMessage("Quantity should not be negative");
		}
	}
}
