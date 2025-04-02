
using CRJ_Shop.Models;
using CRJ_Shop.Repositories.Products;
using System.Linq.Expressions;

namespace CRJ_Shop.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> Delete(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAll(IEnumerable<Product> values)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetAll()
    {
        var list = await _repository.GetAll();

        return list.ToList();
    }

    public Task<Product> GetById(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetWhere(Expression<Func<Product, bool>> filter)
    {
        var list = await _repository.GetWhere(filter);
        return list.ToList();
    }

    public Task<Product> Save(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task<Product> Update(Product entity)
    {
        throw new NotImplementedException();
    }

}
