using DataLayer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region DATA BASE CONTEXT UPCODE

builder.Services.AddDbContext<UPCodeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UPCodeConnection"));
});

#endregion

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
