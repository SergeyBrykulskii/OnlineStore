using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;


namespace OnlineStore.DAL.Repositories.Repositories;

public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<OrderProduct>> GetByOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        return await _context.OrderProducts.AsNoTracking().Include(c => c.Product)
                              .Where(p => p.OrderId == order.Id)
                              .ToListAsync(cancellationToken);
    }
}
