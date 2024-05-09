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



// ==================> INCOME CRUD <=====================

app.MapGet("/receita", async (DataContext context) =>
{
    var incomes = await context.Incomes.Include(i => i.Category).ToListAsync();
    return Results.Ok(incomes);
}).WithTags("Receita");


app.MapGet("/receita/{id}", async (int id, DataContext context) =>
{
    var income = await context.Incomes.Include(i => i.Category).FirstOrDefaultAsync(i => i.IncomeId == id);
    if (income == null)
    {
        return Results.NotFound(new { message = "Receita não encontrada." });
    }
    return Results.Ok(income);
}).WithTags("Receita");

app.MapPost("/receita", IncomesEndpointsV1.AddIncome).WithTags("Receita");


app.MapPut("/receita/{id}", async (int id, DataContext context, Income income) =>
{
    var incomeBanco = await context.Incomes.AsNoTracking<Income>().FirstOrDefaultAsync(f=>f.IncomeId == id);
    if(incomeBanco == null) return Results.NotFound();

    context.Incomes.Update(income);
    var result = await context.SaveChangesAsync();

    return result > 0
        ? Results.NoContent()
        : Results.BadRequest("Não foi possível atualizar o registro.");
}).WithTags("Receita");

app.MapDelete("/receita/{id}", async (int id, DataContext context) =>
{
    var income = await context.Incomes.FindAsync(id);
    if (income == null) return Results.NotFound();

    context.Incomes.Remove(income);
    var result = await context.SaveChangesAsync();

    return result > 0
        ? Results.NoContent()
        : Results.BadRequest("Não foi possível deletar o registro.");
}).WithTags("Receita");

app.MapSpentRoutes();

app.Run();


