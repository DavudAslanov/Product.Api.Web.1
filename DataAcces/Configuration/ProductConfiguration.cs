using Core.DefaultValue;
using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn(seed:DefaultConstantValue.DEFAULT_PRIMARY_KEY_SEED,increment:1);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.Views)
                .HasPrecision(7,3);

            builder.Property(x => x.Rating)
                .HasMaxLength(5);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x=>x.Price)
                .IsRequired()
                .HasPrecision(7,2);

            builder.Property(x=>x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Message)
                .HasMaxLength(3000);

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(p => p.UserProducts)
                   .WithOne(up => up.Product)
                   .HasForeignKey(up => up.ProductId);

            builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryID)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
