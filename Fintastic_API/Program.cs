using Fintastic_API.Data;
using Fintastic_API.Endpoints;
using Fintastic_API.Models;
using Fintastic_API.Routes;
using Fintastic_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IService<Spent>, SpentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ==================> CATEGORY CRUD <=====================
app.MapGet("/categoria", async (DataContext context) =>
    await context.Categories.ToListAsync()
).WithTags("Categoria");


app.MapGet("/categoria/{id}", async (int id, DataContext context) =>
{
    var category = await context.Categories.FindAsync(id);
    if (category == null)
    {
        return Results.NotFound(new { message = "Categoria não encontrada." });
    }
    return Results.Ok(category);
}).WithTags("Categoria");


app.MapPost("/categoria", async (Category newCategory, DataContext context) =>
{
    context.Categories.Add(newCategory);
    await context.SaveChangesAsync();
    return Results.Created($"/categoria/{newCategory.CategoryId}", newCategory);
}).WithTags("Categoria");

app.MapPut("/categoria/{id}", async (int id, DataContext context, Category updatedCategory) =>
{
    var categoryBanco = await context.Categories.AsNoTracking<Category>().FirstOrDefaultAsync(f => f.CategoryId == id); 
    if (categoryBanco == null) return Results.NotFound();

    context.Categories.Update(updatedCategory);
    var result = await context.SaveChangesAsync();

    return result > 0
        ? Results.NoContent()
        : Results.BadRequest(new { message = "Não foi possível atualizar o registro." });
}).WithTags("Categoria");


app.MapDelete("/categoria/{id}", async (int id, DataContext context) =>
{
    var category = await context.Categories.FindAsync(id);
    if (category == null) return Results.NotFound();

    context.Categories.Remove(category);
    var result = await context.SaveChangesAsync();

    return result > 0
        ? Results.NoContent()
        : Results.BadRequest("Não foi possível deletar o registro.");
}).WithTags("Categoria");


app.MapIncomeRoutes();
app.MapSpentRoutes();

app.Run();


