using ECommerce.Data;
using ECommerce.Data.DTO;

namespace ECommerce.Contract.Services
{
    public interface IAuthService
    {
        Task<Response<AuthResponseDTO>> Login(LoginDTO model);
        Task<Response<SuccessDTO>> Logout(Guid userId, LogoutDTO model);
        Task<Response<UserTokensDTO>> RefreshToken(UserTokensDTO model);
        Task<Response<AuthResponseDTO>> Register(RegisterDTO model);
    }
}
