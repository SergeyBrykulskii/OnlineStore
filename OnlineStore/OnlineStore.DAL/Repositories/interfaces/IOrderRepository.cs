using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories.interfaces
{
    internal interface IOrderRepository:IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllByUser(User user,CancellationToken cancellationToken);
    }
}
