using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories.Interfaces
{
    public interface IOrderProductRepository
    {
        Task<OrderProduct> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<OrderProduct>> GetByOrder(Order order, CancellationToken cancellationToken);
    }
}
