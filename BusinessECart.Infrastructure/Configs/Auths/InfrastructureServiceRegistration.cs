using BusinessECart.Persistence.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessECart.Infrastructure.Configs.Auths;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<EFDataContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<EFDataContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}