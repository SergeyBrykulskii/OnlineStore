using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Category category, CancellationToken cancellationToken = default)
    {
        return await _context.Products.AsNoTracking().Include(c => c.Category)
                              .Where(p => p.CategoryId == category.Id)
                              .ToListAsync(cancellationToken);
    }
}
