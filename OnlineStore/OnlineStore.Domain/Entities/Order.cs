using OnlineStore.Domain.Interfaces.Entities;

namespace OnlineStore.Domain.Entities;

public class Order : IEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<Product> Products { get; set; }
    public Guid UserId { get; set; }
}
