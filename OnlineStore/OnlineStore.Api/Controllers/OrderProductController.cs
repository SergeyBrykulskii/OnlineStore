using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.OrderProductDTOs;
using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;

namespace OnlineStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderProductController : ControllerBase
{
    private readonly IOrderProductService _orderProductService;
    public OrderProductController(IOrderProductService orderProductService)
    {
        _orderProductService = orderProductService;
    }

    [HttpGet(nameof(orderId))]
    [Authorize]
    public async Task<ActionResult<CollectionResult<OrderProductDto>>> GetProductsInOrder(long orderId)
    {
        var orderProductServiceResponse = await _orderProductService.GetProductsByOrder(orderId);

        if (orderProductServiceResponse.IsSuccess)
        {
            return Ok(orderProductServiceResponse);
        }

        return BadRequest(orderProductServiceResponse);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<BaseResult<CreateOrderProductDto>>> CreateOrderProduct(
        [FromBody] CreateOrderProductDto orderProductDto)
    {
        var orderProductServiceResponse = await _orderProductService.AddProductToOrderAsync(orderProductDto);

        if (orderProductServiceResponse.IsSuccess)
        {
            return Ok(orderProductServiceResponse);
        }

        return BadRequest(orderProductServiceResponse);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<BaseResult<UpdateProductDto>>> UpdateOrderProduct(
        [FromBody] UpdateOrderProductDto orderProductDto)
    {
        var orderProductServiceResponse =
            await _orderProductService.UpdateProductQuantityInOrderAsync(orderProductDto);

        if (orderProductServiceResponse.IsSuccess)
        {
            return Ok(orderProductServiceResponse);
        }

        return BadRequest(orderProductServiceResponse);
    }

    [HttpDelete(nameof(id))]
    [Authorize]
    public async Task<ActionResult<BaseResult<long>>> DeleteOrderProduct(long id)
    {
        var orderProductServiceResponse = await _orderProductService.RemoveProductFromOrderAsync(id);

        if (orderProductServiceResponse.IsSuccess)
        {
            return Ok(orderProductServiceResponse);
        }

        return BadRequest(orderProductServiceResponse);
    }
}
