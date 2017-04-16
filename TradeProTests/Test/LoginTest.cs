using AutomationFramework.Helpers;
using TradeProTests.Pages;
using NUnit.Framework;
using AutomationFramework.Base;
using System.IO;
using RelevantCodes.ExtentReports;
using System;
using TradeProTests.TestDataAccess;
using System.Threading;
using System.Linq;
using LinqToExcel;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework.Internal;
using NUnit.Framework.Interfaces;
using System.Collections;
using NUnit.Framework.Internal.Builders;
using AutomationFramework.TestDataDriven;


/*-----------------------------------------------------------------------------------------------
-- Automation TestID: TC02
--TestID:2154
-- CREATED BY: Sendil
-- Test Location:Login
-- Sub Test Location:Login
-- Test Description:Login
-- Expected Result:The user should be taken to the Home Dashboard of their Default Manufacturer         
-- DATE CREATED: 
-- REVISION LOG
------------------------------------------------------------------------------ --------------------------*/

namespace TradeProTests.Test
{
   [TestFixture]
    public class LoginTest : Hooks
    {
        //To do list: Code change for browsertype when the test excute in parllel browser
        public LoginTest()
            : base(AutomationSetting.BrowserToRunWith)
        {
             ExcelHelper.PopulateInCollection(Directory.GetCurrentDirectory() + FilePath.DataDrivenExcelFileLocation);
             //ExcelHelper.PopulateInCollection(@"C:\Development\NitikaGoyal\TFS_Tradepro7.6" + FilePath.DataDrivenExcelFileLocation);
            GoTo("https://authasidevtm1b.answerssystems.com/");
            page = PageGenerator.ApplicationLoginPage;
           
        }
        private ExtentReports Report;
        private ExtentTest TestStep;
        private LoginPage page;
        string Path = FilePath.TestCaseExcel;
       [Test]
        public void TC02_LoginToApplication()
        {
            List<string[]> tests = new List<string[]>(new CustomTestCaseSource(typeof(TestDataFactory)).BuildFrom("TC02_LoginToApplication",Path));
           foreach (string[] data in tests)
            {
                if (data.Length != 0)
                {
                    page.LoginToApplication(data);
                }
            }
            Assert.AreEqual("http://fsasidevtm1b.answerssystems.com/Home/", Hooks.Driver.Url,"login Failed ! Please check Username and Password");
        }

        /*---- -----------------------------------------------------------------
        -- Automation TestID: TC05
        --TestID:2155
        -- CREATED BY: Sendil
        -- Test Location:Login
        -- Sub Test Location:Login
        -- Test Description:Forget Password
        -- Expected Result:The user should receive an email with a generic password. Logging back in using that password should take the user to the change password screens         
        -- DATE CREATED: 
        -- REVISION LOG
        --------------------------------------------------------------------------*/


        //[Test]
        public void TC05_ForgetPassword()
        {
            GoTo("https://authasidevtm1b.answerssystems.com/password.aspx");
            //Console.WriteLine("Forget Password link clicked");
            //try
            //{
            //    Assert.AreEqual("Password Request", Hooks.Driver.Title);
            //    Console.WriteLine("Forget Password link verified");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}
            page.ForgetPassword("TestSSSSSSsssss");
            page.ClickOkBtn();
            Thread.Sleep(3000);
        }
        [OneTimeTearDown]
        public void TearDownLoginTest()
        {
            TearDown();
           
        }
        //[TearDown]
        //public void TearDownLoginTest()
        //{
        //    TearDown(Report, TestStep);
        //}
    }
}
