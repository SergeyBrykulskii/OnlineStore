using OnlineStore.Domain.Entities;

namespace OnlineStore.Application.DTOs;

//Dto for obtaining information about all orders/orders by UserId
public class OrderDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public long UserId { get; set; }
}

//Dto for obtaining detailed information about an order by ID
public class OrderDetailDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public long UserId { get; set; }
}

public class CreateOrderDto
{
    public long UserId { get; set; }
}
