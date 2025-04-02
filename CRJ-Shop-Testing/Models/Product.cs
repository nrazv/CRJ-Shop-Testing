using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRJ_Shop.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required Double Price { get; set; }

        [Required]
        public required List<ProductCategory> ProductCategories { get; set; } = new();
        [Required]
        public required List<ProductOrder> ProductOrders { get; set; } = new();


        [Required]
        public required string Image { get; set; }
        public int AvailableAmount { get; set; } = 100;

    }
}
