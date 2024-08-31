using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Reflection;

namespace TogoleseAssociationSystem.AcceptanceTests.SelfHost
{
    public class WebHostManager : IWebHostManager
    {
        private readonly string relativeApplicationPath;
        private readonly string assemblyName;
        private readonly bool useHttps;
        private IWebHost webHost;
        public WebHostManager(IApplicationConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config), "Parameter must not be null.");
            }
            if (string.IsNullOrWhiteSpace(config.ApplicationPath))
            {
                throw new ArgumentException("Parameter must not be null or whitespace.", nameof(config.ApplicationPath));
            }
            if (string.IsNullOrWhiteSpace(config.AssemblyName))
            {
                throw new ArgumentException("Parameter must not be null or whitespace.", nameof(config.AssemblyName));
            }

            relativeApplicationPath = config.ApplicationPath;
            assemblyName = config.AssemblyName;
            useHttps = config.UseHttps;
        }
        public string GetHostAddress()
        {
            throw new NotImplementedException();
        }

        public void SetupForTest(IWebHost webHost)
        {
            throw new NotImplementedException();
        }

        public void Start<T>(int startupTcpPort, IServiceCollection services) where T : class
        {
            if (webHost == null)
            {
                webHost = new WebHostBuilder()
                    .UseKestrel(kestrelOptions =>
                    {
                        kestrelOptions.Listen(IPAddress.Loopback, startupTcpPort, listenOptions =>
                        {
                            if (useHttps)
                            {
                                listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
                                listenOptions.UseHttps();
                            }
                        });
                    }).ConfigureServices(s =>
                    {
                        foreach (var service in services)
                        {
                            s.Add(service);
                        }
                    }).ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddDebug();
                    }).UseContentRoot(GetApplicationPath())
                    .UseStartup<T>()
                    .UseSetting(WebHostDefaults.ApplicationKey, assemblyName)
                    .Build();
            }
            webHost.Start();
        }

        public void Start<T>(int startupTcpPort) where T : class
        {
            var services = new ServiceCollection();
            Start<T>(startupTcpPort, services);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        private string GetApplicationPath()
        {
            return Path.GetFullPath(Path.Combine(GetCurrentDirectory(), relativeApplicationPath));
        }

        private string GetCurrentDirectory() 
        {
            return Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
        }
    }
}
