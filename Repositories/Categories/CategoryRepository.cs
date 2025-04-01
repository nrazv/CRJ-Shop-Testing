using CRJ_Shop.Data;
using CRJ_Shop.Models;

namespace CRJ_Shop.Repositories.Categories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private AppDbContext _appDbContext;

    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Save()
    {
        _appDbContext.SaveChanges();
    }
}
