using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRJ_Shop.Models;

[Index(nameof(ProductCategory), IsUnique = true)]
public class Category
{
    [Key]
    public int Id { get; set; }
    public AvailableCategories ProductCategory { get; set; }
    public List<ProductCategory> ProductCategories { get; set; } = new();
}