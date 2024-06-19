using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories.interfaces
{
    public interface IRepository <T> where T : IEntity
    {
        Task CreateAsync (T entity, CancellationToken cancellationToken);
        Task<bool> UpdateAsync (T entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync (T entity, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
