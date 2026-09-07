using ECommerce.Data;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;

namespace ECommerce.Contract.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<PagedResult<Brand>> GetBrands(BrandListFilterDTO model);
    }
}
