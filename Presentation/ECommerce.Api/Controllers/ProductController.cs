using ECommerce.Api.Attributes;
using ECommerce.Api.Controllers.Base;
using ECommerce.Contract.Services;
using ECommerce.Data.DTO;
using ECommerce.Ground;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("create")]
        [Authorize(Roles = "Admin,Vendor")]
        public async Task<IActionResult> Create([FromForm] CreateProductDTO model)
        {
            var result = await _productService.CreateProduct(model, User.GetUserId());
            return GetApiResponse(result);
        }
        [HttpGet("list")]
        public async Task<IActionResult> ProductList([FromQuery] ProductListFilterDTO model)
        {
            var result = await _productService.ProductList(model);
            return GetApiResponse(result, model.Page.Value, model.PageSize.Value);
        }
    }
}
