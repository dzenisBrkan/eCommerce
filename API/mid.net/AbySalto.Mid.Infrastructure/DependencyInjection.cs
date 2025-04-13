using System.Data;
using System.Text;
using AbySalto.Mid.Domain.Data;
using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AbySalto.Mid.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructur(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDatabase(configuration);
              //.AddIdentityServices(configuration);


            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        //private static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddIdentityCore<User>(opt =>
        //    {
        //        //opt.User.RequireUniqueEmail = true;
        //        //opt.SignIn.RequireConfirmedAccount = false;
        //        //opt.Password.RequireDigit = false;
        //        //opt.Password.RequiredLength = 6;
        //        opt.Password.RequireNonAlphanumeric = false;
        //        //opt.Password.RequireUppercase = false;
        //    })
        //    .AddRoles<Role>()
        //        .AddRoleManager<RoleManager<Role>>()
        //        .AddEntityFrameworkStores<ApplicationDbContext>();

        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //        .AddJwtBearer(options =>
        //        {
        //            options.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuerSigningKey = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
        //                ValidateIssuer = false,
        //                ValidateAudience = false
        //            };
        //        });

        //    services.AddAuthorization(opt =>
        //    {
        //        opt.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
        //    });

        //    return services;
        //}
    }
}
