using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.Interfaces;
using System.Linq.Expressions;


namespace OnlineStore.DAL.Repositories.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        var deletedRows = await _context.SaveChangesAsync();
        return deletedRows > 0;
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filters, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(filters, cancellationToken);
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filters, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = Include(includeProperties);

        return await query.FirstOrDefaultAsync(filters, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        var updatedRows = await _context.SaveChangesAsync();
        return updatedRows > 0;
    }

    private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();
        return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }
}
