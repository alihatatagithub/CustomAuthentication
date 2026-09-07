using ECommerce.Data;
using ECommerce.Data.DTO;

namespace ECommerce.Contract.Services
{
    public interface ICategoryService
    {
        Task<ResponseList<CategoryListDTO>> CategoryList(CategoryListFilterDTO model);
        Task<Response<SuccessDTO>> CreateCategory(CreateCategoryDTO model);
    }
}
