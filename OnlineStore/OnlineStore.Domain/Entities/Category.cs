using OnlineStore.Domain.Interfaces.Entities;

namespace OnlineStore.Domain.Entities;

public class Category : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
}
