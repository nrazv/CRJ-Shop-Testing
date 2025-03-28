using System.Linq.Expressions;

namespace CRJ_Shop.Repositories;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> values);
}