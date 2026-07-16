using ECommerce.Contract;
using ECommerce.Data.Entities;
using ECommerce.Ground;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Presistance
{
    public class JwtToken : IJwtToken
    {
        public string GenerateAccessToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configurations.JWTKey)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(Configurations.JWTDurationMinuates),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string ValidateAccessToken(string accessToken)
        {
            string userId = string.Empty;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Configurations.JWTKey);
                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return userId;
        }
        public string GetUserIdFromExpiredToken(string accessToken)
        {
            var tokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateAudience = false,

                    ValidateIssuer = false,

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                Configurations.JWTKey)),

                    // 🔥 Ignore Expiration
                    ValidateLifetime = false
                };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return string.Empty;
            }
            return principal.FindFirst(ClaimTypes.NameIdentifier).Value;

        }

        public RefreshToken GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            var refreshToken = Convert.ToBase64String(randomBytes);
            return new RefreshToken
            {
                Token = refreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(Configurations.RefreshTokenDurationDays),
                CreateDate = DateTime.UtcNow
            };
        }
    }
}
