using Fintastic_API.Models;
using Fintastic_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Fintastic_API.Services
{
    public class SpentService : IService<Spent>
    {
        private DataContext _db;

        public SpentService(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Spent>> GetRegisters()
        {
            var spents = await _db.Spents.Include(i => i.Category).ToListAsync();
            return _db.Spents;
        }

        public async Task<Spent> GetRegisterById(int id)
        {
            var spents = await _db.Spents.Include(i => i.Category).FirstOrDefaultAsync(i => i.SpendingId == id);
            return await _db.Spents.FirstOrDefaultAsync(x => x.SpendingId == id);
        }

        public async Task AddRegister(Spent spent)
        {
            var category = await _db.Categories.FindAsync(spent.CategoryId);
            if (category == null)
            {
                throw new Exception("Categoria não existe.");

            }
            await _db.Spents.AddAsync(spent);
            await _db.SaveChangesAsync();

        }

        public async Task<Spent> UpdateRegister(int id, Spent spent)
        {
            var spentToBeUpdated = await _db.Spents.FirstOrDefaultAsync(x => x.SpendingId == id);
            if (spentToBeUpdated == null) return null;

            spentToBeUpdated.Description = spent.Description;
            spentToBeUpdated.AmountSpent = spent.AmountSpent;
            spentToBeUpdated.TransactionDate = spent.TransactionDate;
            spentToBeUpdated.Category = spent.Category;

            _db.Spents.Update(spentToBeUpdated);
            await _db.SaveChangesAsync();

            return spentToBeUpdated;

        }

        public async Task<bool> DeleteRegister(int id)
        {
            var spent = _db.Spents.FirstOrDefault(x => x.SpendingId == id);
            if (spent == null) return false;

            _db.Spents.Remove(spent);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
