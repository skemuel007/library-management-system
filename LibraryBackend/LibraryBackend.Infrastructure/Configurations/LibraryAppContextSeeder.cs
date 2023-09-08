using LibraryBackend.Core.Entities;
using LibraryBackend.Infrastructure.Context;
using Microsoft.Extensions.Logging;

namespace LibraryBackend.Infrastructure.Configurations;

public static class LibraryAppContextSeeder
{
    public static async Task SeedAsync(ApplicationDbContext dbContext, ILogger<ApplicationDbContext> logger)
    {
        logger.LogInformation("Attempting to seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
        if (!dbContext.Users.Any())
        {
            logger.LogInformation($"Seeding users into db...");
            //  dbContext.Users.AddRange(GetPreconfiguredUsers());
            // await dbContext.SaveChangesAsync();
            logger.LogInformation("Seeding user into db complete...");
        }

        logger.LogInformation($"Database seeding complete...");
    }

    private static IEnumerable<User> GetPreconfiguredUsers()
    {
        return new List<User>
        {

        };
    }
}