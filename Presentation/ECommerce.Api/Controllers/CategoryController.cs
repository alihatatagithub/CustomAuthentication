using ECommerce.Api.Controllers.Base;
using ECommerce.Contract.Services;
using ECommerce.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateCategoryDTO model)
        {
            var result = await _categoryService.CreateCategory(model);
            return GetApiResponse(result);
        }
        [HttpGet("list")]
        public async Task<IActionResult> CategoryList([FromQuery] CategoryListFilterDTO model)
        {
            var result = await _categoryService.CategoryList(model);
            return GetApiResponse(result);
        }
    }
}
