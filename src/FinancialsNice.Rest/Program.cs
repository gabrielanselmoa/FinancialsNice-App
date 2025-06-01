using FinancialsNice.Rest;
using DotNetEnv;
using FinancialsNice.Application.Interfaces.WebHook;
using FinancialsNice.Infrastructure.Data;
using FinancialsNice.Rest.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Refit;

// var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
// Env.Load(isDocker ? "../.env" : "../../.env");
Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
// REGISTER EXTENSIONS SERVICES
builder.Services.AddSwaggerDocumentation();
builder.Services.AddJwtAuthentication();
builder.Services.AddMinioClient();
// REGISTER REFIT CLIENT DI
builder.Services.AddRefitClient<IWebhookClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://util.devi.tools/api"));
// REGISTER EXTENSIONS SERVICES
builder.Services.AddServices();
builder.Services.AddRepositories();
// REGISTER DATABASE DI
builder.Services.AddPostgreSqlDbContext();
// EXTENSIONS SERVICES - REGISTER CORS POLICY
builder.Services.AddCorsPolicy();
// APPLICATION BUILD & MIDDLEWARE
var app = builder.Build();
// MIDDLEWARE CONFIGURATION
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseScalarUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// EXTENSION SERVICES - CORS MIDDLEWARE
app.UseAppCorsMiddleware();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
// app.MapHealthChecks("/health");

app.Run();