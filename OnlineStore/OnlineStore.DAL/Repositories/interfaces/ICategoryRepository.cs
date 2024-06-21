using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
