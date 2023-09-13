using LibraryBackend.Api.Extensions;
using LibraryBackend.Application.Behaviours;
using LibraryBackend.Application.Extensions;
using LibraryBackend.Infrastructure.Configurations;
using LibraryBackend.Infrastructure.Context;
using LibraryBackend.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// serilog configuration added
builder.Host.UseSerilog(SeriLogger.Configure);

builder.WebHost.ConfigureKestrel(ck =>
{
    ck.ConfigureHttpsDefaults(httpDf =>
    {
        httpDf.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
    });
});

// Add services to the container.
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureWebApplicationServices();

app.MigrateDatabase<ApplicationDbContext>((context, services) =>
{
    var logger = services.GetService<ILogger<ApplicationDbContext>>();
    LibraryAppContextSeeder
        .SeedAsync(context, logger)
        .Wait();
}).Run();