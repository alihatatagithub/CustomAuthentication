using ECommerce.Contract.Repositories;
using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Presistance.Repositories
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public Task<User> GetUserByEmail(string email)
        {
            return AppDbContext.Users
                .Include(a => a.RefreshToken)
                .Include(a => a.UserRoles)
                    .ThenInclude(a => a.Role)
                .Where(u => u.Email == email).FirstOrDefaultAsync();
        }
        public Task<User> GetUserById(Guid id)
        {
            return AppDbContext.Users.Where(u => u.Id == id)
                .Include(a => a.UserRoles)
                    .ThenInclude(a => a.Role)
                .FirstOrDefaultAsync();
        }
        public Task<User> GetUserByRefreshToken(Guid userId, string refreshToken)
        {
            return AppDbContext.Users
                .Where(a => a.RefreshToken.Token == refreshToken && a.Id == userId)
                .FirstOrDefaultAsync();
        }
    }
}
