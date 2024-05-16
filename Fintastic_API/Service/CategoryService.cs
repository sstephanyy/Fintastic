using Fintastic_API.Data;
using Fintastic_API.Models;
using Fintastic_API.Services;
using Microsoft.EntityFrameworkCore;

namespace Fintastic_API.Service
{
    public class CategoryService : IService<Category>
    {
        private DataContext _db;

        public CategoryService(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetRegisters()
        {
            return _db.Categories;
        }

        public async Task<Category> GetRegisterById(int id)
        {
            return await _db.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task AddRegister(Category category)
        {

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
        }

        public async Task<Category> UpdateRegister(int id, Category category)
        {
            var categoryUpdate = await _db.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (categoryUpdate == null) return null;

            categoryUpdate.Title = category.Title;
            categoryUpdate.Icon = category.Icon;
            categoryUpdate.Type = category.Type;

            _db.Categories.Update(categoryUpdate);
            await _db.SaveChangesAsync();

            return categoryUpdate;
        }


        public async Task<bool> DeleteRegister(int id)
        {
            var category = _db.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (category == null) return false;

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return true;
        }



    }
}
