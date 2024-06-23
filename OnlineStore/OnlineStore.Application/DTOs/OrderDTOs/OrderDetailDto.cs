using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.DTOs.OrderDTOs;

public class OrderDetailDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public long UserId { get; set; }
}
