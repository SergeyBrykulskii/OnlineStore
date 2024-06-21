using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.repositories
{
    public class ProductRepository : BaseRepository<Product>,IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByCategory(Category category, CancellationToken cancellationToken)
        {
            return await _context.Products.Include(c => c.Category)
                                  .Where(p => p.CategoryId == category.Id)
                                  .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable().Where(el => el.Id == id);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
