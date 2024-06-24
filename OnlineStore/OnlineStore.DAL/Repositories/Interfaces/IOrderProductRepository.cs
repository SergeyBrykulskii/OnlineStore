using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Interfaces;

public interface IOrderProductRepository : IRepository<OrderProduct>
{
    Task<IEnumerable<OrderProduct>> GetByOrderAsync(Order order, CancellationToken cancellationToken = default);
}
