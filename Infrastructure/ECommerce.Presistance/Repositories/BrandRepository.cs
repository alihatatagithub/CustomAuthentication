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
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<PagedResult<Brand>> GetBrands(BrandListFilterDTO model)
        {
            var query = AppDbContext.Brands.AsQueryable();

            var totalCount = await query.CountAsync();
            query = query.FilterIf(!string.IsNullOrEmpty(model.Name), a => a.Name.ToLower().Contains(model.Name));

            var result = await query
             .Skip((model.Page.Value - 1) * model.PageSize.Value)
             .Take(model.PageSize.Value)
             .ToListAsync();

            return new PagedResult<Brand>(result, totalCount);

        }
    }
}
