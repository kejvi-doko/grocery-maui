using Microsoft.EntityFrameworkCore;
using MyGrocery.WebApi.Data;
using MyGrocery.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyGrocery.WebApi.Data.AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

var app = builder.Build();


app.MapGet("api/grocery", async (AppDbContext context) =>
{
    var items = await context.Groceries.ToListAsync();
    return Results.Ok(items);
});

app.MapPost("api/grocery", async (AppDbContext context, Grocery grocery) =>
{
    await context.AddAsync(grocery);
    await context.SaveChangesAsync();
    return Results.Created($"/api/grocery/{grocery.Id}", grocery);
});


app.MapPut("api/grocery/{id}", async (AppDbContext context,int id, Grocery grocery) =>
{
    var groceryMayBe = await context.Groceries.FirstOrDefaultAsync(g => g.Id == id);
    if (groceryMayBe == null)
        return Results.NotFound();

    groceryMayBe.Name = grocery.Name;

    await context.SaveChangesAsync();

    return Results.NoContent();
});


app.MapDelete("api/grocery/{id}", async (AppDbContext context, int id) =>
{
    var groceryMayBe = await context.Groceries.FirstOrDefaultAsync(g => g.Id == id);
    if (groceryMayBe == null)
        return Results.NotFound();

    context.Groceries.Remove(groceryMayBe);

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

