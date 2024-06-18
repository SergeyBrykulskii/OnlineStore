using OnlineStore.Domain.Interfaces.Entities;

namespace OnlineStore.Domain.Entities;

public class Order : IEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Product> Products { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
