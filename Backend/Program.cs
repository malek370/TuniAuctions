using backend.DataContext;
using backend.DTOs;
using backend.Extensions;
using backend.Services.AccountService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//add services from extensions
builder.Services.AddServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// adding the APIs from Extensions
app.AddMinimalApis();

//add controllers
//app.MapControllers();
app.Run();


