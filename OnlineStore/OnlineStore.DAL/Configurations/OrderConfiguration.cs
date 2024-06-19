using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
