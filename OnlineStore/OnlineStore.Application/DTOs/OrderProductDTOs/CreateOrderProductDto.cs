namespace OnlineStore.Application.DTOs.OrderProductDTOs;

public class CreateOrderProductDto
{
    public long ProductId { get; set; }
    public long OrderId { get; set; }
    public int Quantity { get; set; }
}
