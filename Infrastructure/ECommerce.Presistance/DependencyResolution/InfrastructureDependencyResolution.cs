using ECommerce.Contract;
using ECommerce.Contract.Mappings;
using ECommerce.Contract.MediaService;
using ECommerce.Contract.Repositories;
using ECommerce.Ground;
using ECommerce.Presistance.Common.Mappings;
using ECommerce.Presistance.MediaService;
using ECommerce.Presistance.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace ECommerce.Presistance.DependencyResolution
{
    public static class InfrastructureDependencyResolution
    {
        public static void AddPostgresDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(o => o.UseNpgsql(Configurations.ConnectionString));
        }
        public static void AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configurations.JWTKey))
                };
            });
        }
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtToken, JwtToken>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<IGenericMapper, GenericMapper>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICategoryMapper, CategoryMapper>();
        }
        public static void AddSerilog(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
