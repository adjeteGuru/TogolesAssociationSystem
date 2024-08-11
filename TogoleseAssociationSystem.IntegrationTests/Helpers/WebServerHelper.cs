using BoDi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;
using TogoleseAssociationSystem.IntegrationTests.ConfigurationModels;
using TogoleseAssociationSystem.IntegrationTests.Mocks;

namespace TogoleseAssociationSystem.IntegrationTests.Helpers
{
    public static class WebServerHelper
    {
        //private static WebApplicationHost webApplicationHost;
        //public static string WebServerAddress
        //{
        //    //get { /* webApplicationHost.GetServerAddress()*/; }
        //}

        public static void StartWebServer(IServiceCollection services, IObjectContainer objectContainer)
        {
            var serversConfiguration = objectContainer.Resolve<IServersConfiguration>();

            var builder = new ConfigurationBuilder().AddJsonFile(@"appsettings.json", false, true);
            var config = new TestApplicationConfig
            {
                ApplicationPath = "../../../../TogoleseAssociationSystem.API",
                AssemblyName = typeof(Program).GetTypeInfo().Assembly.FullName,
                StartingPortNumber = serversConfiguration.WebServerPort,
                UseHttps = false
            };

            services.AddSingleton(objectContainer);

            //webApplicationHost = new WebApplicationHost(config);
            //webApplicationHost.StartWebServer<>
        }
    }
}
