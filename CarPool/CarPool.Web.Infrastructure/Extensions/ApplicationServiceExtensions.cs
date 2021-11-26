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

namespace CarPool.Web.Infrastructure.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CarPoolDBContext>(
                options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICheckExistenceService, CheckExistenceService>();
            services.AddSingleton<IMailSettings, MailSettings>();
            services.AddTransient<IMailService, MailService>();
            services.AddScoped<ITripService, TripService>();
            services.AddScoped<IProfilePictureService, ProfilePictureService>();
            services.AddScoped<IBingApiService, BingApiService>();
            services.AddScoped<IBanService, BanService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFuelService, FuelService>();
            services.AddScoped<IGoogleAccountService, GoogleAccountService>();

            services.AddHostedService<BanHostedService>();
            // services.AddScoped<I, >();

            return services;
        }
    }
}
