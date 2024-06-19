using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product entity, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Product entity, CancellationToken cancellationToken)
        {
            _context.Products.Remove(entity);
            var deletedRows = await _context.SaveChangesAsync(cancellationToken);
            return deletedRows > 0;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync();
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
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Product entity, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(entity.Id);

            if (product is null)
            {
                return false;
            }
            updateProduct(product, entity);

            var updatedRows = await _context.SaveChangesAsync(cancellationToken);
            return updatedRows > 0;
        }

        private void updateProduct(Product oldProduct, Product newProduct)
        {
            oldProduct.Id = newProduct.Id;
            oldProduct.Name = newProduct.Name;
            oldProduct.Description = newProduct.Description;
            oldProduct.Price = newProduct.Price;
            oldProduct.CategoryId = newProduct.CategoryId;

        }
    }
}
