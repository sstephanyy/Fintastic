using Fintastic_API.Models;
using Fintastic_API.Service;
using Fintastic_API.Services;

namespace Fintastic_API.Routes
{
    public static class BalanceRoute
    {
        public static void MapBalanceRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/total-despesa", async (IBalance<Balance> _balanceService) =>
            {
                var spentList = await _balanceService.CalculateTotalSpent();
                return Results.Ok(spentList);
            }).WithTags("Total").WithName("GetBalanceSpent");

            endpoints.MapGet("/total-receita", async (IBalance<Balance> _balanceService) =>
            {
                var totalIncome = await _balanceService.CalculateTotalIncome();
                return Results.Ok(totalIncome);
            }).WithTags("Total").WithName("GetBalanceIncome");

            endpoints.MapGet("/saldo", async (IBalance<Balance> _balanceService) =>
            {
                var total = await _balanceService.CalculateTotal();
                return Results.Ok(total);
            }).WithTags("Total").WithName("GetTotal");
        }
    }
}
