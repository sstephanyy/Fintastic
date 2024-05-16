using Fintastic_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal.Tests.MockData
{
    public class IncomeMockData
    {
        public static List<Income> GetIncomes()
        {
            return new List<Income>()
            {
                new Income
                {
                    IncomeId = 1,
                    Description = "Salário",
                    Amount = 5000.00m,
                    CategoryId = 1,
                    Category = new Category { CategoryId = 1, Title = "Salário", Icon = "💰", Type = Fintastic_API.Enums.CategoryType.Income },
                    TransactionDate = new DateTime(2024, 5, 1)
                },
                new Income
                {
                    IncomeId = 2,
                    Description = "Freelance",
                    Amount = 1500.50m,
                    CategoryId = 2,
                    Category = new Category { CategoryId = 2, Title = "Trabalho Autônomo", Icon = "💰", Type = Fintastic_API.Enums.CategoryType.Income },
                    TransactionDate = new DateTime(2024, 5, 10)
                },
                new Income
                {
                    IncomeId = 3,
                    Description = "Rendimento de Investimentos",
                    Amount = 200.00m,
                    CategoryId = 3,
                    Category = new Category { CategoryId = 3, Title = "Investimentos", Icon = "💰", Type = Fintastic_API.Enums.CategoryType.Income },
                    TransactionDate = new DateTime(2024, 5, 15)
                }
            };
        }
    }
}
