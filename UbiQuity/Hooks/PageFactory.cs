using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace UbiQuity.Hooks
{
    public class PageFactory
    {
        public static IWebDriver InitiateWebDriver()
        {
            IWebDriver driver = null;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            //if (browser.Equals(settings.Browser))
            //{
            var path = Directory.GetCurrentDirectory();
            driver = new ChromeDriver(path);
            //}

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(CommonConstants.DriverSettings.DefaultWaitTime);
            string weburl = Configuration.GetSection("AppConfiguration")["WebsiteUrl"];
            driver.Navigate().GoToUrl(weburl);
            driver.Manage().Window.Maximize();


            return driver;
        }

        public static IConfiguration Configuration { get; set; }
    }

}
