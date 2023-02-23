using Core.Servises;
using Core.Servises.Interfaces;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region DATA BASE CONTEXT UPCODE

builder.Services.AddDbContext<UPCodeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("UPCodeConnection"));
});

#endregion


#region IoC

builder.Services.AddTransient<IUserService, UserService>();

#endregion

builder.Services.AddControllersWithViews();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); ;

app.Run();
