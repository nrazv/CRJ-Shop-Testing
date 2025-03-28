using CRJ_Shop.Data;
using CRJ_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRJ_Shop.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
       public Product Product { get; set; } 

        [BindProperty] public AvailableCategories SelectedCategory { get; set; } // Bind selected category

        public SelectList CategorySelectList { get; set; } // Populate dropdown

        public IActionResult OnGet()
        {
            // Populate the dropdown with enum values
            CategorySelectList = new SelectList(
                Enum.GetValues(typeof(AvailableCategories))
                    .Cast<AvailableCategories>()
                    .Select(c => new { Id = (int)c, Name = c.ToString() }),
                "Id",
                "Name"
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Product.Title = Product.Name;

            // Add the product to the database
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            // If a category is selected, create the ProductCategory relationship
            if (SelectedCategory != 0)
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.ProductCategory == SelectedCategory);

                if (category != null)
                {
                    var productCategory = new ProductCategory
                    {
                        ProductId = Product.Id,
                        CategoryId = category.Id
                    };


                    _context.ProductCategories.Add(productCategory);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}