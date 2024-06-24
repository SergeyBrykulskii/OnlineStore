using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}
