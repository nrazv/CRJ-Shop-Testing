using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CRJ_Shop.Data;
using CRJ_Shop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CRJ_Shop.Pages
{
    public class CartModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CartModel(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public AppUser? AppUser { get; set; }
        public List<CartItem> CartItem { get; set; }
        public bool SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User is null)
            {
                return RedirectToPage("/Account/Login");
            }

            CartItem = await _context.CartItems
                .Include(c => c.Product)
                .ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveFromCart(int itemId)
        {
            var cartItem = await _context.CartItems.FindAsync(itemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddToCart(int productId)
        {
            AppUser = await _userManager.GetUserAsync(User);

            if (AppUser is null)
            {
                return RedirectToPage("/Account/Login");
            }

            var cartItem = await _context.CartItems.FirstOrDefaultAsync(m => m.ProductId == productId);
            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);


            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    ProductId = productId,
                    Quantity = 1,
                    Product = product,
                    User = AppUser,
                    UserId = AppUser.Id
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            TempData["AddedProductId"] = productId;
            await _context.SaveChangesAsync();
            return RedirectToPage("/Cart");
        }
    }
}