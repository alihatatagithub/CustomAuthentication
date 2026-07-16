using ECommerce.Contract;
using ECommerce.Contract.Mappings;
using ECommerce.Contract.Services;
using ECommerce.Data;
using ECommerce.Data.DTO;
using ECommerce.Ground;

namespace ECommerce.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtToken _jwtToken;
        public AuthService(IUnitOfWork unitOfWork, IUserMapper mapper, IPasswordHasher passwordHasher, IJwtToken jwtToken)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtToken = jwtToken;
        }
        public async Task<Response<SuccessDTO>> Logout(Guid userId,LogoutDTO model)
        {
            var user = await _unitOfWork.UserRepository.GetUserByRefreshToken(userId, model.RefreshToken);
            if (user == null)
            {
                return new Response<SuccessDTO>
                {
                    IsValid = false,
                };
            }
            user.RefreshToken = null;
            await _unitOfWork.SaveChangesAsync();
            return new Response<SuccessDTO>
            {
                IsValid = true,
                Model = new SuccessDTO { IsSuccess = false }
            };
        }
        public async Task<Response<AuthResponseDTO>> Register(RegisterDTO model)
        {
            var hashedPassword = _passwordHasher.Hash(model.Password);
            var user = _mapper.ToEntity(model);
            user.PasswordHash = hashedPassword;

            SystemRoles role = model.LoginRole == LoginRole.Vendor ? SystemRoles.Vendor : SystemRoles.Customer;
            var accessToken = _jwtToken.GenerateAccessToken(user, new List<string> { role.ToString() });
            var refreshToken = _jwtToken.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            await _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.SaveChangesAsync();
            return new Response<AuthResponseDTO>
            {
                Model = new AuthResponseDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token,
                    UserId = user.Id
                }
            };

        }
        public async Task<Response<AuthResponseDTO>> Login(LoginDTO model)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmail(model.Email);

            var hashedPassword = _passwordHasher.Verify(model.Password,user.PasswordHash);
            if (hashedPassword)
            {
                var roles = user.UserRoles.Select(a => a.Role.Name).ToList();
                if (roles == null || !roles.Any())
                {
                    return new Response<AuthResponseDTO>
                    {
                        IsValid = false,
                    };
                }
                var accessToken = _jwtToken.GenerateAccessToken(user, roles);

                if (user.RefreshToken.ExpiryDate <= DateTime.UtcNow)
                {
                    user.RefreshToken = _jwtToken.GenerateRefreshToken();
                    await _unitOfWork.SaveChangesAsync();
                }
                return new Response<AuthResponseDTO>
                {
                    Model = new AuthResponseDTO
                    {
                        AccessToken = accessToken,
                        RefreshToken = user.RefreshToken.Token,
                        UserId = user.Id
                    }
                };
            }

            return new Response<AuthResponseDTO>
            {
                IsValid = false,
            };

        }

        public async Task<Response<UserTokensDTO>> RefreshToken(UserTokensDTO model)
        {
            var userId = _jwtToken.GetUserIdFromExpiredToken(model.AccessToken);
            if (!string.IsNullOrEmpty(userId))
            {
                return new Response<UserTokensDTO>
                {
                    IsValid = false,
                };
            }

            var user = await _unitOfWork.UserRepository.GetUserById(Guid.Parse(userId));
            if (user == null || user.RefreshToken.Token != model.RefreshToken || user.RefreshToken.ExpiryDate <= DateTime.UtcNow)
            {
                return new Response<UserTokensDTO>
                {
                    IsValid = false,
                };
            }
            var accessToken = _jwtToken.GenerateAccessToken(user, user.UserRoles.Select(a => a.Role.Name).ToList());
            return new Response<UserTokensDTO>
            {
                Model = new UserTokensDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = user.RefreshToken.Token
                }
            };

        }
    }
}
