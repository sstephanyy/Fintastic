using Fintastic_API.Models;
using Fintastic_API.Tests.Helpers;
using Fintastic_API.Tests.Mock;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Fintastic_API.Tests
{
    public class SpentIntegrationTests
    {
        [Fact]
        public async Task GET_All_Spents_ReturnsOk()
        {
            await using var application = new MockDb();

            await SpentMockData.CreateSpents(application, true);
            var url = "/despesas";

            var client = application.CreateClient();
            var result = await client.GetAsync(url);
            var spents = await client.GetFromJsonAsync<Spent[]>(url);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            spents.Should().NotBeNull();
            spents.Should().NotBeEmpty(); // Verifica se há pelo menos uma despesa
        }

        [Fact]
        public async Task GET_Spent_By_Id_Returns_Ok()
        {
            // Arrange
            await using var application = new MockDb();
            await SpentMockData.CreateSpents(application, true);

            var client = application.CreateClient();
            var spentId = 1;
            var url = $"/despesas/{spentId}";

            // Act
            var response = await client.GetAsync(url);
            var spent = await response.Content.ReadFromJsonAsync<Spent>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            spent.Should().NotBeNull();
        }

        [Fact]
        public async Task CREATE_Spent_Returns_Created()
        {
            await using var application = new MockDb();
            await SpentMockData.CreateSpents(application, true);
            await CategoryMockData.CreateCategories(application, true);

            var client = application.CreateClient();
            var url = "/despesas";

            var spent = new Spent
            {
                SpendingId = 6,
                AmountSpent = 150.0m,
                Description = "Compra de materiais",
                TransactionDate = DateTime.Now,
                CategoryId = 1
            };

            var response = await client.PostAsJsonAsync(url, spent);
            var spents = await client.GetFromJsonAsync<Spent[]>(url);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            spent.Should().NotBeNull();
            spents.Should().Contain(s => s.SpendingId == spent.SpendingId);
        }

        [Fact]
        public async Task PUT_Update_Spent_Returns_Ok()
        {
            await using var application = new MockDb();
            await SpentMockData.CreateSpents(application, true);

            var client = application.CreateClient();
            var url = "/despesas/1";

            var spent = new Spent
            {
                SpendingId = 1,
                AmountSpent = 200.0m,
                Description = "Atualização de materiais",
                TransactionDate = DateTime.Now,
                CategoryId = 1
            };

            var response = await client.PutAsJsonAsync(url, spent);
            var updatedSpent = await client.GetFromJsonAsync<Spent>(url);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedSpent.Should().NotBeNull();
        }

        [Fact]
        public async Task DELETE_Spent_That_Exists_Ok()
        {
            await using var application = new MockDb();
            await SpentMockData.CreateSpents(application, true);

            var client = application.CreateClient();
            var spentId = 2;
            var url = $"/despesas/{spentId}";

            var response = await client.DeleteAsync(url);
            var spents = await client.GetFromJsonAsync<Spent[]>("/despesas");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            spents.Should().NotContain(s => s.SpendingId == spentId);
        }
    }
}
