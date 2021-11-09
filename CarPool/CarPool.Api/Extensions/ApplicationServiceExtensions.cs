using CarPool.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarPool.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CarPoolDBContext>(
                options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // services.AddScoped<I, >();
           

            return services;
        }
    }
}
