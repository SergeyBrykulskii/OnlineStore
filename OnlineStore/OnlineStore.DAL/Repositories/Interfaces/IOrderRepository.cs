using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetAllByUser(User user, CancellationToken cancellationToken = default);
    Task<Order> GetByIdAsync(long id, CancellationToken cancellationToken = default);
}
