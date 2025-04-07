using System.Linq.Expressions;
using CRJ_Shop.Data;
using Microsoft.EntityFrameworkCore;

namespace CRJ_Shop.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _appDbContext;
    private DbSet<T> dbSet;
    public Repository(AppDbContext appContext)
    {
        _appDbContext = appContext;
        this.dbSet = _appDbContext.Set<T>();
    }


    public async Task Add(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> values)
    {
        dbSet.RemoveRange(values);
    }

    public async Task<T> Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        IQueryable<T> query = dbSet;
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return await query.ToListAsync();
    }

    public async Task<int> SaveChanges()
    {
        return await _appDbContext.SaveChangesAsync();
    }
}