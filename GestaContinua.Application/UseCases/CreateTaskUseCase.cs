using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace GestaContinua.Application.UseCases
{
    public class CreateTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITaskScheduler _taskScheduler;

        public CreateTaskUseCase(
            ITaskRepository taskRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            ITaskScheduler taskScheduler)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _taskScheduler = taskScheduler;
        }

        public async Task<Guid> ExecuteAsync(
            Guid userId,
            Guid categoryId,
            string name,
            double goal,
            string inputFormat,
            string schedule,
            int weight = 1)
        {
            // Validate if user exists
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found", nameof(userId));
            }

            // Validate if category exists
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new ArgumentException("Category not found", nameof(categoryId));
            }

            // Validate InputFormat and Schedule
            var validInputFormats = new[] { "Boolean", "Number", "Text", "Custom" };
            if (!validInputFormats.Contains(inputFormat))
            {
                throw new ArgumentException("Invalid InputFormat", nameof(inputFormat));
            }

            var validSchedules = new[] { "Daily", "Weekly", "Custom", "Once", "BiWeekly" };
            if (!validSchedules.Contains(schedule))
            {
                throw new ArgumentException("Invalid Schedule", nameof(schedule));
            }

            // Create task
            var task = new Task
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CategoryId = categoryId,
                Name = name,
                Goal = goal,
                Progress = 0,
                Weight = weight,
                Schedule = schedule,
                Status = "Active",
                InputFormat = inputFormat,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            task.NextReminderAt = _taskScheduler.CalculateNextReminder(task, DateTime.UtcNow);

            var createdTask = await _taskRepository.CreateAsync(task);
            return createdTask.Id;
        }
    }
}