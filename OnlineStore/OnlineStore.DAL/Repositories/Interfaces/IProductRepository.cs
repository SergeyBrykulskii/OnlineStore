using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryAsync(Category category, CancellationToken cancellationToken = default);
}
