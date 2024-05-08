using Fintastic_API.Data;
using Fintastic_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Fintastic_API.Endpoints
{
    public class IncomesEndpointsV1
    {
        public static async Task<IResult> AddIncome(Income newIncome, DataContext context)
        {
            // Verifica se a categoria existe
            var category = await context.Categories.FindAsync(newIncome.CategoryId);
            if (category == null)
            {
                return Results.NotFound(new { message = "Categoria não encontrada." });
            }

            context.Incomes.Add(newIncome);
            await context.SaveChangesAsync();

            return Results.Created($"/receita/{newIncome.IncomeId}", newIncome);
        }
    }
}
