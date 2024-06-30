namespace OnlineStore.Application.DTOs.OrderDTOs;

public class OrderDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
}