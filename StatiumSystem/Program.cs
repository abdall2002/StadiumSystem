using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StatiumSystem.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("StatiumConnection")
    ?? throw new InvalidOperationException("Connection string 'StatiumConnection' not found.");

builder.Services.AddDbContext<StadiumDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add Identity with default UI
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<StadiumDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Required for Identity UI

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Required for Identity UI pages

app.Run();
