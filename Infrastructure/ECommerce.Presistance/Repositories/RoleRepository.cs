using ECommerce.Contract.Repositories;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
