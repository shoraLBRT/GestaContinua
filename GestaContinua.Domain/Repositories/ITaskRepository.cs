using GestaContinua.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaContinua.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<Task?> GetByIdAsync(Guid id);
        Task<Task> CreateAsync(Task task);
        Task<Task> UpdateAsync(Task task);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Task>> GetActiveByUserIdAsync(Guid userId);
        Task<IEnumerable<Task>> GetWithRemindersDueAsync(DateTime now);
        Task<IEnumerable<Task>> GetAllByUserIdAsync(Guid userId);
    }
}