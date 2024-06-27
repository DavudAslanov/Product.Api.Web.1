using Core.DefaultValue;
using Entities.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.Property(x => x.Id)
              .IsRequired()
              .UseIdentityColumn(seed: DefaultConstantValue.DEFAULT_PRIMARY_KEY_SEED, increment: 1);

            builder.Property(x => x.CategoryName)
             .IsRequired()
             .HasMaxLength(500);
        }
    }
}
