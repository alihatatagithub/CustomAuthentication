using ECommerce.Ground;

namespace ECommerce.Data.DTO
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public LoginRole LoginRole { get; set; }
    }
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AuthResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
    }
    public class UserTokensDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class LogoutDTO
    {
        public string RefreshToken { get; set; }
    }
}
