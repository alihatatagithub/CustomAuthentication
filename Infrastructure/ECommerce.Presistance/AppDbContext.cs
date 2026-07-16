using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Emit;

namespace ECommerce.Presistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // apply entity type configurations from this assembly
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            RemovePluralizingTableNameConvention(builder);
            base.OnModelCreating(builder);
        }
        private void RemovePluralizingTableNameConvention(ModelBuilder builder)
        {
            foreach (IMutableEntityType entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }
    }
}
