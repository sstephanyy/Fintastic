using Fintastic_API.Data;
using Fintastic_API.Models;
using Fintastic_API.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fintastic_API.Service
{
    public class IncomeService : IService<Income>
    {
        private DataContext _db;

        public IncomeService(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Income>> GetRegisters()
        {
            return  _db.Incomes;
        }

        public async Task<Income> GetRegisterById(int id)
        {
            return await _db.Incomes.FirstOrDefaultAsync(x => x.IncomeId == id);
        }

        public async Task AddRegister(Income income)
        {
            await _db.Incomes.AddAsync(income);
            await _db.SaveChangesAsync();
        }

        public async Task<Income> UpdateRegister(int id, Income income)
        {
            var incomeUpdate = await _db.Incomes.FirstOrDefaultAsync(x => x.IncomeId == id);
            if (incomeUpdate == null) return null;

            incomeUpdate.Description = income.Description;
            incomeUpdate.Amount = income.Amount;
            incomeUpdate.TransactionDate = income.TransactionDate;
            incomeUpdate.Category = income.Category;

            _db.Incomes.Update(incomeUpdate);
            await _db.SaveChangesAsync();

            return incomeUpdate;
        }


        public async Task<bool> DeleteRegister(int id)
        {
            var income = _db.Incomes.FirstOrDefault(x => x.IncomeId == id);
            if (income == null) return false;

            _db.Incomes.Remove(income);
            await _db.SaveChangesAsync();
            return true;
        }

    }
}
