using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRJ_Shop.Data;
using CRJ_Shop.Models;

namespace CRJ_Shop.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly CRJ_Shop.Data.AppDbContext _context;

        public EditModel(CRJ_Shop.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public AvailableCategories SelectedCategory { get; set; }
        public SelectList CategorySelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            Product = product;

            // populate dropdown with enum values
            CategorySelectList = new SelectList(
                Enum.GetValues(typeof(AvailableCategories))
                    .Cast<AvailableCategories>()
                    .Select(c => new { Id = (int)c, Name = c.ToString() }),
                "Id",
                "Name"
            );

            if (Product.ProductCategories != null && Product.ProductCategories.Any())
            {
                SelectedCategory = Product.ProductCategories
                    .FirstOrDefault()?.Category?.ProductCategory ?? 0;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // repopulate the dropdown if validation fails
                CategorySelectList = new SelectList(
                    Enum.GetValues(typeof(AvailableCategories))
                        .Cast<AvailableCategories>()
                        .Select(c => new { Id = (int)c, Name = c.ToString() }),
                    "Id",
                    "Name"
                );
                return Page();
            }


            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Attach(Product).State = EntityState.Modified;

                if (SelectedCategory != 0)
                {
                    //get the current product ábd its categories
                    var existingProduct = await _context.Products
                        .Include(p => p.ProductCategories)
                        .FirstOrDefaultAsync(p => p.Id == Product.Id);

                    if (existingProduct != null)
                    {
                        // remove existing productcategory relationships
                        _context.ProductCategories.RemoveRange(existingProduct.ProductCategories);

                        var category = await _context.Categories
                            .FirstOrDefaultAsync(c => c.ProductCategory == SelectedCategory);

                        var productCategory = new ProductCategory
                        {
                            ProductId = Product.Id,
                            CategoryId = category.Id
                        };

                        _context.ProductCategories.Add(productCategory);
                    }
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();

                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}