using CRJ_Shop.Models;

namespace CRJ_Shop.Repositories.Products;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product product);
    void Save();
}
