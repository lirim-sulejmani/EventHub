using Carmax.Application.Common.Interfaces;
using Carmax.Infrastructure.Persistence;
using Carmax.API.Filters;
using Carmax.API.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
//using NSwag;
//using NSwag.Generation.Processors.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        //services.AddOpenApiDocument(configure =>
        //{
        //    configure.Title = "api";
        //    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
        //    {
        //        Type = OpenApiSecuritySchemeType.ApiKey,
        //        Name = "Authorization",
        //        In = OpenApiSecurityApiKeyLocation.Header,
        //        Description = "Type into the textbox: Bearer {your JWT token}."
        //    });

        //    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        //});

        //    services
        //.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //.AddJwtBearer(o =>
        //{
        //    o.Audience = "8d708afe-2966-40b7-918c-a39551625958";
        //    o.Authority = "https://localhost:44447";
        //});

        return services;
    }
}
