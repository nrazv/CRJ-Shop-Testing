using CRJ_Shop.Data;
using CRJ_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRJ_Shop.Pages;

public class IndexModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserStore<AppUser> _userStore;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext dbContext;



    public List<Product> ProductList { get; set; }
    public List<Category> Categories { get; set; }
    public string SearchQuery { get; set; }
    public int? SelectedCategoryId { get; set; }
    // Konstruktor
    public IndexModel(
            AppDbContext dbContext,
            UserManager<AppUser> userManager,
            IUserStore<AppUser> userStore,
            RoleManager<IdentityRole> roleManager)
    {
        this.dbContext = dbContext;
        _userManager = userManager;
        _userStore = userStore;
        _roleManager = roleManager;


    }

    public async Task OnGet(string searchQuery, int? selectedCategoryId)
    {
        Categories = await dbContext.Categories.ToListAsync();

        // Show prodcts based on what you search for, and what category you select
        var productsQuery = dbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            productsQuery = productsQuery.Where(p => p.Name.Contains(searchQuery));
            SearchQuery = searchQuery;
        }

        // Category filter
        if (selectedCategoryId.HasValue)
        {
            var selectedCategory = await dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == selectedCategoryId);

            if (selectedCategory != null)
            {
                productsQuery = productsQuery.Where(p => p.ProductCategories
                    .Any(pc => pc.Category.ProductCategory == selectedCategory.ProductCategory));
                SelectedCategoryId = selectedCategoryId; // Persist the selected category ID
            }
        }

        // Fetch the filtered product list
        ProductList = await productsQuery.ToListAsync();
    }



}
