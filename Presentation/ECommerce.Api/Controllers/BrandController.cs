using ECommerce.Api.Controllers.Base;
using ECommerce.Contract.Services;
using ECommerce.Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : BaseApiController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> CategoryList([FromQuery] BrandListFilterDTO model)
        {
            var result = await _brandService.BrandList(model);
            return GetApiResponse(result, model.Page.Value, model.PageSize.Value);
        }
    }
}
