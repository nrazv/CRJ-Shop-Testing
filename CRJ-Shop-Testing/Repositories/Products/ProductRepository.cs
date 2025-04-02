using CRJ_Shop.Data;
using CRJ_Shop.Models;
using CRJ_Shop.Repositories.Products;

namespace CRJ_Shop.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private AppDbContext _appDbContext;
    public ProductRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
    }

    public void Save()
    {
        _appDbContext.SaveChanges();
    }

    public void Update(Product product)
    {
        _appDbContext.Update(product);
    }
}
