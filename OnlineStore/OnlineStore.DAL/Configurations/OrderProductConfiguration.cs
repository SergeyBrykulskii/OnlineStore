using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntitiesConfiguration
{
    internal class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrderProducts");

            builder.HasKey(op => op.Id);

            builder.Property(op => op.ProductId)
                .IsRequired();

            builder.Property(op => op.OrderId)
                .IsRequired();

            builder.Property(op => op.Quantity)
                .IsRequired();
        }
    }
}
