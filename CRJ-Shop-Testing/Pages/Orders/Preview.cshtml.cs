using CRJ_Shop.Data;
using CRJ_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRJ_Shop.Pages.Order;


public class PreviewModel : PageModel
{

    private readonly AppDbContext _appDbContext;
    private readonly UserManager<AppUser> _userManager;

    public PreviewModel(AppDbContext appDbContext, UserManager<AppUser> userManager)
    {
        _appDbContext = appDbContext;
        _userManager = userManager;
    }

    public AppUser? AppUser { get; set; }
    public List<CartItem> CartItems { get; set; }


    public async Task<IActionResult> OnGetAsync()
    {
        AppUser = await _userManager.GetUserAsync(User); // Get logged-in user
        if (AppUser == null)
        {
            return RedirectToPage("/Account/Login");
        }

        CartItems = await _appDbContext.CartItems
            .Include(c => c.Product)
            .ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        AppUser = await _userManager.GetUserAsync(User);
        CartItems = await _appDbContext.CartItems.Include(c => c.Product).ToListAsync();
        var order = createOrder(AppUser);
        var orderProducts = getProducts(CartItems);

        order.Products = orderProducts;
        await _appDbContext.Orders.AddAsync(order);
        var affectedRows = await _appDbContext.SaveChangesAsync();



        if (affectedRows >= 1)
        {
            _appDbContext.CartItems.RemoveRange(CartItems);
            await _appDbContext.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        return Page();
    }


    private UserOrder createOrder(AppUser user)
    {
        UserOrder newOrder = new()
        {
            User = user,
            UserId = user.Id,
        };

        return newOrder;
    }

    private List<ProductOrder> getProducts(List<CartItem> cartItems)
    {
        var products = new List<ProductOrder>();

        cartItems.ForEach(item =>
        {
            ProductOrder newProduct = new();
            newProduct.Product = item.Product;
            newProduct.ProductId = item.ProductId;
            newProduct.Quantity = item.Quantity;

            products.Add(newProduct);

        });

        return products;
    }
}
