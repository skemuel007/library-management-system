using Hangfire;
using LibraryBackend.Application.Dtos.Response;
using LibraryBackend.Application.Interfaces.Persistence;
using LibraryBackend.Core.Entities;
using LibraryBackend.Infrastructure.Context;
using LibraryBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace LibraryBackend.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static void AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region -- Database connection configuration
        var connectionString = configuration.GetConnectionString("LibraryAppConnectionString");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                // options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        #endregion
        
        #region-- Hangfire Setup
        services.AddHangfire(x =>
        {
            x.UseSerializerSettings(new Newtonsoft.Json.JsonSerializerSettings() { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All });
            x.UseSqlServerStorage(configuration.GetConnectionString("LibraryAppConnectionString"));
        });
        
        services.AddHangfireServer();
        #endregion

        #region -- Add Repository to service collection
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        #endregion
    }

    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                var retry = Policy.Handle<SqlException>()
                    .WaitAndRetry(
                        retryCount: 5,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        onRetry: (exception, retryCount, context) =>
                        {
                            logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                        }
                    );
                retry.Execute(() => InvokeSeeder(seeder, context, services));

                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "An error occured while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            }
        }
        return host;
    }
    
    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
        TContext context,
        IServiceProvider services)
        where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}