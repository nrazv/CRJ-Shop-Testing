using CRJ_Shop.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CRJ_Shop.Models;
using CRJ_Shop.Services;
using Microsoft.EntityFrameworkCore;
using CRJ_Shop.DbInitializer;

var builder = WebApplication.CreateBuilder(args);


// configure Identity to use IdentityDbContext
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<SeedService>();
builder.Services.AddScoped<DbInitializer>();

var app = builder.Build();

// SEED DATA
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await dbInitializer.InitializeDb();
}

using (var scope = app.Services.CreateScope())
{

    var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
    await seedService.SeedCategories();
    await seedService.SeedData();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
