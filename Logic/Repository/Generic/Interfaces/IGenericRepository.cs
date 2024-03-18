using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Repository.Generic.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        #region Asyncronous Methods
        Task<T> AddAsync(T t);
        Task<int> CountAsync();
        Task<int> DeleteAsync(T entity);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> GetAllAsync();
        Task<IQueryable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(string id);
        Task<int> SaveAsync();
        Task<T> UpdateAsync(T t, object key);
        void Dispose();
        #endregion
    }
}
