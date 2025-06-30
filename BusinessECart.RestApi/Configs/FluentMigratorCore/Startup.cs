using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessECart.RestApi.Configs.FluentMigratorCore;

public static class Startup
{
    public static IServiceCollection AddProjectFluentMigratorCore(
        this IServiceCollection services, string? connectionString)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(_202505091207_AddAspNetUsersTable).Assembly).For.Migrations());
        return services;
    }
}