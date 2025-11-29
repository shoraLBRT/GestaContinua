using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using GestaContinua.Application.Services;
using System;
using System.Threading.Tasks;

namespace GestaContinua.Application.UseCases
{
    public class ProcessUserResponseUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProgressRecordRepository _progressRecordRepository;
        private readonly ITaskScheduler _taskScheduler;

        public ProcessUserResponseUseCase(
            ITaskRepository taskRepository,
            IProgressRecordRepository progressRecordRepository,
            ITaskScheduler taskScheduler)
        {
            _taskRepository = taskRepository;
            _progressRecordRepository = progressRecordRepository;
            _taskScheduler = taskScheduler;
        }

        public async System.Threading.Tasks.Task<bool> ExecuteAsync(Guid taskId, object value)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new ArgumentException("Task not found", nameof(taskId));
            }

            if (task.Status == "Completed")
            {
                throw new InvalidOperationException("Cannot update a completed task");
            }

            // Validate value type against InputFormat
            ValidateValueType(task.InputFormat, value);

            // Create progress record
            var progressRecord = new ProgressRecord
            {
                Id = Guid.NewGuid(),
                TaskId = taskId,
                Value = value,
                RecordedAt = DateTime.UtcNow,
                IsCompleted = value is bool boolValue ? boolValue : true
            };

            await _progressRecordRepository.CreateAsync(progressRecord);

            // Update task progress based on input format
            UpdateTaskProgress(task, value);

            // Check if task is completed
            if (task.Progress >= task.Goal)
            {
                task.Status = "Completed";
                task.NextReminderAt = null;
            }
            else
            {
                // Reschedule the reminder if the task is still active
                _taskScheduler.RescheduleTask(task);
            }

            task.UpdatedAt = DateTime.UtcNow;
            await _taskRepository.UpdateAsync(task);

            return true;
        }

        private void ValidateValueType(string inputFormat, object value)
        {
            switch (inputFormat)
            {
                case "Boolean":
                    if (!(value is bool))
                        throw new ArgumentException("Value must be boolean for Boolean InputFormat", nameof(value));
                    break;
                case "Number":
                    if (!(value is int || value is double || value is float))
                        throw new ArgumentException("Value must be numeric for Number InputFormat", nameof(value));
                    break;
                case "Text":
                    if (!(value is string))
                        throw new ArgumentException("Value must be string for Text InputFormat", nameof(value));
                    break;
                case "Custom":
                    // Custom format can accept any value
                    break;
                default:
                    throw new ArgumentException("Invalid InputFormat", nameof(inputFormat));
            }
        }

        private void UpdateTaskProgress(Domain.Entities.Task task, object value)
        {
            switch (task.InputFormat)
            {
                case "Boolean":
                    if ((bool)value)
                    {
                        task.Progress += 1;
                    }
                    break;
                case "Number":
                    if (value is int intValue)
                    {
                        task.Progress += intValue;
                    }
                    else if (value is double doubleValue)
                    {
                        task.Progress += doubleValue;
                    }
                    else if (value is float floatValue)
                    {
                        task.Progress += floatValue;
                    }
                    break;
                case "Text":
                    // For text, we might increment by 1 or handle differently based on business logic
                    task.Progress += 1;
                    break;
                case "Custom":
                    // Handle custom format based on business logic
                    task.Progress += 1;
                    break;
            }
        }
    }
}