using CarPool.Common;
using CarPool.Common.Contracts;
using CarPool.Data;
using CarPool.Services;
using CarPool.Services.Contracts;
using CarPool.Services.Data.Contracts;
using CarPool.Services.Data.Services;
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
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddSingleton<IMailSettings, MailSettings>();
            services.AddTransient<IMailService, MailService>();
            //services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddHostedService<BanHostedService>();
            // services.AddScoped<I, >();

            return services;
        }
    }
}
