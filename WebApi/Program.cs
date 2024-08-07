/*
 * Author: Apache X692 Attack Helicopter
 * Created: 24/07/2024
 */
using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using WebApi.Authentication;
using WebApi.Contexts;
using WebApi.Email;
using WebApi.Services;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load from ENV
        builder.Configuration.AddEnvironmentVariables();

        // Azure Key Vault Management
        var credentialsContainer = new ClientSecretCredential(
            builder.Configuration["AZURE_TENANT_ID"],
            builder.Configuration["AZURE_CLIENT_ID"],
            builder.Configuration["AZURE_CLIENT_SECRET"]
        );
        var secretsClient = new SecretClient(
            new Uri(builder.Configuration["AZURE_KEYVAULT_URI"]!),
            credentialsContainer
        );

        builder.Services.AddSingleton(secretsClient);

        // Database
        var connectionString = secretsClient.GetSecret(
            builder.Environment.IsProduction()
            ? "ProductionDbConnectionString"
            : "DevelopemntDbConnectionString"
        );

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(
                connectionString.Value.Value,
                ServerVersion.AutoDetect(connectionString.Value.Value)
            );
        });

        // DIC
        ConfigureDependencyInjectionContainer(builder.Services);

        // Swagger
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1.0.0", new OpenApiInfo
            {
                Version = "v1.0.0",
                Title = "Employee Grievance Redressal System - Presidio",
                Description = "Capstone Project Back-end",
                Contact = new OpenApiContact
                {
                    Name = "Sakthi Santhosh",
                    Url = new Uri("https://sakthisanthosh.in")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/license/MIT")
                }
            });

            var basicAuthSecurityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                In = ParameterLocation.Header,
                Description = "Basic Authentication"
            };

            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                In = ParameterLocation.Header,
                Description = "JWT-based Authentication",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Basic", basicAuthSecurityScheme);
            options.AddSecurityDefinition("Bearer", jwtSecurityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Basic"
                        }
                    },
                    Array.Empty<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // Authentication and Authorization
        var jwtSecret = secretsClient.GetSecret("SecretKey");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT_ISSUER"],
                ValidAudience = builder.Configuration["JWT_AUDIENCE"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    jwtSecret.Value.Value
                ))
            };
        }).AddScheme<AuthenticationSchemeOptions, HttpBasicAuthenticationHandler>(
            "Basic", options => { });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("User", policy => policy.RequireRole("User"));
        });

        // CORS
        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("AllowAll", builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
        });

        // Middleware Build
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(context =>
            {
                context.SwaggerEndpoint("v1.0.0/swagger.json",
                    "Employee Grievance Redressal System - Presidio");
            });
        }

        // CORS
        app.UseCors("AllowAll");

        app.UseAuthorization();
        //app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }

    public static void ConfigureDependencyInjectionContainer(
        IServiceCollection serviceBuilder)
    {
        // Services
        serviceBuilder.AddSingleton<ICryptographyService, CryptographyService>();
        serviceBuilder.AddSingleton<IJwtService, JwtService>();

        serviceBuilder.AddSingleton<IAzureBlobService, AzureBlobService>();
        serviceBuilder.AddSingleton<IEmailService, EmailService>();
        serviceBuilder.AddSingleton<IEmailTemplateLoader, EmailTemplateLoader>();

        serviceBuilder.AddScoped<IEmployeeService, EmployeeService>();
        serviceBuilder.AddScoped<IDesignationService, DesignationService>();
        serviceBuilder.AddScoped<IDepartmentService, DepartmentService>();
        serviceBuilder.AddScoped<IFileAttachmentService, FileAttachmentService>();
        serviceBuilder.AddScoped<IGrievanceService, GrievanceService>();
        serviceBuilder.AddScoped<IGrievanceCategoryService, GrievanceCategoryService>();
        serviceBuilder.AddScoped<IGrievancePriorityService, GrievancePriorityService>();
        serviceBuilder.AddScoped<IGrievanceResponseService, GrievanceResponseService>();
        serviceBuilder.AddScoped<IGrievanceStatusService, GrievanceStatusService>();
        serviceBuilder.AddScoped<IGrievanceSubcategoryService, GrievanceSubcategoryService>();
        serviceBuilder.AddScoped<IMeetingService, MeetingService>();
    }
}
