using ECommerce.Data;
using ECommerce.Data.DTO;

namespace ECommerce.Contract.Services
{
    public interface IProductService
    {
        Task<ResponseList<ProductListDTO>> ProductList(ProductListFilterDTO model);
        Task<Response<SuccessDTO>> CreateProduct(CreateProductDTO model, Guid vendorId);
    }
}
