using Serilog;
using Persistence.Repositories;

namespace API.Configurations
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection LoggerConfigurations(this IServiceCollection services)
        {
            var logFile = AppSettingsExtensions.Configuration.GetSection("LogFiles").Value;

            var log = new LoggerConfiguration()
                .WriteTo.File(logFile.Replace("{date}", DateTime.Today.ToString("yyyy-MM-dd")), shared: true)
                .MinimumLevel.Error()
                .CreateLogger();

            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load("Persistence"));

            return services;
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetTypeService, AssetTypeService>();
            services.AddScoped<IOrderStateService, OrderStateService>();
            services.AddScoped<IOperationTypeService, OperationTypeService>();
            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IAssetTypeRepository, AssetTypeRepository>();
            services.AddScoped<IOrderStateRepository, OrderStateRepository>();
            services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();

            return services;
        }

        public static IServiceCollection RegisterApplicationValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();

            return services;
        }
    }
}
