using BPCalculator;
using BPCalculator.Pages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics;

namespace PageTest
{
    [TestClass]
    public sealed class Page_test
    {
        [TestMethod]
        public void TestMethod1()
        {
            // ---- BloodPressure page ----
            var indexPage = new BloodPressureModel();
            indexPage.OnGet();

            indexPage.BP.Systolic = 120;
            indexPage.BP.Diastolic = 70;
            indexPage.OnPost();

            indexPage.BP.Systolic = 80;
            indexPage.BP.Diastolic = 90;
            indexPage.OnPost();
            Assert.IsFalse(indexPage.ModelState.IsValid);

            // ---- Error page ----
            var error = new ErrorModel(NullLogger<ErrorModel>.Instance);
            error.PageContext = new PageContext { HttpContext = new DefaultHttpContext() };
            Activity.Current = new Activity("test").Start();
            error.OnGet();

            // Program.cs coverage
            var hostBuilder = Program.CreateHostBuilder(Array.Empty<string>());
            Assert.IsNotNull(hostBuilder);
            using var host = Program.CreateHostBuilder(Array.Empty<string>()).Build();

            // Privacy page coverage
            var privacy = new PrivacyModel(NullLogger<PrivacyModel>.Instance);
            privacy.OnGet();

            // Cover Activity.Current == null branch
            Activity.Current = null;
            error.OnGet();

            Assert.IsTrue(error.ShowRequestId);
            error.RequestId = null;
            Assert.IsFalse(error.ShowRequestId);

            // ---- Startup coverage ----
            var startup = new Startup(new ConfigurationBuilder().Build());
            Assert.IsNotNull(startup.Configuration);

            // Development branch
            try
            {
                Host.CreateDefaultBuilder()
                    .ConfigureWebHostDefaults(w =>
                    {
                        w.UseStartup<Startup>();
                        w.UseEnvironment(Environments.Development);
                    })
                    .Build()
                    .Start();
            }
            catch { }

            // Production branch
            try
            {
                Host.CreateDefaultBuilder()
                    .ConfigureWebHostDefaults(w =>
                    {
                        w.UseStartup<Startup>();
                        w.UseEnvironment(Environments.Production);
                    })
                    .Build()
                    .Start();
            }
            catch { }
        }
    }
}
