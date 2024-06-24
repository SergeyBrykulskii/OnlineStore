using OnlineStore.Domain.Interfaces.Entities;
using System.Linq.Expressions;

namespace OnlineStore.DAL.Repositories.Interfaces;

public interface IRepository<T> where T : class, IEntity
{
    public Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    public Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    public Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    public Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
    public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filters, CancellationToken cancellationToken = default);
    public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filters, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties);
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
