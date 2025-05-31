namespace FinancialsNice.Rest.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL");
        var backendUrl = Environment.GetEnvironmentVariable("BACKEND_URL");

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", policy =>
            {
                var allowedOrigins = new List<string>();
                if (!string.IsNullOrEmpty(frontendUrl)) allowedOrigins.Add(frontendUrl);
                if (!string.IsNullOrEmpty(backendUrl)) allowedOrigins.Add(backendUrl);

                policy.WithOrigins(allowedOrigins.ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        return services;
    }
    
    // Encapsula o middleware de CORS (UseCors)
    public static IApplicationBuilder UseAppCorsMiddleware(this IApplicationBuilder app)
    {
        app.UseCors("AllowSpecificOrigins");
        return app;
    }
}