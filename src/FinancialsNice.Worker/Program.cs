using DotNetEnv;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Data;
using FinancialsNice.Infrastructure.Repositories;
using FinancialsNice.Worker;
using Microsoft.EntityFrameworkCore;

Env.Load("../../.env");

var builder = Host.CreateApplicationBuilder(args);

var conn = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(conn));

builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

var host = builder.Build();
host.Run();
