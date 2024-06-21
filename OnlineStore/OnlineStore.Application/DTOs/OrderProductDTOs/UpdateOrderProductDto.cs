namespace OnlineStore.Application.DTOs.OrderProductDTOs;

public class UpdateOrderProductDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long OrderId { get; set; }
    public int Quantity { get; set; }
}
