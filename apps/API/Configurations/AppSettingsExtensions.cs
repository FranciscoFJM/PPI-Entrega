namespace API.Configurations
{
public static class AppSettingsExtensions
    {
        private static IConfiguration appSettingsExtensions;

        public static IConfiguration Configuration
        {
            get
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var buider = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();

                appSettingsExtensions = buider.Build();

                return appSettingsExtensions;
            }
        }

        public static string GetConfigurationValue(string key)
        {
            var value = Configuration.GetSection(key).Value;

            if (string.IsNullOrEmpty(value))
            {
                value = Environment.GetEnvironmentVariable(key) ?? Environment.GetEnvironmentVariable(key.Replace(":", "__"));
            }

            return value;
        }
    }
}
