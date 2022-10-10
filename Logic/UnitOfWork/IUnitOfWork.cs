

using Data.Entities.Context;
using Logic.Repository.Generic.Interfaces;
using Logic.Repository;
using System.Collections;

namespace Logic.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task Complete();

        //IApplicationDbContextProcedures GetProcedures();

    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                        .MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        //public IApplicationDbContextProcedures GetProcedures()
        //{

        //    return _context.GetProcedures();
        //}


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
