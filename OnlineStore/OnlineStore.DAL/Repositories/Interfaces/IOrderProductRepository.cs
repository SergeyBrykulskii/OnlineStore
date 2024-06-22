using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories.interfaces
{
    public interface IOrderProductRepository: IRepository<OrderProduct>
    {
        Task<OrderProduct> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderProduct>> GetByOrder(Order order, CancellationToken cancellationToken = default);
    }
}
