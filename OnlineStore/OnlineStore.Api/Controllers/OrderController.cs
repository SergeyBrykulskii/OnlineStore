using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Application.Services;
using OnlineStore.Application.Services.Interfaces;
using OnlineStore.Domain.Entities;


namespace OnlineStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailDto>> GetOrder(long id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersByUser(User user)
        {
            var orders = await _orderService.GetAllOrdersByUserAsync(user);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(CreateOrderDto orderDto)
        {
            var createdOrder = await _orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Data.UserId }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(long id, OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest();
            }
            await _orderService.UpdateOrderAsync(orderDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}