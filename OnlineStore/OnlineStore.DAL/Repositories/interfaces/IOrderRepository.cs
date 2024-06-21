using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.interfaces
{
    internal interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllByUser(User user, CancellationToken cancellationToken);
        Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
