using BusinessECart.Persistence.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessECart.RestApi.Configs.Identity;

public static class Startup
{
    public static IServiceCollection ProjectIdentity
        (this IServiceCollection service)
    {
        
        service.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<EFDataContext>()
            .AddDefaultTokenProviders();
        return service;
    }
}