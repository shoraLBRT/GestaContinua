using GestaContinua.Domain.Entities;
using GestaContinua.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaContinua.WebApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private static readonly List<Category> _categories = new List<Category>();

        public Task<Category?> GetByIdAsync(Guid id)
        {
            var category = _categories.Find(c => c.Id == id);
            return Task.FromResult(category);
        }

        public Task<Category> CreateAsync(Category category)
        {
            category.Id = Guid.NewGuid();
            _categories.Add(category);
            return Task.FromResult(category);
        }

        public Task<Category> UpdateAsync(Category category)
        {
            var index = _categories.FindIndex(c => c.Id == category.Id);
            if (index != -1)
            {
                _categories[index] = category;
            }
            return Task.FromResult(category);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var category = _categories.Find(c => c.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.FromResult(_categories.AsEnumerable());
        }
    }
}