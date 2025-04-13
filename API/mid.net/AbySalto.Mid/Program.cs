using AbySalto.Mid.Application;
using AbySalto.Mid.Domain.Data;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Infrastructure;
using AbySalto.Mid.WebApi.Services.AuthService;
using AbySalto.Mid.WebApi.Services.ProductItemService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register services
            builder.Services
                .AddPresentation()
                .AddApplication()
                .AddInfrastructur(builder.Configuration);

            // Register HttpClient
            builder.Services.AddHttpClient();

            // Register IProductService and ProductService for DI
            builder.Services.AddScoped<IProductService, ProductService>();


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddIdentity<User, Role>(options => {
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<ITokenService, TokenService>();

            //builder.Services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            //builder.Services.AddScoped<ITokenService, TokenService>();

            // Register CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //// Register the DbContext with the connection string from appsettings.json
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            //);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Desk Link");
                    options.RoutePrefix = string.Empty;
                });
            }

            // Use CORS middleware
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
