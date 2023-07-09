using Carmax.Infrastructure.Persistence;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Carmax.Application;
using Carmax.Application.Common.Exceptions;
using System.Security.Claims;
using Carmax.Domain.Entities;
using Carmax.Infrastructure.Auth.Jwt;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.Extensions.Configuration;
using Serilog.Core;
using Serilog.Events;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddWebUIServices();

        Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Error() // Set the minimum log level to Error
             .WriteTo.File(builder.Configuration["Serilog:Path"],
                 rollingInterval: RollingInterval.Day,
                 shared: true
             )
             .CreateLogger();


        //Log.Logger = new LoggerConfiguration()
        //    .WriteTo.File("logs/Mylog.log", rollingInterval: RollingInterval.Hour)
        //    .CreateLogger();


        builder.Services.AddSwaggerGen(swagger =>
        {
            //This is to generate the Default UI of Swagger Documentation
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Carmax API",
                Description = "ASP.NET Core 7.0 Web API"
            });
            // To Enable authorization using Swagger (JWT)
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                    });
        });

        //JWT Authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

        builder.Services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();

            // Initialise and seed database
            using (var scope = app.Services.CreateScope())
            {
                var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
                await initialiser.InitialiseAsync();
                await initialiser.SeedAsync();
            }
        }
        else
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseCors(MyAllowSpecificOrigins);
        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{

        //}
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");
        //redirects to swagger page
        var option = new RewriteOptions();
        option.AddRedirect("^$", "swagger");
        app.UseRewriter(option);
        app.Run();
    }
}