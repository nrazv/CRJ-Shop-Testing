using System.Linq.Expressions;
using CRJ_Shop.Data;
using Microsoft.EntityFrameworkCore;

namespace CRJ_Shop.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _appDbContext;
    private DbSet<T> dbSet;
    public Repository(AppDbContext appContext)
    {
        _appDbContext = appContext;
        this.dbSet = _appDbContext.Set<T>();
    }


    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> values)
    {
        dbSet.RemoveRange(values);
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }
}