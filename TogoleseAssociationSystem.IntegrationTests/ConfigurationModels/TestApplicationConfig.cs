namespace TogoleseSolidarity.IntegrationTests.ConfigurationModels
{
    public class TestApplicationConfig
    {
        public string? ApplicationPath { get; set; }
        public string? AssemblyName { get; set; }
        public int StartingPortNumber { get; set; }
        public bool UseHttps { get; set; }

    }
}
