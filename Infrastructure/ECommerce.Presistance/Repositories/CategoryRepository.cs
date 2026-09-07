using ECommerce.Contract.Repositories;
using ECommerce.Data;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using ECommerce.Ground;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Repositories
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
