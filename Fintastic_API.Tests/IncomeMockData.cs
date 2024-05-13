using Fintastic_API.Data;
using Fintastic_API.Models;
using Fintastic_API.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintastic_API.Tests
{
    public class IncomeMockData
    {
        public static async Task CreateIncomes(MockDb application, bool criar)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var incomeDbContext = provider.GetRequiredService<DataContext>())
                {
                    await incomeDbContext.Database.EnsureCreatedAsync();

                    if (criar)
                    {
                        await incomeDbContext.Incomes.AddAsync(new Income
                        {
                            IncomeId = 10,
                            Description = "Salário de Abril",
                            Amount = 5000,
                            CategoryId = 1,
                            TransactionDate = new DateTime(2024, 4, 30)
                        });

                        await incomeDbContext.Incomes.AddAsync(new Income
                        {
                            IncomeId = 11,
                            Description = "Dividendos",
                            Amount = 1500,
                            CategoryId = 2,
                            TransactionDate = new DateTime(2024, 4, 30)

                        });

                        await incomeDbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
