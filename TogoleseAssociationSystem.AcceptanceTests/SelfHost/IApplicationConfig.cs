namespace TogoleseAssociationSystem.AcceptanceTests.SelfHost
{
    public interface IApplicationConfig
    {
        string ApplicationPath { get; }
        string AssemblyName { get; }
        int StartingPortNumber { get; }
        bool UseHttps { get; }
    }
}
