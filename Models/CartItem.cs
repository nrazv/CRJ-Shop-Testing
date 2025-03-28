
namespace CRJ_Shop.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public AppUser User { get; set; } = null!;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; }
    }
}