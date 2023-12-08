using System.Linq.Expressions;

namespace Api.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> Query { get; }

    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task RemoveAsync(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
}
