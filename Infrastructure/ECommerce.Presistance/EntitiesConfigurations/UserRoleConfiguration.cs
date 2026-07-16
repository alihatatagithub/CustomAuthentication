using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Presistance.EntitiesConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(t => new { t.UserId, t.RoleId });

            builder
                .HasOne(s => s.User)
                .WithMany(c => c.UserRoles)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(s => s.Role)
                .WithMany(c => c.UserRoles)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
