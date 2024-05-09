using Fintastic_API.Models;
using Fintastic_API.Services;

namespace Fintastic_API.Routes
{
    public static class IncomeRoutes
    {
        public static void MapIncomeRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/receita", async (IService<Income> _incomeService) =>
            {
                var incomeList = await _incomeService.GetRegisters();
                return Results.Ok(incomeList);
            }).WithTags("Receita").WithName("GetIncomes");

            endpoints.MapGet("/receita/{id}", async (IService<Income> _incomeService, int id) =>
            {
                var income = await _incomeService.GetRegisterById(id);
                if (income != null) return Results.Ok(income);
                return Results.NotFound();
            }).WithTags("Receita").WithName("AddSpentsById");

            endpoints.MapPost("/receita", async (Income income, IService<Income> _incomeService) =>
            {
                if (income == null) TypedResults.BadRequest();
                await _incomeService.AddRegister(income);
                return Results.Created($"{income.IncomeId}", income);
            }).WithTags("Receita").WithName("AddIncomes");

            endpoints.MapPut("/receita/{id}", async (IService<Income> _incomeService, int id, Income income) =>
            {
                var updatedIncome = await _incomeService.UpdateRegister(id, income);
                if (updatedIncome != null) return Results.Ok(updatedIncome);
                return Results.NotFound();
            }).WithTags("Receita").WithName("UpdateIncomes");

            endpoints.MapDelete("/receita/{id}", async (IService<Income> _incomeService, int id) =>
            {
                var deleteResult = await _incomeService.DeleteRegister(id);
                if (deleteResult) return Results.Ok();
                return Results.NotFound();
            }).WithTags("Receita").WithName("DeleteIncome");
        }

    }
}
