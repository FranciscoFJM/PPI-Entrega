using Microsoft.Data.SqlClient;

namespace API.Configurations
{
    public static class DBExtensions
    {
        public static IServiceCollection RegistrerDataBase(this IServiceCollection services)
        {
            var server = AppSettingsExtensions.GetConfigurationValue("ConnectionString:Server");
            var database = AppSettingsExtensions.GetConfigurationValue("ConnectionString:DataBase");
            var user = AppSettingsExtensions.GetConfigurationValue("ConnectionString:User");
            var password = AppSettingsExtensions.GetConfigurationValue("ConnectionString:Password");

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                TrustServerCertificate = true,
                Encrypt = false,
                ConnectTimeout = 30,
                CommandTimeout = 300
            };

            //Si hay usuario, SQL Server Authentication, si no, Windows Authentication
            if (!string.IsNullOrEmpty(user))
            {
                builder.UserID = user;
                builder.Password = password;
                builder.IntegratedSecurity = false;
            }
            else
            {
                builder.IntegratedSecurity = true;
            }

            services.AddDbContext<PPIContext>(conf =>
                conf.UseSqlServer(builder.ConnectionString));

            return services;
        }
    }
}
