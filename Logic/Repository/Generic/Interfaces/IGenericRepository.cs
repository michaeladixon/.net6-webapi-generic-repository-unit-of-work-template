using System.Linq.Expressions;

namespace Logic.Repository.Generic.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> Query();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}
