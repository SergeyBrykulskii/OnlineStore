﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

namespace OnlineStore.DAL.EntitiesConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
            .IsRequired();
            builder.Property(p => p.CategoryId)
                .IsRequired();

            builder.HasOne(p => p.Category)        
                  .WithMany(c => c.Products)          
                  .HasForeignKey(p => p.CategoryId)  
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}