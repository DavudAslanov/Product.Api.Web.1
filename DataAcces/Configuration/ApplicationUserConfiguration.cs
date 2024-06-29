using Core.DefaultValue;
using Entities.Concrete.TableModels.Membership;
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
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUsers");

            builder.Property(x => x.FirstName)
             .IsRequired()
             .HasMaxLength(300);

            builder.Property(x => x.LastName)
             .IsRequired()
             .HasMaxLength(300);

            builder.Property(x => x.Email)
             .IsRequired()
             .HasMaxLength(500);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Gender)
             .IsRequired();



        }
    }
}
