using Microsoft.AspNetCore.Identity;

namespace OnlineStore.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public List<Order>? Orders { get; set; }
}
