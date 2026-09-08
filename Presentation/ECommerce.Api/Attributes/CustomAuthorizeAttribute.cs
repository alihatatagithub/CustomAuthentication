using ECommerce.Ground;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Api.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// CustomAuthorizeAttribute
        /// </summary>
        /// <param name="roles"></param>
        public CustomAuthorizeAttribute(params SystemRoles[] roles) : base()
        {
            Roles = string.Join(",", roles.Select(r => ((int)r).ToString()));
        }
    }
}
