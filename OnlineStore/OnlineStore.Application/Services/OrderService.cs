using AutoMapper;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.DAL.Repositories.interfaces;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.Services
{
   public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<OrderDto>>(orders);
        }
        
        public async Task<List<OrderDto>> GetAllOrdersByUserAsync(User user)
        {
            var orders = await _orderRepository.GetAllByUser(user);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDetailDto> GetOrderByIdAsync(long id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDetailDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            await _orderRepository.CreateAsync(order);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> UpdateOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            var updated = await _orderRepository.UpdateAsync(order);
            return updated;
        }

        public async Task<bool> DeleteOrderAsync(long id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return false;
            }

            var deleted = await _orderRepository.DeleteAsync(order);
            if (deleted)
            {
                await _orderRepository.SaveChangesAsync();
            }
            return deleted;
        }
    }
}