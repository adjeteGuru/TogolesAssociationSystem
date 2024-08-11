namespace TogoleseAssociationSystem.IntegrationTests.Mocks
{
    public interface IServersConfiguration
    {
        int MockServerPort { get; }
        string MockServerUrl { get; }
        int WebServerPort { get; }
    }
}
