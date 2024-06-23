using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntitiesConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedAt)
            .IsRequired();

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.HasOne(c => c.User)
         .WithMany(p => p.Orders)
         .HasForeignKey(p => p.UserId)
         .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Products)
           .WithMany(p => p.Orders)
           .UsingEntity<OrderProduct>(
                l => l.HasOne<Product>().WithMany().HasForeignKey(e => e.OrderId),
                r => r.HasOne<Order>().WithMany().HasForeignKey(e => e.ProductId));
    }
}
