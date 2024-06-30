using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.DAL.Repositories.Repositories;

namespace OnlineStore.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var conectionString = configuration.GetConnectionString("PostrgeSql");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(conectionString);
        });

        services.InitRepositories();
    }

    public static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IOrderProductRepository, OrderProductRepository>();
    }
}
