using GestaContinua.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GestaContinua.Infrastructure.Services
{
    public class ReminderBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ReminderBackgroundService> _logger;
        private readonly TimeSpan _interval;

        public ReminderBackgroundService(IServiceScopeFactory scopeFactory, ILogger<ReminderBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _interval = TimeSpan.FromMinutes(2); // Default interval
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                
                var scheduleRemindersUseCase = scope.ServiceProvider.GetRequiredService<ScheduleRemindersUseCase>();
                
                try
                {
                    _logger.LogDebug("Executing ScheduleRemindersUseCase at {ExecutionTime}", DateTime.UtcNow);
                    await scheduleRemindersUseCase.ExecuteAsync(DateTime.UtcNow);
                    _logger.LogDebug("Successfully executed ScheduleRemindersUseCase");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while executing ScheduleRemindersUseCase at {ExecutionTime}", DateTime.UtcNow);
                }

                _logger.LogDebug("Waiting for {Interval} before next reminder check", _interval);
                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}