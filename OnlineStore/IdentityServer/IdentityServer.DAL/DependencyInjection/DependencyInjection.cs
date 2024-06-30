using IdentityServer.DAL.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var conectionString = configuration.GetConnectionString("PostrgeSql");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(conectionString);
        });
    }
}
