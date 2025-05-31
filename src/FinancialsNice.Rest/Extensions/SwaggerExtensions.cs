using System.Reflection;
using FinancialsNice.Rest.Swagger.Filters;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinNice API", Version = "v1" });

            c.EnableAnnotations();
            c.ExampleFilters();

            // Custom operation filters for Swagger documentation (mantidos aqui por serem relacionados ao Swagger)
            c.OperationFilter<AddressSwaggerOperationFilter>();
            c.OperationFilter<CardSwaggerOperationFilter>();
            c.OperationFilter<DashboardSwaggerOperationFilter>();
            c.OperationFilter<ReportSwaggerOperationFilter>();
            c.OperationFilter<AuthSwaggerOperationFilter>();
            c.OperationFilter<EmailSenderSwaggerOperationFilter>();
            c.OperationFilter<PayerReceiverSwaggerOperationFilter>();
            c.OperationFilter<PaymentSwaggerOperationFilter>();
            c.OperationFilter<PermissionSwaggerOperationFilter>();
            c.OperationFilter<ReportSwaggerOperationFilter>();
            c.OperationFilter<RoleSwaggerOperationFilter>();
            c.OperationFilter<TransactionSwaggerOperationFilter>();
            c.OperationFilter<UserSwaggerOperationFilter>();
            c.OperationFilter<GoalSwaggerOperationFilter>();
            c.OperationFilter<WalletSwaggerOperationFilter>();

            // JWT Security Scheme for Swagger UI
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter JWT token with 'Bearer ' prefix",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            // Require JWT authentication in Swagger UI
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });

            // Include XML comments for Swagger documentation
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });

        // Register Swagger examples
        services.AddSwaggerExamplesFromAssemblyOf<Program>();

        return services;
    }
    
    // Este método encapsula o middleware do Swagger e Scalar UI
    public static IApplicationBuilder UseScalarUI(this WebApplication app)
    {
        app.UseSwagger();

        app.MapScalarApiReference("/scalar", options =>
        {
            options.AddDocument("v1", routePattern: "/swagger/v1/swagger.json");

            options.Theme = ScalarTheme.BluePlanet;
            options.Layout = ScalarLayout.Classic;
            options.Title = "FinNice API";
            options.DarkMode = true;

            options.WithPersistentAuthentication();
        });
        
        return app;
    }
}