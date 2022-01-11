using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BrandBySoundex.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BrandBySoundexContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BrandBySoundexContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//Marcas de ejemplo:
//Braum
//Braun
//Broom
//Broon
//Brown
//Browne
//casa
//case
//cosa
//kase
//keis
//keys
//queis
