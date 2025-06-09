using System.Linq.Expressions;

namespace PersonManagement.DAL.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);
    Task SaveChangesAsync();
}