using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.ExtentReport
{
    public abstract class ExtentManager
    {
        //Singelton Pattern
        private static ExtentReports Report;
        public static ExtentReports GetInstance()
        {
            if (Report == null)
            {
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string reportPath = projectPath + "Reports\\AutomationTestLogs.html";
                Report = new ExtentReports(reportPath, DisplayOrder.NewestFirst);
                Report.AddSystemInfo("Host Name", "Vinay Singh")
                    .AddSystemInfo("Environment", "QA")
                    .AddSystemInfo("User Name", "Vinay Singh");
                Report.LoadConfig(projectPath + "content-config.xml");
                
                Report = new ExtentReports(@"C:\\report\\report.html", DisplayOrder.OldestFirst);
                Report.LoadConfig(Directory.GetCurrentDirectory() + "extent-config.xml");
                Report.AddSystemInfo("Selenium version", "2.53.1.0").AddSystemInfo("Environment", "PROD");
            }
            return Report;
        }

        public static void TakeScreenshot(string fileName, IWebDriver driver)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenShot = screenshotDriver.GetScreenshot();
            screenShot.SaveAsFile("C:\\report\\" + fileName + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
        }

    }

    public abstract class ExtentBase
    {
        //protected static ExtentReports extent;
        //protected ExtentTest test;

        //public static ExtentReports GetInstance()
        //{
        //    if (extent == null)
        //    {
        //        extent = new ExtentReports(@"C:\\report\\report.html");
        //        extent.LoadConfig(Directory.GetCurrentDirectory() + "extent-config.xml");
        //        extent.AddSystemInfo("Selenium version", "2.53.1.0").AddSystemInfo("Environment", "PROD");
        //    }
        //    return extent;
        //}
       
        //[OneTimeSetUp]
        //public void FixtureInit()
        //{
        //    //GetInstance();
        //    //extent = ExtentManager.Instance;
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //    var status = TestContext.CurrentContext.Result.Outcome.Status;
        //    var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
        //            ? ""
        //            : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
        //    LogStatus logstatus;

        //    switch (status)
        //    {
        //        case TestStatus.Failed:
        //            logstatus = LogStatus.Fail;
        //            break;
        //        case TestStatus.Inconclusive:
        //            logstatus = LogStatus.Warning;
        //            break;
        //        case TestStatus.Skipped:
        //            logstatus = LogStatus.Skip;
        //            break;
        //        default:
        //            logstatus = LogStatus.Pass;
        //            break;
        //    }

        //    test.Log(logstatus, "Test ended with " + logstatus + stacktrace);

        //    extent.EndTest(test);
        //    extent.Flush();
        //}
    }
}
