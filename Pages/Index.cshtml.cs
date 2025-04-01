using CRJ_Shop.Data;
using CRJ_Shop.Models;
using CRJ_Shop.Repositories.Categories;
using CRJ_Shop.Services.Categories;
using CRJ_Shop.Services.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRJ_Shop.Pages;

public class IndexModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserStore<AppUser> _userStore;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public IndexModel(UserManager<AppUser> userManager, IUserStore<AppUser> userStore, RoleManager<IdentityRole> roleManager, IProductService productService, ICategoryService categoryService)
    {
        _userManager = userManager;
        _userStore = userStore;
        _roleManager = roleManager;
        _productService = productService;
        _categoryService = categoryService;
    }

    public List<Product> ProductList { get; set; }
    public List<Category> Categories { get; set; }
    public string SearchQuery { get; set; }
    public int? SelectedCategoryId { get; set; }


    public async Task OnGet(string searchQuery, int? selectedCategoryId)
    {
        Categories = await _categoryService.GetAll();
        ProductList = await _productService.GetAll();


        if (!string.IsNullOrEmpty(searchQuery))
        {
            ProductList = await _productService.GetWhere(p => p.Name.Contains(searchQuery));
        }

        if (selectedCategoryId.HasValue)
        {
            var category = await _categoryService.GetById(selectedCategoryId.Value);

            if (category != null)
            {
                ProductList = await _productService.GetWhere(p => p.ProductCategories.Any(c => c.CategoryId == category.Id));
            }
        }

    }

}
