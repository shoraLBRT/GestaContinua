using System;
using System.Threading.Tasks;

namespace GestaContinua.Application.Services
{
    public interface INotificationService
    {
        Task<bool> SendReminderAsync(long telegramId, Guid taskId, string message);
    }
}