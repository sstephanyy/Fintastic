using Fintastic_API.Models;
using Fintastic_API.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;

namespace Fintastic_API.Tests
{
    public class IncomeIntegrationTests 
    {
       

       [Fact]
        public async Task Create_Incomes_ReturnsCreatedStatusCode()
        {
          
                await using var application = new MockDb();

                await IncomeMockData.CreateIncomes(application, true);

                var income = new Income
                {
                    IncomeId = 12,
                    Description = "Sal�rio de Maio",
                    Amount = 5000,
                    CategoryId = 1,
                    TransactionDate = new DateTime(2024, 5, 1) 
                };

                var client = application.CreateClient();

                var response = await client.PostAsJsonAsync("/receita", income);

                var incomes = await client.GetFromJsonAsync<Income[]>("/receita");

                response.EnsureSuccessStatusCode();

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                incomes.Should().NotBeNull();
        }

        [Fact]
        public async Task GET_All_Incomes()
        {
            await using var application = new MockDb();

            await IncomeMockData.CreateIncomes(application, true);
            var url = "/receita";

            var client = application.CreateClient();

            var result = await client.GetAsync(url);
            var incomes = await client.GetFromJsonAsync<Income[]>("/receita");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(incomes);
        }

        [Fact]
        public async Task GET_Income_By_Id_Returns_Ok()
        {
            // Arrange
            await using var application = new MockDb();
            await IncomeMockData.CreateIncomes(application, true);

            var client = application.CreateClient();
            var incomeId = 11;
            var url = $"/receita/{incomeId}"; 

            // Act
            var response = await client.GetAsync(url);
            var income = await response.Content.ReadFromJsonAsync<Income>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            income.Should().NotBeNull();
        }

        [Fact]
        public async Task DELETE_Income_That_NotExists()
        {
            await using var application = new MockDb();

            await IncomeMockData.CreateIncomes(application, true);
            var url = "/receita/1";

            var client = application.CreateClient();
            var response = await client.DeleteAsync(url);

            var incomes = await client.GetFromJsonAsync<Income[]>("/receita");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotNull(incomes);
     
        }

        [Fact]
        public async Task DELETE_Income_That_Exists()
        {
            await using var application = new MockDb();

            await IncomeMockData.CreateIncomes(application, true);
            var url = "/receita/11";

            var client = application.CreateClient();
            var response = await client.DeleteAsync(url);

            var incomes = await client.GetFromJsonAsync<Income[]>("/receita");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(incomes);

        }




    }
}