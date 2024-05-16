using Fintastic_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fintastic_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
                
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Spent> Spents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Balance> Balances { get; set; }
    }
}
