using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.ApplicationDbContext;
using OnlineStore.DAL.Repositories.Interfaces;
using OnlineStore.Domain.Entities;


namespace OnlineStore.DAL.Repositories.Repositories;

public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(AppDbContext context) : base(context) { }

    public async Task<OrderProduct> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var query = _context.OrderProducts.AsNoTracking().AsQueryable().Where(el => el.Id == id);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderProduct>> GetByOrder(Order order, CancellationToken cancellationToken)
    {
        return await _context.OrderProducts.AsNoTracking().Include(c => c.Product)
                              .Where(p => p.OrderId == order.Id)
                              .ToListAsync(cancellationToken);
    }
}
