using ECommerce.Data.Entities;

namespace ECommerce.Contract
{
    public interface IJwtToken
    {
        string GenerateAccessToken(User user, List<string> roles);
        RefreshToken GenerateRefreshToken();
        string GetUserIdFromExpiredToken(string accessToken);
    }
}
