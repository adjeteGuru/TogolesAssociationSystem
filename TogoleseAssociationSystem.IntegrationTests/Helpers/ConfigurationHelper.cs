using Microsoft.Extensions.Configuration;

namespace TogoleseAssociationSystem.IntegrationTests.Helpers
{
    public static class ConfigurationHelper
    {
        private static IConfiguration cachedAppSettings;

        public static IConfiguration GetAppSettings()
        {
            if (cachedAppSettings == null)
            {
                cachedAppSettings = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
            }

            return cachedAppSettings;
        }

        public static string GetConfigurationByName(string settingName)
        {
            return GetAppSettings().GetValue<string>(settingName);
        }
    }
}
