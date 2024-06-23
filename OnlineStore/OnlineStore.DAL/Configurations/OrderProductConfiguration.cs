using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntitiesConfiguration;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {

        builder.HasKey(op => op.Id);

        builder.Property(op => op.ProductId)
            .IsRequired();

        builder.Property(op => op.OrderId)
            .IsRequired();

        builder.Property(op => op.Quantity)
            .IsRequired();

        builder.HasOne(op => op.Product)
        .WithOne()
        .HasForeignKey<OrderProduct>(op => op.ProductId);

        //builder.HasOne()
        //     .WithMany(c => c.OrderProducts)
        //     .HasForeignKey(p => p.OrderId)
        //     .OnDelete(DeleteBehavior.Restrict);
    }
}
