using Microsoft.AspNetCore.Identity;
using OnlineStore.Domain.Interfaces.Entities;

namespace OnlineStore.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public List<Order>? Orders { get; set; }
}
