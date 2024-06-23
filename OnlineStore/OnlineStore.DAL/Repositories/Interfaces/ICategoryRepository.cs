﻿using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category> GetByIdAsync(long id, CancellationToken cancellationToken = default);
}
