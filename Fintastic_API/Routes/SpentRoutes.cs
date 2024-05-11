using Fintastic_API.Models;
using Fintastic_API.Services;
using System.Runtime.CompilerServices;

namespace Fintastic_API.Routes
{
    public static class SpentRoutes
    {
        public static void MapSpentRoutes(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapGet("/despesas", async (IService<Spent> _spentService) =>
            {
                var spentList = await _spentService.GetRegisters();
                return Results.Ok(spentList);
            }).WithTags("Despesa").WithName("GetSpents");

            endpoints.MapGet("/despesas/{id}", async (IService<Spent> _spentService, int id) =>
            {
                var spent = await _spentService.GetRegisterById(id);
                if (spent != null) return Results.Ok(spent);
                return Results.NotFound();
            }).WithTags("Despesa").WithName("GetSpentById");

            endpoints.MapPost("/despesas", async (Spent spent, IService<Spent> _spentService) =>
            {
                if (spent == null) TypedResults.BadRequest();
                await _spentService.AddRegister(spent);
                return Results.Created($"{spent.SpendingId}", spent);
            }).WithTags("Despesa").WithName("AddSpents");

            endpoints.MapPut("/despesas/{id}", async (IService<Spent> _spentService, int id, Spent spent) =>
            {
                var updatedSpent = await _spentService.UpdateRegister(id, spent);
                if (updatedSpent != null) return Results.Ok(updatedSpent);
                return Results.NotFound();
            }).WithTags("Despesa").WithName("UpdateSpent");

            endpoints.MapDelete("/despesas/{id}", async (IService<Spent> _spentService, int id) =>
            {
                var deleteResult = await _spentService.DeleteRegister(id);
                if (deleteResult) return Results.Ok();
                return Results.NotFound();
            }).WithTags("Despesa").WithName("DeleteSpents");

        }
    }
}
