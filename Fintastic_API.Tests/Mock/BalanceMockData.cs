using Fintastic_API.Data;
using Fintastic_API.Enums;
using Fintastic_API.Models;
using Fintastic_API.Service;
using Fintastic_API.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;


namespace Fintastic_API.Tests.Mock
{
    public class BalanceMockData
    {
        public static async Task CreateBalances(MockDb application, bool criar)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var balanceDbContext = provider.GetRequiredService<DataContext>())
                {

                    await balanceDbContext.Database.EnsureCreatedAsync();

                    if (criar)
                    {
                        await balanceDbContext.Balances.AddAsync(
                            new Balance
                            {
                                Total = 100m,
                                TotalIncome = 50m,
                                TotalSpent = 430m
                            });

                        await balanceDbContext.Balances.AddAsync(new Balance
                        {
                            Total = 5040m,
                            TotalIncome = 4550m,
                            TotalSpent = 580m
                        });

                        await balanceDbContext.Balances.AddAsync(new Balance
                        {
                            Total = 800m,
                            TotalIncome = 55m,
                            TotalSpent = 40m
                        });

                        await balanceDbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
