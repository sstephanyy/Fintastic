﻿using Fintastic_API.Data;
using Fintastic_API.Enums;
using Fintastic_API.Models;
using Fintastic_API.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintastic_API.Tests.Mock
{
    public class CategoryMockData
    {
        public static async Task CreateCategories(MockDb application, bool criar)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var categoryDbContext = provider.GetRequiredService<DataContext>())
                {
                    await categoryDbContext.Database.EnsureCreatedAsync();

                    if (criar)
                    {
                        await categoryDbContext.Categories.AddAsync(new Category
                        {
                            CategoryId = 1,
                            Title = "Salario",
                            Icon = "💰",
                            Type = CategoryType.Income
                        });

                        await categoryDbContext.Categories.AddAsync(new Category
                        {
                            CategoryId = 2,
                            Title = "Dividendos",
                            Icon = "💸",
                            Type = CategoryType.Income
                        });

                        await categoryDbContext.Categories.AddAsync(new Category
                        {
                            CategoryId = 3,
                            Title = "Alimentação",
                            Icon = "🍔",
                            Type = CategoryType.Expense
                        });

                        await categoryDbContext.Categories.AddAsync(new Category
                        {
                            CategoryId = 4,
                            Title = "Transporte",
                            Icon = "🚗",
                            Type = CategoryType.Expense
                        });

                        await categoryDbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
