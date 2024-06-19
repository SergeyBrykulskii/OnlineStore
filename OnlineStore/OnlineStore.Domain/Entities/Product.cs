using OnlineStore.Domain.Interfaces.Entities;

namespace OnlineStore.Domain.Entities;

public class Product : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
}
