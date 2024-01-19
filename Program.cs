using Microsoft.EntityFrameworkCore;
using WebApplication2.dal;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add your Entity Framework Core DbContext configuration

builder.Services.AddDbContext<LivreDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);



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


app.MapControllerRoute(
    name: "Livre",
    pattern: "{controller=LivreController}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "LivreCreate",
    pattern: "{controller=LivreController}/{action=Create}/{id?}");

app.MapControllerRoute(
    name: "LivresEmpruntes",
    pattern: "{controller=LivreController}/{action=LivresEmpruntes}/{id?}");

app.MapControllerRoute(
    name: "ABONNE",
    pattern: "{controller=LivreController}/{action=index2}/{id?}");
    
app.MapControllerRoute(
    name: "EditEmprunt",
    pattern: "{controller=EmpruntController}/{action=EditEmprunt}/{id?}");
app.Run();
