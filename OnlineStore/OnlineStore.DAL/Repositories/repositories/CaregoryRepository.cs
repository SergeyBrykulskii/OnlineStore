using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.DAL.Repositories.repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Category entity, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Category entity, CancellationToken cancellationToken)
        {
            _context.Categories.Remove(entity);
            var deletedRows = await _context.SaveChangesAsync(cancellationToken);
            return deletedRows > 0;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = _context.Categories.AsQueryable();

            query = query.Where(el => el.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Category entity, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(entity.Id);

            if (category is null)
            {
                return false;
            }
            updateCategory(category, entity);

            var updatedRows = await _context.SaveChangesAsync(cancellationToken);
            return updatedRows > 0;
        }

        private void updateCategory(Category oldCategory, Category newCategory)
        {
            oldCategory.Id = newCategory.Id;
            oldCategory.Name = newCategory.Name;
        }
    }
}
