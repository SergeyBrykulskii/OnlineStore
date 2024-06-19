using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.DAL.Repositories.repositories;

namespace OnlineStore.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.InitRepositories();
    }

    public static void InitRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<ICategoryRepository, CategoryRepository>()
            .AddTransient<IOrderRepository, OrderRepository>();
    }
}
