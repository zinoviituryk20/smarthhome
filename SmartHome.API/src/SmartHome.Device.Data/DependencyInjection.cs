using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SmartHome.Device.Data.Configurations;
using SmartHome.Device.Data.DB.Repository.Devices;
using SmartHome.Device.Data.DB.Repository.Locations;

namespace SmartHome.Device.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<IMongoDatabase>(options =>
            {
                var settings = configuration.GetSection("MongoDBSettings").Get<MongoDbSettings>();
                var client = new MongoClient(settings.ConnectionString);
                return client.GetDatabase(settings.DatabaseName);
            });
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILocationsRepository, LocationsRepository>();
            services.AddScoped<IDevicesRepository, DevicesRepository>();
            return services;
        }
    }
}
