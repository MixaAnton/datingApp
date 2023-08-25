using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Interfaces;
using webapi.Services;
using AutoMapper;
using webapi.Helpers;

namespace webapi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILikesRepository, LikesRepository>(); 
            services.AddScoped<LogUserActivity>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
