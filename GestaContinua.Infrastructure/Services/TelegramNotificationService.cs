using GestaContinua.Application.Services;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace GestaContinua.Infrastructure.Services
{
    public class TelegramNotificationService : INotificationService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<TelegramNotificationService> _logger;

        public TelegramNotificationService(ITelegramBotClient botClient, ILogger<TelegramNotificationService> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }

        public async Task<bool> SendReminderAsync(long telegramId, Guid taskId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                _logger.LogWarning("Message is null or empty for task ID: {TaskId}", taskId);
                return false;
            }

            try
            {
                await _botClient.SendTextMessageAsync(telegramId, message);
                _logger.LogInformation("Reminder sent successfully to Telegram ID: {TelegramId} for task ID: {TaskId}", telegramId, taskId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send reminder to Telegram ID: {TelegramId} for task ID: {TaskId}. Exception: {ExceptionMessage}", telegramId, taskId, ex.Message);
                return false;
            }
        }
    }
}