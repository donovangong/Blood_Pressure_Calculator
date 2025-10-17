using BPCalculator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace PSTest
{
    [TestClass]
    public sealed class PS_test
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Program test
            var hostBuilder = Program.CreateHostBuilder(Array.Empty<string>());
            using var host = hostBuilder.Build();
            host.Start();
            host.StopAsync().Wait();

            //Startup test
            using var nhost = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder => {webBuilder.UseStartup<Startup>();}).Build();
            nhost.Start();
            nhost.StopAsync().Wait();
        }
    }
}
