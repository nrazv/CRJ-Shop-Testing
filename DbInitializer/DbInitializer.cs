using CRJ_Shop.Data;
using CRJ_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRJ_Shop.DbInitializer;

public class DbInitializer
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _dbContext;

    public DbInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public async Task InitializeDb()
    {
        try
        {
            if (_dbContext.Database.GetPendingMigrations().Count() > 0)
            {
                _dbContext.Database.Migrate();
            }

        }
        catch (Exception e)
        {

        }

        if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
        {
            var customerRole = new IdentityRole("Customer");
            var adminRole = new IdentityRole("Admin");
            await _roleManager.CreateAsync(adminRole);
            await _roleManager.CreateAsync(customerRole);
        }

        var adminUsers = _userManager.GetUsersInRoleAsync("Admin").Result;

        if (adminUsers.Count == 0)
        {

            var adminEmail = "admin@shop.com";

            var response = await _userManager.CreateAsync(new AppUser
            {
                FirsName = "Admin",
                LastName = "Admin",
                Address = "crjshop.com",
                Email = adminEmail,
                UserName = adminEmail

            }, "Password123!");


            if (response.Succeeded)
            {
                var adminUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == adminEmail);
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        var joeEmail = "joe.thompson@shop.com";
        var customerUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == joeEmail);

        if (customerUser is null)
        {
            var response = await _userManager.CreateAsync(new AppUser
            {
                FirsName = "Joe",
                LastName = "Thompson",
                Address = "Toronto 123 Maple Street",
                Email = joeEmail,
                UserName = joeEmail

            }, "Password123!");

            if (response.Succeeded)
            {
                var adminUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == joeEmail);
                await _userManager.AddToRoleAsync(adminUser, "Customer");
            }
        }

        return;
    }
}
