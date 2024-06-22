﻿using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.interfaces
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(Category category, CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}
