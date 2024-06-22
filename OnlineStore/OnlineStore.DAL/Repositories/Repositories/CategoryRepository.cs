using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.repositories
{
    public class CategoryRepository : BaseRepository<Category>,ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<Category> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var query = _context.Categories.AsNoTracking().AsQueryable().Where(el => el.Id == id);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
