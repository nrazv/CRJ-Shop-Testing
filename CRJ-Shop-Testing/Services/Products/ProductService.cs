
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

    public Task<bool> AddNew(Product entity)
    {

        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Product entity)

    {
        _repository.Delete(entity);
        var affectedRows = await _repository.SaveChanges();
        return affectedRows >= 1;
    }

    public async Task<bool> DeleteAll(IEnumerable<Product> values)
    {
        _repository.DeleteRange(values);
        var affectedRows = await _repository.SaveChanges();
        return affectedRows >= 1;
    }

    public async Task<List<Product>> GetAll()
    {
        var list = await _repository.GetAll() ?? new List<Product>();

        return list.ToList();
    }

    public async Task<Product> GetById(int Id)
    {
        return await _repository.Get(p => p.Id == Id);
    }

    public async Task<List<Product>> GetWhere(Expression<Func<Product, bool>> filter)
    {
        var list = await _repository.GetWhere(filter);
        return list.ToList();
    }

    public Task<Product> Update(Product entity)
    {
        throw new NotImplementedException();
    }

}
