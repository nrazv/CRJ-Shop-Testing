using System.Linq.Expressions;

namespace CRJ_Shop.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(Expression<Func<T, bool>> filter);
    Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> filter);
    Task<int> SaveChanges();
    Task Add(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> values);
}