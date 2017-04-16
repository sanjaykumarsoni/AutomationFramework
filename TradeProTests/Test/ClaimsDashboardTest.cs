using AutomationFramework.Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeProTests.Pages;
using AutomationFramework.Extensions;
using AutomationFramework.Helpers;
using System.IO;
using TradeProTests.TestDataAccess;

namespace TradeProTests.Test
{
    [TestFixture]
    [Category("Claims Dashboard Test")]
    public class ClaimsDashboardTest : Hooks
    {
        private ClaimsDashboardPage page;
        public ClaimsDashboardTest()
            : base(AutomationSetting.BrowserToRunWith)
        {
            ExcelHelper.PopulateInCollection(Path.GetFullPath(Directory.GetCurrentDirectory() + FilePath.DataDrivenExcelFileLocation));
            GoTo("https://authasidevtm1b.answerssystems.com/");
            Console.WriteLine("Application has been loaded");
            string[] data = new[] { "Automation.tester@afsi.com", "Testing1" };
            PageGenerator.ApplicationLoginPage.LoginToApplication(data);
            Console.WriteLine("Login to the application");
            GoTo("http://fsasidevtm1b.answerssystems.com/Claims/");
            Console.WriteLine("Navigate to the contracts dashboard page");
            page = PageGenerator.ClaimsDashboardPage;
        }
        
        [Test]
        public void ClaimTypeMultiSelectDropdown()
        {
            Hooks.Driver.WaitForJQuery(1000);;
            page.AdvancedSearchlink();
            page.ClaimTypeDropDown();
        }
    
    }
}
