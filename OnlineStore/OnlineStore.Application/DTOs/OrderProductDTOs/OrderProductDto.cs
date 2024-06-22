namespace OnlineStore.Application.DTOs.OrderProductDTOs;

public class OrderProductDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long OrderId { get; set; }
    public int Quantity { get; set; }
}
