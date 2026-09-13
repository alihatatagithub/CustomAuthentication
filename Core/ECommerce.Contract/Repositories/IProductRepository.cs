using ECommerce.Data;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Contract.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResult<Product>> GetProducts(ProductListFilterDTO model);
    }
}
