using CRJ_Shop.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CRJ_Shop.Models;
using CRJ_Shop.Services;
using Microsoft.EntityFrameworkCore;
using CRJ_Shop.DbInitializer;
using CRJ_Shop.Repositories.Products;
using CRJ_Shop.Repositories;
using CRJ_Shop.Services.Products;
using CRJ_Shop.Services.Categories;
using CRJ_Shop.Repositories.Categories;

var builder = WebApplication.CreateBuilder(args);


// configure Identity to use IdentityDbContext
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
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

    //var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
    //await seedService.SeedCategories();
    //await seedService.SeedData();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
