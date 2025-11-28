using GestaContinua.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaContinua.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByTelegramIdAsync(long telegramId);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
    }
}