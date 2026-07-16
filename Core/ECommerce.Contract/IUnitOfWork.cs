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
        Task SaveChangesAsync();
    }
}
