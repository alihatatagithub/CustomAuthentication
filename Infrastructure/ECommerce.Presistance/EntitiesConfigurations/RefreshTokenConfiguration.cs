using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Presistance.EntitiesConfigurations
{
    public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(
            EntityTypeBuilder<RefreshToken> builder)
        {

            builder.HasKey(x => x.Id);

            builder
                .HasOne(c => c.User)
                .WithOne(x => x.RefreshToken)
                .HasForeignKey<RefreshToken>(c => c.Id);
        }
    }
}
