using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetAllByUser(User user, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.AsNoTracking().Include(o => o.User)
                             .Where(p => p.UserId == user.Id)
                             .ToListAsync();
    }

    public async Task<Order> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var query = _context.Orders.AsNoTracking().Include(o => o.OrderProducts).AsQueryable().Where(el => el.Id == id);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
