using OnlineStore.Domain.Interfaces.Entities;
using System.Linq.Expressions;

namespace OnlineStore.DAL.Repositories.interfaces
{
    public abstract class IRepository<T> where T : class
    {
        abstract public Task CreateAsync(T entity, CancellationToken cancellationToken);
        abstract public bool Update(T entity, CancellationToken cancellationToken);
        abstract public bool Delete(T entity, CancellationToken cancellationToken);
        abstract public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filters, CancellationToken cancellationToken);
        abstract public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filters, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
        abstract public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        abstract public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
