using System.Security.Claims;

namespace ECommerce.Ground
{
    public static class UserExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        }
        public static List<string> GetUserRoles(this ClaimsPrincipal user)
        {
            return user.FindAll(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        }
    }
}
