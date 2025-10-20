using BPCalculator.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace PageTest
{
    [TestClass]
    public sealed class Page_test
    {
        [TestMethod]
        public void TestMethod1()
        {
            var indexPage = new BloodPressureModel();
            indexPage.OnGet();
            indexPage.BP.Systolic = 120;
            indexPage.BP.Diastolic = 70;
            indexPage.OnPost();

            // Privacy page
            var privacyPage = new PrivacyModel(NullLogger<PrivacyModel>.Instance);
            privacyPage.OnGet();

            // Error page
            var error = new ErrorModel(new LoggerFactory().CreateLogger<ErrorModel>());
            error.PageContext = new PageContext { HttpContext = new DefaultHttpContext() };
            error.OnGet();
        }
    }
}
