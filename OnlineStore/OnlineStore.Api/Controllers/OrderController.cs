using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.OrderDTOs;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;


namespace OnlineStore.API.Controllers;

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
    public async Task<ActionResult<BaseResult<OrderDto>>> GetOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet(nameof(id))]
    public async Task<ActionResult<BaseResult<OrderDetailDto>>> GetOrder(long id)
    {
        var orderServiceResponse = await _orderService.GetOrderByIdAsync(id);

        if (orderServiceResponse.IsSuccess)
        {
            return Ok(orderServiceResponse);
        }

        return BadRequest(orderServiceResponse);
    }

    [HttpGet(nameof(userId))]
    public async Task<ActionResult<BaseResult<OrderDto>>> GetOrdersByUser(Guid userId)
    {
        var orderServiceResponse = await _orderService.GetAllOrdersByUserAsync(userId);

        if (orderServiceResponse.IsSuccess)
        {
            return Ok(orderServiceResponse);
        }

        return BadRequest(orderServiceResponse);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResult<OrderDto>>> CreateOrder(CreateOrderDto orderDto)
    {
        var orderServiceResponse = await _orderService.CreateOrderAsync(orderDto);

        if (orderServiceResponse.IsSuccess)
        {
            return CreatedAtAction(nameof(GetOrder), new { id = orderServiceResponse.Data!.UserId }, orderServiceResponse.Data);
        }

        return BadRequest(orderServiceResponse);
    }

    [HttpPut]
    public async Task<ActionResult<BaseResult<OrderDto>>> UpdateOrder(OrderDto orderDto)
    {
        var orderServiceResponse = await _orderService.UpdateOrderAsync(orderDto);


        if (orderServiceResponse.IsSuccess)
        {
            return Ok(orderServiceResponse);
        }

        return BadRequest(orderServiceResponse);
    }

    [HttpDelete(nameof(id))]
    public async Task<ActionResult<BaseResult<long>>> DeleteOrder(long id)
    {
        var orderServiceResponse = await _orderService.DeleteOrderAsync(id);

        if (orderServiceResponse.IsSuccess)
        {
            return Ok(orderServiceResponse);
        }

        return BadRequest(orderServiceResponse);
    }
}