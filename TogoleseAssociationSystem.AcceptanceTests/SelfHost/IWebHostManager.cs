using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace TogoleseSolidarity.AcceptanceTests.SelfHost
{
    public interface IWebHostManager
    {
        void Start<T>(int startupTcpPort, IServiceCollection services) where T : class;
        void Start<T>(int startupTcpPort) where T : class;
        void SetupForTest(IWebHost webHost);
        void Stop();
        string GetHostAddress();
    }
}
