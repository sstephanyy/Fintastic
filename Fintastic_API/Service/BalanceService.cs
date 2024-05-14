using Fintastic_API.Data;
using Fintastic_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fintastic_API.Service
{
    public class BalanceService : IBalance<Balance>
    {
        private DataContext _db;

        public BalanceService(DataContext db)
        {
            _db = db;
        }


        public async Task<decimal> CalculateTotalIncome()
        {
            decimal totalIncome = await _db.Incomes.SumAsync(i => i.Amount);
            await _db.SaveChangesAsync();
            return totalIncome;
        }

        public async Task<decimal> CalculateTotalSpent()
        {
            decimal totalSpent = await _db.Spents.SumAsync(s => s.AmountSpent);
            await _db.SaveChangesAsync();
            return totalSpent;
        }

        public async Task<decimal> CalculateTotal()
        {
            decimal totalIncome = await CalculateTotalIncome();
            decimal totalSpent = await CalculateTotalSpent();
            decimal total = totalIncome - totalSpent;

            // Save total balance to the database
            var balance = new Balance { 
                Total = total,
                TotalIncome = totalIncome,
                TotalSpent = totalSpent
            };
            _db.Balances.Add(balance);
            await _db.SaveChangesAsync();

            return total;
        }
    }
}
