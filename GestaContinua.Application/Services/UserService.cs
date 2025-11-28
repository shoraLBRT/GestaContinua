using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace GestaContinua.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> RegisterUserAsync(long telegramId)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByTelegramIdAsync(telegramId);
            if (existingUser != null)
            {
                return existingUser; // Return existing user
            }

            // Create new user
            var user = new User
            {
                Id = Guid.NewGuid(),
                TelegramId = telegramId,
                CreatedAt = DateTime.UtcNow,
                LastActiveAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = "Active"
            };

            return await _userRepository.CreateAsync(user);
        }

        public async Task<User?> GetUserByTelegramIdAsync(long telegramId)
        {
            return await _userRepository.GetByTelegramIdAsync(telegramId);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
    }
}