using System.Collections.Concurrent;
using Data.Context.Entities;
using Logic.Repository;
using Logic.Repository.Generic.Interfaces;

namespace Logic.IUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IApplicationDbContextProcedures GetProcedures();
}

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();
    private bool _disposed;

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var typeName = typeof(TEntity).Name;

        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(typeName, _ =>
            new GenericRepository<TEntity>(context));
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public IApplicationDbContextProcedures GetProcedures()
    {
        return context.GetProcedures();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            context.Dispose();
        }
        _disposed = true;
    }
}
