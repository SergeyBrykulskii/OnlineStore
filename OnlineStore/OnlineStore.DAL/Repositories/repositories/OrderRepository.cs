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
    internal class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Order entity, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Order entity, CancellationToken cancellationToken)
        {
            _context.Orders.Remove(entity);
            var deletedRows = await _context.SaveChangesAsync(cancellationToken);
            return deletedRows > 0;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllByUser(User user, CancellationToken cancellationToken)
        {
            return await _context.Orders.Include(o => o.User)
                                 .Where(p => p.UserId == user.Id)
                                 .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = _context.Orders.Include(o => o.OrderProducts).AsQueryable();

            query = query.Where(el => el.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Order entity, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(entity.Id);

            if (order is null)
            {
                return false;
            }
            updateOrder(order, entity);

            var updatedRows = await _context.SaveChangesAsync(cancellationToken);
            return updatedRows > 0;
        }

        private void updateOrder(Order oldOrder, Order newOrder)
        {
            oldOrder.Id = newOrder.Id;
            oldOrder.CreatedAt = newOrder.CreatedAt;
            oldOrder.UserId = newOrder.UserId;

        }
    }
}
