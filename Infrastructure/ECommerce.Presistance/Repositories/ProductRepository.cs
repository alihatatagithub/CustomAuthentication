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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<PagedResult<Product>> GetProducts(ProductListFilterDTO model)
        {
            var query = AppDbContext.Products
                .Include(x => x.ProductUploadedFiles)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Vendor)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            query = query.FilterIf(!string.IsNullOrEmpty(model.Name), a => a.Name.ToLower().Contains(model.Name.ToLower()));
            query = query.FilterIf(model.CategoryId.HasValue, a => a.CategoryId == model.CategoryId);
            query = query.FilterIf(model.BrandId.HasValue, a => a.BrandId == model.BrandId);


            var result = await query
             .Skip((model.Page.Value - 1) * model.PageSize.Value)
             .Take(model.PageSize.Value)
             .ToListAsync();

            return new PagedResult<Product>(result, totalCount);
        }
    }
}
