using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using GestaContinua.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaContinua.Infrastructure.Repositories
{
    public class EfTaskRepository : ITaskRepository
    {
        private readonly GestaContinuaDbContext _context;

        public EfTaskRepository(GestaContinuaDbContext context)
        {
            _context = context;
        }

        public async Task<Task?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<Task> CreateAsync(Task task)
        {
            task.Id = Guid.NewGuid();
            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;
            
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> UpdateAsync(Task task)
        {
            task.UpdatedAt = DateTime.UtcNow;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Task>> GetActiveByUserIdAsync(Guid userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId && t.Status == "Active")
                .ToListAsync();
        }

        public async Task<IEnumerable<Task>> GetWithRemindersDueAsync(DateTime now)
        {
            return await _context.Tasks
                .Where(t => t.NextReminderAt.HasValue && t.NextReminderAt.Value <= now && t.Status != "Completed")
                .ToListAsync();
        }

        public async Task<IEnumerable<Task>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
    }
}