using GestaContinua.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GestaContinua.Infrastructure.Services
{
    public class ReminderBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IServiceProvider _serviceProvider;

        public ReminderBackgroundService(IServiceScopeFactory scopeFactory, IServiceProvider serviceProvider)
        {
            _scopeFactory = scopeFactory;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                
                var scheduleRemindersUseCase = scope.ServiceProvider.GetRequiredService<ScheduleRemindersUseCase>();
                
                try
                {
                    await scheduleRemindersUseCase.ExecuteAsync(DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    // Log the exception in a real implementation
                    Console.WriteLine($"Error in ReminderBackgroundService: {ex.Message}");
                }

                // Check every 2 minutes for due reminders
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }
    }
}