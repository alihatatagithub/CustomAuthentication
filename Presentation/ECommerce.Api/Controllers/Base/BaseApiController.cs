using ECommerce.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Base
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult GetApiResponse<TResult>(Response<TResult> result) where TResult : class
        {
            if (result.IsValid)
            {
                return Ok(result.Model);
            }
            return GetErrorResult(result);
        }
        protected IActionResult GetApiResponse<TResult>(ResponseList<TResult> result, int page, int pageSize) where TResult : class
        {
            if (result.IsValid)
            {
                return Ok(result.Model);
            }
            return GetErrorResult(result);
        }
        protected IActionResult GetErrorResult(ResponseBase result)
        {
            if (result == null)
            {
                return StatusCode(500);
            }

            return BadRequest(result);
        }
    }
}
