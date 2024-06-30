using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
