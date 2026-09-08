using ECommerce.Contract;
using ECommerce.Data.Entities;
using ECommerce.Ground;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Presistance
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, IJwtToken jwtToken, IPasswordHasher passwordHasher)
        {
            await SeedRoles(context);
            await SeedUsers(context,jwtToken,passwordHasher);
        }
        private static async Task SeedRoles(AppDbContext context)
        {
            if (!await context.Roles.AnyAsync(a => a.Name == SystemRoles.Admin.ToString()))
                await context.Roles.AddAsync(new Role() { Id = Guid.NewGuid(), Name = SystemRoles.Admin.ToString() });

            if (!await context.Roles.AnyAsync(a => a.Name == SystemRoles.Customer.ToString()))
                await context.Roles.AddAsync(new Role() { Id = Guid.NewGuid(), Name = SystemRoles.Customer.ToString() });

            if (!await context.Roles.AnyAsync(a => a.Name == SystemRoles.Vendor.ToString()))
                await context.Roles.AddAsync(new Role() { Id = Guid.NewGuid(), Name = SystemRoles.Vendor.ToString() });

            await context.SaveChangesAsync();
        }
        private static async Task SeedUsers(AppDbContext context, IJwtToken jwtToken, IPasswordHasher passwordHasher)
        {
            if (!await context.Users.AnyAsync(a => a.Email == "Ali.Admin@gmail.com"))
            {
                var adminRole = await context.Roles.FirstOrDefaultAsync(a => a.Name == SystemRoles.Admin.ToString());

                var adminUser = new User() { Id = Guid.NewGuid(), Phone = "0123456789" , FullName = "Admin Ali",  Email = "Ali.Admin@gmail.com", PasswordHash = passwordHasher.Hash("Test@123") };
                adminUser.RefreshToken = jwtToken.GenerateRefreshToken();
                adminUser.UserRoles = new List<UserRole>() { new UserRole { RoleId = adminRole.Id, UserId = adminUser.Id } };
                
                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }

        }
    }
}
