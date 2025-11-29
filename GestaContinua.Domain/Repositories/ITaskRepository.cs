using GestaContinua.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaContinua.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<Domain.Entities.Task?> GetByIdAsync(Guid id);
        Task<Domain.Entities.Task> CreateAsync(Domain.Entities.Task task);
        Task<Domain.Entities.Task> UpdateAsync(Domain.Entities.Task task);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Domain.Entities.Task>> GetActiveByUserIdAsync(Guid userId);
        Task<IEnumerable<Domain.Entities.Task>> GetWithRemindersDueAsync(DateTime now);
        Task<IEnumerable<Domain.Entities.Task>> GetAllByUserIdAsync(Guid userId);
    }
}