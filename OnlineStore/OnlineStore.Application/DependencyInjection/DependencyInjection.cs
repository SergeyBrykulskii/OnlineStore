using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.DTOs.CategoryDTOs;
using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Application.DTOs.OrderProductDTOs;
using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Application.Services.Implementations;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Application.Validations.CategoryDTOsValidators;
using OnlineStore.Application.Validations.OrderDTOsValidator;
using OnlineStore.Application.Validations.OrderProductsDTOsValidators;
using OnlineStore.Application.Validations.ProductDTOsValidators;
using System.Reflection;

namespace OnlineStore.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IValidator<CategoryDto>, CategoryDtoValidator>();
        services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();
        services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryDtoValidator>();

        services.AddScoped<IValidator<OrderDto>, OrderDtoValidator>();
        services.AddScoped<IValidator<OrderDetailDto>, OrderDetailDtoValidator>();
        services.AddScoped<IValidator<CreateOrderDto>, CreateOrderDtoValidator>();

        services.AddScoped<IValidator<OrderProductDto>, OrderProductDtoValidator>();
        services.AddScoped<IValidator<CreateOrderProductDto>, CreateOrderProductDtoValidator>();
        services.AddScoped<IValidator<UpdateOrderProductDto>, UpdateOrderProductDtoValidator>();

        services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();
        services.AddScoped<IValidator<ProductDetailDto>, ProductDetailDtoValidator>();
        services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();
        services.AddScoped<IValidator<UpdateProductDto>, UpdateProductDtoValidator>();

        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderProductService, OrderProductService>();
    }
}
