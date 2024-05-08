using Fintastic_API.Data;
using Fintastic_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minimal.Tests.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal.Tests.Controllers
{

    public class TestIncomeController
    {
        [Fact]
        public async Task GetIncomes_ReturnsOkResultWithIncomes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "IncomeTestDatabase")
                .Options;

            // Preencha o banco de dados em memória com dados de teste
            using (var context = new DataContext(options))
            {
                context.Incomes.AddRange(IncomeMockData.GetIncomes());
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                // Simule o endpoint da Minimal API
                var result = await context.Incomes.Include(i => i.Category).ToListAsync();

                // Act
                var actionResult = Results.Ok(result);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<Income>>(okResult.Value);
                Assert.True(model.Any()); // Verifica se há alguma renda retornada

            }

        }
    }
}
