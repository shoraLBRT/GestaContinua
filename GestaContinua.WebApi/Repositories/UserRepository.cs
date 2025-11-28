using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaContinua.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();

        public Task<User?> GetByIdAsync(Guid id)
        {
            var user = _users.Find(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<User?> GetByTelegramIdAsync(long telegramId)
        {
            var user = _users.Find(u => u.TelegramId == telegramId);
            return Task.FromResult(user);
        }

        public Task<User> CreateAsync(User user)
        {
            user.Id = Guid.NewGuid();
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User> UpdateAsync(User user)
        {
            var index = _users.FindIndex(u => u.Id == user.Id);
            if (index != -1)
            {
                _users[index] = user;
            }
            return Task.FromResult(user);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}