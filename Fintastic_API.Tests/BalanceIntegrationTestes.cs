using Fintastic_API.Models;
using Fintastic_API.Tests.Helpers;
using Fintastic_API.Tests.Mock;
using FluentAssertions;
using System;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using Fintastic_API.Data;
using Fintastic_API.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Fintastic_API.Tests
{
    public class BalanceIntegrationTestes
    {
        
        [Fact]
        public async Task CalculateTotalIncome_ShouldReturnCorrectTotalAmount()
        {
            // Arrange
            await using var application = new MockDb();
            await IncomeMockData.CreateIncomes(application, true);

            using (var scope = application.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var dbContext = serviceProvider.GetRequiredService<DataContext>();

                var balanceService = new BalanceService(dbContext);

                // Act
                var totalIncome = await balanceService.CalculateTotalIncome();

                // Assert
                Assert.Equal(6500.0m, totalIncome);
            }

        }

        [Fact]
        public async Task CalculateTotalExpensives_ShouldReturnCorrectTotalAmount()
        {
            // Arrange
            await using var application = new MockDb();
            await SpentMockData.CreateSpents(application, true);

            // Criar um escopo para acessar os serviços escopo
            using (var scope = application.Services.CreateScope())
            {
                // Obter o provedor de serviços do escopo
                var serviceProvider = scope.ServiceProvider;

                // Obter o contexto do banco de dados do provedor de serviços do escopo
                var dbContext = serviceProvider.GetRequiredService<DataContext>();

                // Create an instance of BalanceService with the mock DataContext
                var balanceService = new BalanceService(dbContext);

                // Act
                var totalSpent = await balanceService.CalculateTotalSpent();

                // Assert
                Assert.Equal(1025.0m, totalSpent); 
            }
        }


        [Fact]
        public async Task CalculateTotalIncomeAndExpenses_ShouldReturnCorrectTotalAmount()
        {
            // Arrange
            await using var application = new MockDb();
            await SpentMockData.CreateSpents(application, true);
            await IncomeMockData.CreateIncomes(application, true);

            using (var scope = application.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var dbContext = serviceProvider.GetRequiredService<DataContext>();

                var balanceService = new BalanceService(dbContext);

                var total = await balanceService.CalculateTotal();

                Assert.Equal(5475.0m, total);
            }
        }


    }
}
