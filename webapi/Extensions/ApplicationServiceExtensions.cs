using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Interfaces;

namespace webapi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenService, ITokenService>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
