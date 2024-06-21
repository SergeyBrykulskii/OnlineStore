using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories.repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public override async Task CreateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public override async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Remove(entity);
            var deletedRows = await _context.SaveChangesAsync();
            return deletedRows > 0;
        }

        public override async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filters, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                                     .FirstOrDefaultAsync(filters, cancellationToken);
        }

        public override async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filters, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);

            return await query.FirstOrDefaultAsync(filters, cancellationToken);
        }

        public override async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public override async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
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
}
