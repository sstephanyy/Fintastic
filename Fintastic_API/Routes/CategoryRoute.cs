using Fintastic_API.Models;
using Fintastic_API.Services;

namespace Fintastic_API.Routes
{
    public static class CategoryRoute
    {
        public static void MapCategoryRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/categoria", async (IService<Category> _categoryService) =>
            {
                var categoryList = await _categoryService.GetRegisters();
                return Results.Ok(categoryList);
            }).WithTags("Categoria").WithName("GetCategories");

            endpoints.MapGet("/categoria/{id}", async (IService<Category> _categoryService, int id) =>
            {
                var category = await _categoryService.GetRegisterById(id);
                if (category != null) return Results.Ok(category);
                return Results.NotFound();
            }).WithTags("Categoria").WithName("AddCategoryById");

            endpoints.MapPost("/categoria", async (Category category, IService<Category> categoryService) =>
            {
                if (category == null) TypedResults.BadRequest();
                await categoryService.AddRegister(category);
                return Results.Created($"{category.CategoryId}", category);
            }).WithTags("Categoria").WithName("AddCategories");

            endpoints.MapPut("/categoria/{id}", async (IService<Category> _categoryService, int id, Category category) =>
            {
                var updatedCategory = await _categoryService.UpdateRegister(id, category);
                if (updatedCategory != null) return Results.Ok(updatedCategory);
                return Results.NotFound();
            }).WithTags("Categoria").WithName("UpdateCategory");

            endpoints.MapDelete("/categoria/{id}", async (IService<Category> _categoryService, int id) =>
            {
                var deleteResult = await _categoryService.DeleteRegister(id);
                if (deleteResult) return Results.Ok();
                return Results.NotFound();
            }).WithTags("Categoria").WithName("DeleteCategory");
        }
    }
}