using ECommerce.Contract.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Service.DependencyResolution
{
    public static class CoreDependencyResolution
    {
        public static void AddCoreService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
