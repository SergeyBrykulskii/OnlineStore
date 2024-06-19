using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Mapping;
using System.Reflection;

namespace OnlineStore.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
    }
}
