using Core.BaseMessages;
using Entities.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcces.Configuration
{
    public class UserProductConfiguration : IEntityTypeConfiguration<UserProduct>
    {
        public void Configure(EntityTypeBuilder<UserProduct> builder)
        {
           
            builder.HasKey(up => new { up.Id, up.ProductId });

            builder.HasOne(up => up.User)
                   .WithMany(u => u.UserProducts)
                   .HasForeignKey(up => up.Id)
                   .HasPrincipalKey(u => u.UserId);


            builder.HasOne(up => up.User)
                   .WithMany(u => u.UserProducts)
                   .HasForeignKey(up => up.Id);

            builder.HasOne(up => up.Product)
                   .WithMany(p => p.UserProducts)
                   .HasForeignKey(up => up.ProductId);
        }
    }
}
