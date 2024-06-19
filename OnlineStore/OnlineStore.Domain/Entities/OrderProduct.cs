using OnlineStore.Domain.Interfaces.Entities;

namespace OnlineStore.Domain.Entities;

public class OrderProduct : IEntity
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long OrderId { get; set; }
    public int Quantity { get; set; }
}
