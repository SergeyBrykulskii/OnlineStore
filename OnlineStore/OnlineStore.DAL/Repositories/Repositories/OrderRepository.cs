using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.Repositories.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetAllByUserAsync(User user, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.AsNoTracking().Include(o => o.User)
                             .Where(p => p.UserId == user.Id)
                             .ToListAsync(cancellationToken);
    }
}
