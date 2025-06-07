using DotNetEnv;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using FinancialsNice.Infrastructure.Repositories;
using FinancialsNice.Worker;
using Microsoft.EntityFrameworkCore;

Env.Load("../../.env");

var builder = Host.CreateApplicationBuilder(args);

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME");
var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");

var connectionString = $"Host={dbHost};Port={dbPort};Username={dbUser};Password={dbPass};Database={dbName}";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

var host = builder.Build();
host.Run();
