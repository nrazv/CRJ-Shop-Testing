namespace CRJ_Shop.Models;

public class UserOrder
{

    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public int OrderNumber { get; set; } = DateTime.Now.Second;

    public List<ProductOrder> Products { get; set; } = new();

    public double TotalPrice => Products.Sum(item => item.Product.Price);

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

}
