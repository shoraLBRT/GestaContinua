using GestaContinua.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaContinua.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
    }
}