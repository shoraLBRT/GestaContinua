using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace GestaContinua.Application.UseCases
{
    public class UpdateTaskStatusUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskStatusUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> ExecuteAsync(Guid taskId, string newStatus)
        {
            var validStatuses = new[] { "Active", "Paused", "Completed" };
            if (!validStatuses.Contains(newStatus))
            {
                throw new ArgumentException("Invalid status", nameof(newStatus));
            }

            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new ArgumentException("Task not found", nameof(taskId));
            }

            task.Status = newStatus;

            // If pausing, clear the reminder
            if (newStatus == "Paused")
            {
                task.NextReminderAt = null;
            }

            await _taskRepository.UpdateAsync(task);
            return true;
        }
    }
}