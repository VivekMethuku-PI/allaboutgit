using Microsoft.Extensions.Configuration;

namespace VoucherCreation.BAL.Infrastructure.AppSettingsConfig
{
    public class AppSettings
    {
        public readonly ConfigAppSettings configAppSettings;
        public string connectionString { get; set; }

        public IConfigurationRoot config;

        public AppSettings()
        {
            string dirPath = System.AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(dirPath + "\\appsettings.json"))
            {
                dirPath = Directory.GetCurrentDirectory();
            }
            config = new ConfigurationBuilder().SetBasePath(dirPath).AddJsonFile("appsettings.json").Build();

            this.configAppSettings = new ConfigAppSettings
            {
                JwtSecret = config.GetSection("AppSettings:JwtSecret").Value,
                JwtIssuer = config.GetSection("AppSettings:JwtIssuer").Value,
                JwtValidityMinutes = Convert.ToInt32(config.GetSection("AppSettings:JwtValidityMinutes").Value)
            };

            this.connectionString = config.GetSection("ConnectionStrings:dbConnection").Value;
        }

    }



    public class ConfigAppSettings
    {
        public string? JwtSecret { get; set; }
        public string? JwtIssuer { get; set; }
        public int JwtValidityMinutes { get; set; }
    }


}
