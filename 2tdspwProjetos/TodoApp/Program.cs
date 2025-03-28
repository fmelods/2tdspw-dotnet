using Microsoft.EntityFrameworkCore;
using TodoApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>(optionsBuilder =>
    optionsBuilder.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/todoitems", async (TodoDb db) =>  await  db.Todos.ToListAsync());

app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos.Where(todo => todo.IsComplete).ToListAsync());

app.MapGet("/todoitems/{id:int}/", async (int id,TodoDb db) => await
    db.Todos.FindAsync(id) is {  } todo 
        ? Results.Ok(todo) 
        : Results.NotFound() 
);

app.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
{
    db.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id:int}", async (int id, Todo todo, TodoDb db) =>
{
    var td = await db.Todos.FindAsync(id);
    
    if (td is null)
        return Results.NotFound();
    
    td.Name = todo.Name;
    td.IsComplete = todo.IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id:int}", async (int id, TodoDb db) =>
{
    var td = await db.Todos.FindAsync(id);
    if (td is null)
        return Results.NotFound();

    db.Remove(td);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();