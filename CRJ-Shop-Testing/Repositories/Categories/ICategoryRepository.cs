using CRJ_Shop.Models;

namespace CRJ_Shop.Repositories.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    void Save();
}
