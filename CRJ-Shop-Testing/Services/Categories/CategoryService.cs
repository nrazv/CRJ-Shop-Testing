using CRJ_Shop.Models;
using CRJ_Shop.Repositories.Categories;
using System.Linq.Expressions;

namespace CRJ_Shop.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository repository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        this.repository = categoryRepository;
    }

    public bool Delete(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAll(IEnumerable<Category> values)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetAll()
    {
        var list = await repository.GetAll();
        return list.ToList();
    }

    public async Task<Category> GetById(int Id)
    {
        return await repository.Get(c => c.Id == Id);
    }

    public async Task<List<Category>> GetWhere(Expression<Func<Category, bool>> filter)
    {
        var list = await repository.GetWhere(filter);
        return list.ToList();
    }

    public Task<Category> Save(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<Category> Update(Category entity)
    {
        throw new NotImplementedException();
    }

    Task<bool> Service<Category>.Delete(Category entity)
    {
        throw new NotImplementedException();
    }
}
