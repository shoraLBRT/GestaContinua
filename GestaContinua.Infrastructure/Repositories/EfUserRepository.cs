using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using GestaContinua.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaContinua.Infrastructure.Repositories
{
    public class EfUserRepository : IUserRepository
    {
        private readonly GestaContinuaDbContext _context;

        public EfUserRepository(GestaContinuaDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByTelegramIdAsync(long telegramId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            user.LastActiveAt = DateTime.UtcNow;
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow; // Note: User entity doesn't have UpdatedAt, we might want to add it
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}