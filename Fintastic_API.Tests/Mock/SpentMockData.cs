using Fintastic_API.Data;
using Fintastic_API.Models;
using Fintastic_API.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Fintastic_API.Tests.Mock
{
    public static class SpentMockData
    {
        public static async Task CreateSpents(MockDb application, bool criar)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var spentDbContext = provider.GetRequiredService<DataContext>())
                {
                    await spentDbContext.Database.EnsureCreatedAsync();

                    if (criar)
                    {
                        await spentDbContext.Spents.AddAsync(new Spent
                        {
                            SpendingId = 1,
                            AmountSpent = 500.0m,
                            Description = "Compra de suprimentos",
                            TransactionDate = DateTime.Now.AddDays(-10),
                            CategoryId = 1
                        });

                        await spentDbContext.Spents.AddAsync(new Spent
                        {
                            SpendingId = 2,
                            AmountSpent = 150.0m,
                            Description = "Jantar fora",
                            TransactionDate = DateTime.Now.AddDays(-5),
                            CategoryId = 2
                        });

                        await spentDbContext.Spents.AddAsync(new Spent
                        {
                            SpendingId = 3,
                            AmountSpent = 300.0m,
                            Description = "Manutenção do carro",
                            TransactionDate = DateTime.Now.AddDays(-3),
                            CategoryId = 3
                        });

                        await spentDbContext.Spents.AddAsync(new Spent
                        {
                            SpendingId = 4,
                            AmountSpent = 75.0m,
                            Description = "Combustível",
                            TransactionDate = DateTime.Now.AddDays(-1),
                            CategoryId = 4
                        });

                        await spentDbContext.SaveChangesAsync();
                    }
                }
            }
        }

    }
}
