using CRJ_Shop.Data;
using CRJ_Shop.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRJ_Shop.Pages.Products
{
    public class Index : PageModel
    {
        private readonly AppDbContext _database;

        public Index(AppDbContext database)
        {
            _database = database;
        }

        public List<Product> ProductList { get; set; }
        public List<Category> Categories { get; set; }
        public string SearchQuery { get; set; }
        public int? SelectedCategoryId { get; set; }

        public async Task OnGet(string searchQuery, int? selectedCategoryId)
        {
            Categories = await _database.Categories.ToListAsync();

            // Show prodcts based on what you search for, and what category you select
            var productsQuery = _database.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchQuery));
                SearchQuery = searchQuery; 
            }

            // Category filter
            if (selectedCategoryId.HasValue)
            {
                var selectedCategory = await _database.Categories
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
}