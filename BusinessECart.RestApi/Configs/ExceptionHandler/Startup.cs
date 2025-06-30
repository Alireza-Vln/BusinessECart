using System.Net.Mime;
using System.Text.Json;
using BusinessECart.Contracts.BassClass;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BusinessECart.RestApi.Configs.ExceptionHandler;

public static class Startup
{
    public static IApplicationBuilder UseProjectExceptionHandler(
        this IApplicationBuilder app)
    {
        var environment = app.ApplicationServices
            .GetRequiredService<IWebHostEnvironment>();
        var jsonOptions = app.ApplicationServices
            .GetService<IOptions<JsonOptions>>()?.Value.JsonSerializerOptions;

        app.UseExceptionHandler(_ => _.Run(async context =>
        {
            var exception = context.Features
                .Get<IExceptionHandlerPathFeature>()?.Error;

            var isAssignToBusinessException = exception?.GetType()
                .IsAssignableTo(typeof(BusinessException));

            const string errorInProduction =
                "UnknownError";

            var result = new ExceptionErrorDto();
            if (!environment.IsDevelopment())
            {
                if (isAssignToBusinessException is false)
                {
                    result.Error = errorInProduction;
                    result.Description = null;
                }
                else
                {
                    result.Error = exception?.GetType()
                        .Name.Replace("Exception", string.Empty);
                    result.Description = null;
                }
            }
            else
            {
                result.Error = exception?.GetType()
                    .Name.Replace("Exception", string.Empty);
                result.Description = exception?.ToString();
            }

            context.Response.StatusCode = StatusCodes
                .Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response
                .WriteAsync(JsonSerializer.Serialize(result, jsonOptions));
        }));

        if (environment.IsProduction()) app.UseHsts();

        return app;
    }
}