using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Contract.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByRefreshToken(Guid userId, string refreshToken);
    }
}
