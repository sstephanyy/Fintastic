using Fintastic_API.Models;
using Fintastic_API.Tests.Helpers;
using Fintastic_API.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fintastic_API.Enums;
using FluentAssertions;
using FluentAssertions.Equivalency;

namespace Fintastic_API.Tests
{
    public class CategoryIntegrationTests
    {
        [Fact]
        public async Task GET_All_Categories_ReturnsOk()
        {
            await using var application = new MockDb();

            await CategoryMockData.CreateCategories(application, true);
            var url = "/categoria";

            var client = application.CreateClient();
            var result = await client.GetAsync(url);
            var categories = await client.GetFromJsonAsync<Category[]>(url);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(categories);

            // Verificar se há pelo menos uma categoria de renda (income) e uma de despesa (expense)
            var incomeCategory = categories.FirstOrDefault(c => c.Type == CategoryType.Income);
            var expenseCategory = categories.FirstOrDefault(c => c.Type == CategoryType.Expense);

            Assert.NotNull(incomeCategory); // Verificar se há pelo menos uma categoria de renda
            Assert.NotNull(expenseCategory); // Verificar se há pelo menos uma categoria de despesa

        }

        [Fact]
        public async Task GET_Category_By_Id_Returns_Ok()
        {
            // Arrange
            await using var application = new MockDb();
            await CategoryMockData.CreateCategories(application, true);

            var client = application.CreateClient();
            var categoryId = 1;
            var url = $"/categoria/{categoryId}";

            // Act
            var response = await client.GetAsync(url);
            var category = await response.Content.ReadFromJsonAsync<Category>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            category.Should().NotBeNull();

            // Verificar se a categoria é de renda (income) ou despesa (expense) com base no seu CategoryId
            if (category.CategoryId == 1 || category.CategoryId == 2)
            {
                category.Type.Should().Be(CategoryType.Income);
            }
            else if (category.CategoryId == 3 || category.CategoryId == 4)
            {
                category.Type.Should().Be(CategoryType.Expense);
            }
        }

        [Fact]
        public async Task CREATE_Category_Income_Returns_Created()
        {
            await using var application = new MockDb();
            await CategoryMockData.CreateCategories(application, true);

            var client = application.CreateClient();
            var url = "/categoria/";


            var category = new Category
            {
                CategoryId = 6,
                Type = CategoryType.Income,
                Title = "Bônus",
                Icon = "💰"
            };

            var response = await client.PostAsJsonAsync(url, category);
            var categories = await client.GetFromJsonAsync<Category[]>(url);

            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            category.Should().NotBeNull();
            var incomeCategory = categories.FirstOrDefault(c => c.Type == CategoryType.Income);
            Assert.NotNull(incomeCategory); // Verificar se há pelo menos uma categoria de renda

        }

        [Fact]
        public async Task CREATE_Category_Expense_Returns_Created()
        {
            await using var application = new MockDb();
            await CategoryMockData.CreateCategories(application, true);

            var client = application.CreateClient();
            var url = "/categoria/";


            var category = new Category
            {
                CategoryId = 7,
                Type = CategoryType.Expense,
                Title = "Lazer",
                Icon = "💰"
            };

            var response = await client.PostAsJsonAsync(url, category);
            var categories = await client.GetFromJsonAsync<Category[]>(url);

            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            category.Should().NotBeNull();
            var expenseCategory = categories.FirstOrDefault(c => c.Type == CategoryType.Expense);
            Assert.NotNull(expenseCategory); 

        }

        [Fact]
        public async Task PUT_Update_Category_Income_Returns_Ok()
        {
            await using var application = new MockDb();
            await CategoryMockData.CreateCategories(application, true);

            var client = application.CreateClient();
            var url = "/categoria/1";


            var category = new Category
            {
                CategoryId = 1,
                Type = CategoryType.Income,
                Title = "Bônus",
                Icon = "💰"
            };

            var response = await client.PutAsJsonAsync(url, category);
            var categoriesUpdated = await client.GetFromJsonAsync<Category>(url);

            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            category.Should().NotBeNull();
          

        }

        [Fact]
        public async Task PUT_Update_Category_Expense_Returns_Ok()
        {
            await using var application = new MockDb();
            await CategoryMockData.CreateCategories(application, true);

            var client = application.CreateClient();
            var url = "/categoria/1";


            var category = new Category
            {
                CategoryId = 4,
                Type = CategoryType.Expense,
                Title = "Lazer",
                Icon = "💰"
            };

            var response = await client.PutAsJsonAsync(url, category);
            var categoriesUpdated = await client.GetFromJsonAsync<Category>(url);

            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            category.Should().NotBeNull();
           

        }

        [Fact]
        public async Task DELETE_Category_That_Exists_Ok()
        {
            await using var application = new MockDb();
            await CategoryMockData.CreateCategories(application, true);

            var client = application.CreateClient();
            var categoryId = 2;
            var url = $"/categoria/{categoryId}";

            var response = await client.DeleteAsync(url);
            var categorydeleted = await client.GetFromJsonAsync<Category[]>("/categoria");

            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }


    }
}
