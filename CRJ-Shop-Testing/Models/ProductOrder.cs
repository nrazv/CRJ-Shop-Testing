using System.ComponentModel.DataAnnotations;

namespace CRJ_Shop.Models
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public int UserOrderId { get; set; }
        public UserOrder UserOrder { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
