using System.Reflection;
using FluentValidation;
using LibraryBackend.Application.Behaviours;
using LibraryBackend.Application.Utilities.Configurations;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryBackend.Application.Extensions;

public static class ApplicationServiceExtension
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        services.Configure<CacheSetting>(configuration.GetSection("CacheSetting"));
        services.Configure<ElasticConfiguration>(configuration.GetSection("ElasticConfiguration"));
        
        services.AddScoped<JwtHandler>();
    }
}