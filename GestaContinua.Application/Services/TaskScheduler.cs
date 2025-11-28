using GestaContinua.Domain.Entities;
using GestaContinua.Application.Services;
using System;

namespace GestaContinua.Application.Services
{
    public class TaskScheduler : ITaskScheduler
    {
        public DateTime CalculateNextReminder(Task task, DateTime lastReminder)
        {
            return task.Schedule.ToLower() switch
            {
                "daily" => lastReminder.Date.AddDays(1).Add(GetTaskTime(task)),
                "weekly" => lastReminder.Date.AddDays(7).Add(GetTaskTime(task)),
                "biweekly" => lastReminder.Date.AddDays(14).Add(GetTaskTime(task)),
                "once" => task.Goal <= task.Progress ? DateTime.MaxValue : DateTime.MaxValue, // For one-time tasks, no more reminders after goal reached
                "custom" => lastReminder.AddHours(24), // Default for custom
                _ => lastReminder.Date.AddDays(1).Add(GetTaskTime(task)) // Default to daily
            };
        }

        public void RescheduleTask(Task task)
        {
            if (task.Status != "Completed" && task.Status != "Paused")
            {
                task.NextReminderAt = CalculateNextReminder(task, DateTime.UtcNow);
            }
        }

        private TimeSpan GetTaskTime(Task task)
        {
            // For simplicity, default to 9:00 AM
            // In a more sophisticated implementation, this could be configurable per task
            return new TimeSpan(9, 0, 0);
        }
    }
}