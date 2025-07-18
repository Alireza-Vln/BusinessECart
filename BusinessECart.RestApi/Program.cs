using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessECart.Infrastructure.Configs.Autofac;
using BusinessECart.RestApi.Configs.Authentication;
using BusinessECart.RestApi.Configs.DbContext;
using BusinessECart.RestApi.Configs.ExceptionHandler;
using BusinessECart.RestApi.Configs.FluentMigratorCore;
using BusinessECart.RestApi.Configs.Identity;
using BusinessECart.RestApi.Configs.Swagger;
using BusinessECart.Entities.Commons;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule(configuration));
});
builder.Services.AddHttpClient();
var service = builder.Services;
builder.Configuration.AddJsonFile(
    "appsettings.json", optional: false, reloadOnChange: true);
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

service.AddControllers();
service
    .ProjectIdentity()
    .AddProjectAuthentication(builder)
    .AddProjectDbContext(connectionString)
    .AddProjectFluentMigratorCore(connectionString)
    .AddEndpointsApiExplorer()
    .AddProjectSwaggerAndJwt()
    .AddMemoryCache();

//AddScoped(builder);


var app = builder.Build();
app.UseProjectExceptionHandler();
using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesAndAdmin(services);
}

// پیکربندی Swagger برای محیط توسعه
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// استفاده از Authentication و Authorization
app.UseAuthentication(); // این باید قبل از UseAuthorization قرار بگیره
app.UseAuthorization();

// نقشه‌برداری کنترلرها
app.MapControllers();

app.Run();

static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles =
    {
        SystemRole.Admin,
        SystemRole.User, SystemRole.Client,SystemRole.Athlete
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}