using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

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
                // Wait a bit to ensure PostgreSQL server is ready (useful for containerized setups)
                await Task.Delay(1000);

                context.Database.EnsureCreated();
                Console.WriteLine("Database ensured successfully.");
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"A PostgreSQL error occurred while ensuring the database: {ex.Message}");
                Console.WriteLine("Make sure PostgreSQL server is running and connection string is correct.");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while ensuring the database: {ex.Message}");
                throw;
            }
        }
    }
}