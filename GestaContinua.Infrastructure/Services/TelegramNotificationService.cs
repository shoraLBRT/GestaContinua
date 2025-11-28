using GestaContinua.Application.Services;
using Telegram.Bot;
using System;
using System.Threading.Tasks;

namespace GestaContinua.Infrastructure.Services
{
    public class TelegramNotificationService : INotificationService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly string _botToken;

        public TelegramNotificationService(ITelegramBotClient botClient, string botToken)
        {
            _botClient = botClient;
            _botToken = botToken;
        }

        public async Task<bool> SendReminderAsync(long telegramId, Guid taskId, string message)
        {
            try
            {
                await _botClient.SendTextMessageAsync(telegramId, message);
                return true;
            }
            catch (Exception)
            {
                // Log the exception in a real implementation
                return false;
            }
        }
    }
}