using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CRJ_Shop.Models;

public class AppUser : IdentityUser
{
    [Required]
    public string FirsName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;

    public ICollection<UserOrder> Orders { get; set; } = new List<UserOrder>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

}
