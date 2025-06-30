using BusinessECart.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessECart.RestApi.Configs.DbContext;

public static class Startup
{
    public static IServiceCollection AddProjectDbContext(
        this IServiceCollection services,string ?connectionString)
    {
        services.AddDbContext<EFDataContext>(options =>
            options.UseSqlServer(connectionString));
        return services;
    }
}