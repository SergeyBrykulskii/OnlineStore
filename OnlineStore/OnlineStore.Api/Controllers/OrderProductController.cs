using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.DTOs.OrderProductDTOs;
using OnlineStore.Application.DTOs.ProductDTOs;
using OnlineStore.Application.Result;
using OnlineStore.Application.Services.Interfaces;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService _orderProductService;
        public OrderProductController(IOrderProductService orderProductService)
        {
            _orderProductService = orderProductService;
        }

        [HttpGet]
        public async Task<ActionResult<CollectionResult<OrderProductDto>>> GetProductsInOrder(long orderId)
        {
            var result = await _orderProductService.GetProductsByOrder(orderId);

            if (result == null || result.Data == null)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResult<CreateOrderProductDto>>> CreateOrderProduct([FromBody] CreateOrderProductDto orderProductDto)
        {
            var result = await _orderProductService.AddProductToOrderAsync(orderProductDto);

            if (result.Data == null)
            {
                return BadRequest(new { result.ErrorMessage });
            }

            return CreatedAtAction(nameof(CreateOrderProduct), result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResult<UpdateProductDto>>> UpdateOrderProduct(long id, [FromBody] UpdateOrderProductDto orderProductDto, CancellationToken cancellationToken)
        {
            if (id != orderProductDto.Id)
            {
                return BadRequest(new { Message = "ID doessn't exist" });
            }

            var result = await _orderProductService.UpdateProductQuantityInOrderAsync(orderProductDto);

            if (result.Data == null)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResult<long>>> DeleteOrderProduct(long id)
        {
            var result = await _orderProductService.RemoveProductFromOrderAsync(id);

            if (result.Data == 0)
            {
                return NotFound(new { result.ErrorMessage });
            }

            return Ok(result);
        }
    }
}
