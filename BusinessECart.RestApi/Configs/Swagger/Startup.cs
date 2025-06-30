using Microsoft.Extensions.DependencyInjection;

namespace BusinessECart.RestApi.Configs.Swagger;

public static class Startup
{
    private const string ModuleTitle = "Business E Cart";

    public static IServiceCollection AddProjectSwaggerAndJwt(
        this IServiceCollection services)
    {
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = ModuleTitle +" "+ "API", Version = "v1" });

            // JWT support in Swagger UI
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your token."
            });

            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        return services;
    }
}