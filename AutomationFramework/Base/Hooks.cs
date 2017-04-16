using AutomationFramework.ExtentReport;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutomationFramework.Base
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        IE
    }

    /// <summary>
    /// framework gets life from here. 
    /// The Driver instance will be used in any test project as a single instance untill it gets close their driver itself.
    /// </summary>
    public class Hooks : ExtentManager
    {
        public static ExtentReports Report;
        public ExtentTest TestStep = null;
        private BrowserType _browserType;
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver driver;

        /// <summary>
        /// Driver instance to be used throught out the test project once instantiated and untill it disposed it self.
        /// </summary>
        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                //**give this code once environment set up made for central wait*** 
                //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                return driver;
            }
            private set
            {
                driver = value;
            }
        }
        private static string _DriverServicesPathLocation = string.Empty;

        /// <summary>
        /// constructor to invoked the tester choiced browser.
        /// </summary>
        /// <param name="browser">browser name</param>
        public Hooks(String browser)
        {
            if (browser.ToLower().Contains("chrome"))
            {
                _browserType = BrowserType.Chrome;
            }
            else if (browser.ToLower().Contains("ie"))
            {
                _browserType = BrowserType.IE;
            }
            else if (browser.ToLower().Contains("firefox"))
            {
                _browserType = BrowserType.Firefox;
            }
            Report = ExtentManager.GetInstance();
            ChooseDriverInstance(_browserType);
        }

        /// <summary>
        /// based on browser, create the instance of browser which is an excutable file.
        /// </summary>
        /// <param name="browserType"></param>
        private void ChooseDriverInstance(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Firefox:
                    if (driver == null)
                    {
                        driver = new FirefoxDriver();
                        Driver = driver;
                        driver.Manage().Window.Maximize();
                        Drivers.Add("Firefox", Driver);
                    }
                    break;

                case BrowserType.IE:
                    if (driver == null)
                    {
                        driver = new InternetExplorerDriver(Directory.GetCurrentDirectory() + FrameworkSettings.DriverServicesPath);
                        Driver = driver;
                        driver.Manage().Window.Maximize();
                        Drivers.Add("IE", Driver);
                    }
                    break;

                case BrowserType.Chrome:
                    if (driver == null)
                    {
                        driver = new ChromeDriver(Directory.GetCurrentDirectory() + FrameworkSettings.DriverServicesPath);
                        Driver = driver;
                        driver.Manage().Window.Maximize();
                        Drivers.Add("Chrome", Driver);
                    }
                    break;
            }
        }

        public static void GoTo(string url)
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Url = url;
            }
        }

        public String FormatTraceMsg(string msg)
        {
            String traceMsg = string.IsNullOrEmpty(msg) ? "" : string.Format("<pre>{0}</pre>", msg);
            return traceMsg;
        }

        public void TearDown(ExtentReports report)
        {
            // ExtentManager.TakeScreenshot("HtmlView", Driver);
            report.Flush();
            CloseAllDrivers();
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys)
            {
                Drivers[key].Close();
                Drivers[key].Quit();
                //Drivers.Remove(key);
                driver = null;
            }
        }

        public void TearDown()
        {
            CloseAllDrivers();
        }

    }
}
