
using System.Linq.Expressions;

namespace CRJ_Shop.Services;

public interface Service<T>
{
    Task<List<T>> GetAll();
    Task<T> GetById(int Id);
    Task<bool> AddNew(T entity);
    Task<T> Update(T entity);
    Task<bool> Delete(T entity);
    Task<bool> DeleteAll(IEnumerable<T> values);
    Task<List<T>> GetWhere(Expression<Func<T, bool>> filter);
}
