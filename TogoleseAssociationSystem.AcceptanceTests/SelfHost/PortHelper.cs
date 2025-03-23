using System.Net.NetworkInformation;

namespace TogoleseSolidarity.AcceptanceTests.SelfHost
{
    public class PortHelper : IPortHelper
    {
        private readonly IEnvironmentHelper environmentHelper;
        private int expectedStartPort;

        public PortHelper(IEnvironmentHelper environmentHelper, int expectedStartPort)
        {
            this.environmentHelper = environmentHelper;
            this.expectedStartPort = expectedStartPort;
        }
        public int GetFreeTcpPort()
        {
            if (expectedStartPort <= 0)
            {
                expectedStartPort = 1025;
            }
            if (!environmentHelper.RunningOnTeamCity())
            {
                return expectedStartPort;
            }

            var properties = IPGlobalProperties.GetIPGlobalProperties();

            IEnumerable<int> tcpConnectionPorts = properties.GetActiveTcpConnections()
                .Where(x => x.LocalEndPoint.Port >= expectedStartPort)
                .Select(x => x.LocalEndPoint.Port);

            IEnumerable<int> tcpListenerPorts = properties.GetActiveTcpListeners()
               .Where(x => x.Port >= expectedStartPort)
               .Select(x => x.Port);

            IEnumerable<int> udpListenerPorts = properties.GetActiveUdpListeners()
               .Where(x => x.Port >= expectedStartPort)
               .Select(x => x.Port);

            int port = Enumerable
                .Range(expectedStartPort, ushort.MaxValue)
                .Where(x => !tcpConnectionPorts.Contains(x))
                .Where(x => !tcpListenerPorts.Contains(x))
                .FirstOrDefault(x => !udpListenerPorts.Contains(x));

            return port;
        }
    }
}
