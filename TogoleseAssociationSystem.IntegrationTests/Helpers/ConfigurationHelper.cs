using Newtonsoft.Json.Linq;

namespace TogoleseSolidarity.IntegrationTests.Helpers
{
    public static class ConfigurationHelper
    {
        private static readonly Dictionary<string, string> Config = new Dictionary<string, string>();
        static ConfigurationHelper()
        {
            var launchSettings = JObject.Parse(File.ReadAllText("Properties/launchSettings.json"));
            var environmentVariables = launchSettings["profiles"]["TogolesAssociationSystem.IntegrationTests"]["environmentVariables"];
            var tempDictionary = environmentVariables.ToObject<Dictionary<string, string>>();
            foreach (var (key, value) in tempDictionary)
            {
                Config.Add(key, value.ToString());
            }
        }

        public static string GetSetting(string name)
        {
            return Environment.GetEnvironmentVariable(name) ?? Config[name];
        }
    }
}
