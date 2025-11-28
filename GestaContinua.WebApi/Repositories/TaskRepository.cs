using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaContinua.WebApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private static readonly List<Task> _tasks = new List<Task>();

        public Task<Task?> GetByIdAsync(Guid id)
        {
            var task = _tasks.Find(t => t.Id == id);
            return Task.FromResult(task);
        }

        public Task<Task> CreateAsync(Task task)
        {
            task.Id = Guid.NewGuid();
            _tasks.Add(task);
            return Task.FromResult(task);
        }

        public Task<Task> UpdateAsync(Task task)
        {
            var index = _tasks.FindIndex(t => t.Id == task.Id);
            if (index != -1)
            {
                _tasks[index] = task;
            }
            return Task.FromResult(task);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<IEnumerable<Task>> GetActiveByUserIdAsync(Guid userId)
        {
            var activeTasks = _tasks.Where(t => t.UserId == userId && t.Status == "Active").ToList();
            return Task.FromResult(activeTasks.AsEnumerable());
        }

        public Task<IEnumerable<Task>> GetWithRemindersDueAsync(DateTime now)
        {
            var dueTasks = _tasks.Where(t => t.NextReminderAt.HasValue && t.NextReminderAt.Value <= now && t.Status != "Completed").ToList();
            return Task.FromResult(dueTasks.AsEnumerable());
        }

        public Task<IEnumerable<Task>> GetAllByUserIdAsync(Guid userId)
        {
            var userTasks = _tasks.Where(t => t.UserId == userId).ToList();
            return Task.FromResult(userTasks.AsEnumerable());
        }
    }
}