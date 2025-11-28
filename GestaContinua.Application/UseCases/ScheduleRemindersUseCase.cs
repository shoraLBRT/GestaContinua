using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace GestaContinua.Application.UseCases
{
    public class ScheduleRemindersUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;

        public ScheduleRemindersUseCase(
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            INotificationService notificationService)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
        }

        public async Task ExecuteAsync(DateTime now)
        {
            var tasks = await _taskRepository.GetWithRemindersDueAsync(now);

            foreach (var task in tasks)
            {
                // Get user to send notification
                var user = await _userRepository.GetByIdAsync(task.UserId);
                if (user == null) continue; // Skip if user not found

                // Send reminder notification
                var message = $"Reminder: Don't forget to work on '{task.Name}'!";
                var success = await _notificationService.SendReminderAsync(user.TelegramId, task.Id, message);

                if (success)
                {
                    // Set next reminder to 5 minutes from now if task is not completed
                    if (task.Status != "Completed")
                    {
                        task.NextReminderAt = now.AddMinutes(5);
                        await _taskRepository.UpdateAsync(task);
                    }
                }
            }
        }
    }
}