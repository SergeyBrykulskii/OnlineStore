using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Interfaces;

public interface IOrderProductRepository : IRepository<OrderProduct>
{
    Task<OrderProduct> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderProduct>> GetByOrder(Order order, CancellationToken cancellationToken = default);
}
