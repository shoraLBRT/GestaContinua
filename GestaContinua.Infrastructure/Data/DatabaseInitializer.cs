using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestaContinua.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<GestaContinuaDbContext>();

            try
            {
                context.Database.EnsureCreated();
                Console.WriteLine("Database ensured successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while ensuring the database: {ex.Message}");
                throw;
            }
        }
    }
}