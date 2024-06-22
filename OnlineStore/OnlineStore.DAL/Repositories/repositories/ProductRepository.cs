using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Repositories
{
    public class ProductRepository : BaseRepository<Product>,IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(Category category, CancellationToken cancellationToken = default)
        {
            return await _context.Products.AsNoTracking().Include(c => c.Category)
                                  .Where(p => p.CategoryId == category.Id)
                                  .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var query = _context.Products.AsNoTracking().AsQueryable().Where(el => el.Id == id);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
