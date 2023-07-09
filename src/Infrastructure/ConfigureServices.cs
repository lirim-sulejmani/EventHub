using Carmax.Application.Common.Interfaces;
using Carmax.Domain.Entities;
using Carmax.Infrastructure.Identity;
using Carmax.Infrastructure.Persistence;
using Carmax.Infrastructure.Persistence.Interceptors;
using Carmax.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        //{
        //    services.AddDbContext<BaseDbContext>(options =>
        //        options.UseInMemoryDatabase("CarmaxDb"));
        //}
        //else //mssql
        //{
        //    services.AddDbContext<BaseDbContext>(options =>
        //        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        //            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        //}

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BaseDbContext>(x => x.UseSqlServer(connectionString));
        services.AddDbContext<ApplicationDbContext>();




        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<BaseDbContext>(provider => provider.GetRequiredService<BaseDbContext>());



        services.AddScoped<ApplicationDbContextInitialiser>();


        //services
        //    .AddDefaultIdentity<ApplicationUser>()
        //    //.AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>();





        //services.AddIdentityServer()
        //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        //services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<ITemplateService, TemplateService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITenantResolver, TenantResolver>();


        services.AddAuthentication();
            //.AddIdentityServerJwt();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));


        return services;
    }
}
