using Fintastic_API.Models;
using Fintastic_API.Services;
using System.Runtime.CompilerServices;

namespace Fintastic_API.Routes
{
    public static class SpentRoutes
    {
        public static void MapSpentRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/spents", async (Spent spent, IService<Spent> _spentService) =>
            {
                if (spent == null) TypedResults.BadRequest();
                await _spentService.AddRegister(spent);
                return Results.Created($"{spent.SpendingId}", spent);
            }).WithName("AddSpents");

            endpoints.MapGet("/spents", async (IService<Spent> _spentService) =>
            {
                var spentList = await _spentService.GetRegisters();
                return Results.Ok(spentList);
            }).WithName("GetSpents");

            endpoints.MapGet("/spents/{id}", async (IService<Spent> _spentService, int id) =>
            {
                var spent = await _spentService.GetRegisterById(id);
                if (spent != null) return Results.Ok(spent);
                return Results.NotFound();
            }).WithName("GetSpentById");

            endpoints.MapPut("/spents/{id}", async (IService<Spent> _spentService, int id, Spent spent) =>
            {
                var updatedSpent = await _spentService.UpdateRegister(id, spent);
                if (updatedSpent != null) return Results.Ok(updatedSpent);
                return Results.NotFound();
            }).WithName("UpdateSpent");

            endpoints.MapDelete("/spents/{id}", async (IService<Spent> _spentService, int id) =>
            {
                var deleteResult = await _spentService.DeleteRegister(id);
                if (deleteResult) return Results.Ok();
                return Results.NotFound();
            }).WithName("DeleteSpents");

        }
    }
}

}
