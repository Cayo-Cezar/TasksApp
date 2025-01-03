using API.Controllers;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();
builder.AddDatabase();
builder.AddValidations();
builder.AddMapper();
builder.AddSwaggerDocs();
builder.AddJwtAuth();   
builder.AddInjections();
builder.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.WorkspacesRoute();

app.Run();
