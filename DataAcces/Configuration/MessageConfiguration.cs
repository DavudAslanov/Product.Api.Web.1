using Core.DefaultValue;
using Entities.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcces.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.Property(x =>x.Id)
              .IsRequired()
              .UseIdentityColumn(seed: DefaultConstantValue.DEFAULT_PRIMARY_KEY_SEED, increment: 1);

            builder.Property(x => x.Mesage)
             .IsRequired()
             .HasMaxLength(3000);
        }
    }
}
