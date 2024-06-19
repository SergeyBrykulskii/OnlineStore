using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.interfaces
{
    internal interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllByUser(User user, CancellationToken cancellationToken);
    }
}
