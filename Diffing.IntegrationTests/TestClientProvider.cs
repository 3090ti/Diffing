using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Diffing.IntegrationTests
{
    public class TestClientProvider : IDisposable
    {
        public HttpClient HttpClient { get; private set; }
        private TestServer testServer { get; set; }

        public TestClientProvider()
        {
            testServer = new TestServer(new WebHostBuilder().UseStartup<DiffingAPI.Startup>());
            HttpClient = testServer.CreateClient();
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
            testServer?.Dispose();
        }
    }
}
