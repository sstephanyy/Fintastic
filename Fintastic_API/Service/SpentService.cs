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

        public async Task AddRegister(Spent spent)
        {
            await _db.Spents.AddAsync(spent);
            await _db.SaveChangesAsync();

        }

        public async Task<bool> DeleteRegister(int id)
        {
            var spent = _db.Spents.FirstOrDefault(x => x.SpendingId == id);
            if (spent == null) return false;

            _db.Spents.Remove(spent);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Spent> GetRegisterById(int id)
        {
            return await _db.Spents.FirstOrDefaultAsync(x => x.SpendingId == id);
        }

        public async Task<IEnumerable<Spent>> GetRegisters()
        {
            return _db.Spents;
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
    }
}
