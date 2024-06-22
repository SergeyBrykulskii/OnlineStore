using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}
