using Microsoft.Extensions.DependencyInjection;

namespace OnlineStore.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
    }
}
