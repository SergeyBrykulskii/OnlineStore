using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Mapping;

namespace OnlineStore.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CategoryProfile), 
                               typeof(ProductProfile),
                               typeof(OrderProfile));
    }
}
