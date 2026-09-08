using ECommerce.Contract.Repositories;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Contract
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBrandRepository BrandRepository { get; }
        Task SaveChangesAsync();
    }
}
