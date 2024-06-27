using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Services.Implementations;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Application.Validations.ProductDTOsValidators;
using System.Reflection;

namespace OnlineStore.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddFluentValidation();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

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
