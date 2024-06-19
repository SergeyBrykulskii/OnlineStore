using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DAL.EntitiesConfiguration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            //builder.HasMany(o => o.OrderProducts)
            //    .WithOne(op => op.Order)
            //    .HasForeignKey(op => op.OrderId); 

            builder.Property(o => o.UserId)
                .IsRequired();

            //builder.HasOne(o => o.User)
            //    .WithMany()
            //    .HasForeignKey(o => o.UserId); 
        }
    }
}
