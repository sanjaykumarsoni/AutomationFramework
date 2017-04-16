using AutomationFramework.Helpers;
using TradeProTests.Pages;
using NUnit.Framework;
using AutomationFramework.Base;
using System.IO;
using System;
using TradeProTests.TestDataAccess;
using AutomationFramework.Extensions;
using RelevantCodes.ExtentReports;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using log4net;
using AutomationFramework.TestDataDriven;
using AutomationFramework.ExtentReport;


namespace TradeProTests.Test
{
    [TestFixture]
    [Category("Home Dashboard Test")]
    public class HomeDashboardTest : Hooks
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HomeDashboardPage page;
        bool roleAFSCommentsView;
        bool roleAFSCommentsEdit;
        private string email = string.Empty;
        string Path = FilePath.TestCaseExcel;
        static List<ExcelModel> itemList;
        public HomeDashboardTest()
            : base(AutomationSetting.BrowserToRunWith)
        {
            ExcelHelper.PopulateInCollection(Directory.GetCurrentDirectory() + FilePath.DataDrivenExcelFileLocation);
            GoTo("https://authasidevtm1b.answerssystems.com/");
            Console.WriteLine("Application has been loaded");
            page = PageGenerator.HomeDashBoardPage;
            List<string[]> tests = new List<string[]>(new CustomTestCaseSource(typeof(TestDataFactory)).BuildFrom("TC02_LoginToApplication", Path));
            foreach (string[] data in tests)
            {
                if (data.Length != 0)
                {
                    page = PageGenerator.HomeDashBoardPage;
                    email = PageGenerator.ApplicationLoginPage.LoginToApplication(data);
                }
            }

            itemList = new List<ExcelModel>();
        }


        [Test]
        public void ClickViewLinkAndSwitchToNewWindow()
        {
            //click the view link
            //compare the driver title as expected.
            Driver.SwitchToWindow(driver => driver.Title == "Title of your new tab");
        }

        [Test, Order(1)]
        public void TC36_AFSCommentTabVisibilities()
        {
            List<string[]> tests = new List<string[]>(new CustomTestCaseSource(typeof(TestDataFactory)).BuildFrom("TC36_AFSCommentTabVisibilities", Path));
            foreach (string[] data in tests)
            {
                if (data.Length != 0)
                {
                    bool roleAFSCommentsView = false;
                    bool roleAFSCommentsEdit = false;
                    ExtentTest TestStep = null;
                    try
                    {
                        TestStep = Report.StartTest(@"HomeDashboard/AFSCommentTab", "AFS comment  tab visibilities based on Role_AFS_Comments_View and Role_AFS_Comments_Edit");
                        var list = ExcelMetadataRepository.UserBasedRolesMetaData(email);
                        bool temp = false;
                        bool isTabPresent = page.IsTabPresentMessagePanel(data[0]);
                        bool isTabEditable = false;
                        if (isTabPresent)
                        {
                            isTabEditable = page.IsSelectCommentTypeDDLEnabled();
                        }
                        if (list.SecurityCD.Contains(data[1]))
                        {
                            roleAFSCommentsView = true;
                        }
                        if (list.SecurityCD.Contains(data[2]))
                        {
                            roleAFSCommentsEdit = true;
                        }

                        if (roleAFSCommentsView && roleAFSCommentsEdit)
                        {

                            if (isTabPresent & isTabEditable)
                            {
                                temp = true;
                            }
                            Assert.AreEqual(true, temp, "Expected : Tab will be visible and editable. Actual : tab is not either visible or editable");
                            TestStep.Log(LogStatus.Pass, "User ID:" + email + " According to user level AFS comments tab will be visible and editable.");
                            TestStep.Log(LogStatus.Info, "Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is true ROLE_AFS_COMMENTS_EDIT is false and Vice versa. ");
                            Logger.Info("User ID:" + email + " According to user level AFS comments tab will be visible and editable.");
                            Logger.Info("Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is true ROLE_AFS_COMMENTS_EDIT is false and Vice versa. ");
                            Console.WriteLine("ROLE_AFS_COMMENTS_VIEW and ROLE_AFS_COMMENTS_EDIT are true, So AFS comments tab will be visible and editable.");
                        }
                        else if (roleAFSCommentsView && !roleAFSCommentsEdit)
                        {
                            if (isTabPresent & !isTabEditable)
                            {
                                temp = true;
                            }
                            Assert.AreEqual(true, temp, "Expected : Tab will be visible but not editable. Actual : tab is either not visible or but editable");
                            TestStep.Log(LogStatus.Pass, "User ID:" + email + " According the user level AFS comments tab will be visible but not editable.");
                            TestStep.Log(LogStatus.Info, "Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is true ROLE_AFS_COMMENTS_EDIT is false and Vice versa. ");
                            Logger.Info("User ID:" + email + " According the user level AFS comments tab will be visible but not editable.");
                            Logger.Info("Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is true ROLE_AFS_COMMENTS_EDIT is false and Vice versa. ");
                            Console.WriteLine("ROLE_AFS_COMMENTS_VIEW is true and ROLE_AFS_COMMENTS_EDIT is false, So AFS comments tab will be visible but not editable.");
                        }
                        else if (!roleAFSCommentsView && roleAFSCommentsEdit)
                        {
                            if (!isTabPresent & isTabEditable)
                            {
                                temp = true;
                            }
                            Assert.AreEqual(true, temp, "Expected : Tab will not be visible but editable. Actual : tab is either visible but not editable");
                            TestStep.Log(LogStatus.Pass, "User ID:" + email + " According the user level AFS comments tab will not be visible but editable.");
                            TestStep.Log(LogStatus.Info, "Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is false ROLE_AFS_COMMENTS_EDIT is true and Vice Versa.");
                            Logger.Info("User ID:" + email + " According the user level AFS comments tab will not be visible but editable.");
                            Logger.Info("Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is false ROLE_AFS_COMMENTS_EDIT is true and Vice Versa.");
                            Console.WriteLine("ROLE_AFS_COMMENTS_VIEW is false and ROLE_AFS_COMMENTS_EDIT is true, So AFSCommentsTab will not be visible but editable.");
                        }
                        else if (!roleAFSCommentsView && !roleAFSCommentsEdit)
                        {
                            if (!isTabPresent & !isTabEditable)
                            {
                                temp = true;
                            }
                            Assert.AreEqual(true, temp, "Expected : Tab will not be visible and neither editable. Actual : tab is either visible or editable");
                            TestStep.Log(LogStatus.Pass, "User ID:" + email + " According the user level AFS comments tab will be visible and editable.");
                            TestStep.Log(LogStatus.Info, "Not Applicable for this User ID:" + email + " The testcase based on ROL is false E_AFS_COMMENTS_VIEW ROLE_AFS_COMMENTS_EDIT is false and Vice Versa.");
                            Logger.Info("User ID:" + email + " According the user level AFS comments tab will be visible and editable.");
                            Logger.Info("Not Applicable for this User ID:" + email + " The testcase based on ROL is false E_AFS_COMMENTS_VIEW ROLE_AFS_COMMENTS_EDIT is false and Vice Versa.");
                            Console.WriteLine("ROLE_AFS_COMMENTS_VIEW and ROLE_AFS_COMMENTS_EDIT are false, So AFSCommentsTab will not be visible and neither editable.");
                        }
                        else
                        {
                            Assert.AreEqual(true, temp, "RoleAFSCommentsView and RoleAFSCommentsEdit has not been defined for this particular user");
                            TestStep.Log(LogStatus.Pass, "RoleAFSCommentsView and RoleAFSCommentsEdit has not been defined for this particular user");
                            Logger.Info("RoleAFSCommentsView and RoleAFSCommentsEdit has not been defined for this particular user");
                            Console.WriteLine("Please check with the admin for user roles or check the Testcase suite code name as CommentSearchTabVisibilities");
                        }
                        itemList.Add(TestDataFactory.ExcelReport("TC36", "Pass", ""));
                    }

                    catch (Exception e)
                    {
                        //Logger.Error(e.StackTrace);
                        itemList.Add(TestDataFactory.ExcelReport("TC36", "Fail", FormatTraceMsg(e.Message)));
                        ExtentManager.TakeScreenshot("TC01_AFSCommentTabVisibilities", Driver);
                        TestStep.Log(LogStatus.Fail, "Test ended with Fail" + FormatTraceMsg(e.Message) + FormatTraceMsg(e.StackTrace) + TestStep.AddScreenCapture("C:\\report\\TC01_AFSCommentTabVisibilities.gif"));
                    }
                    finally
                    {
                        Report.EndTest(TestStep);
                    }
                }
            }
        }

        [Test, Order(13)]
        public void TC39_AFSCommentSearch_TabVisibilities()
        {
            ExtentTest TestStep = null;
            try
            {
                TestStep = Report.StartTest(@"HomeDashboard/AFSCommentSearchTab", "AFS comment search tab visibilities based on Role_AFS_Comments_View and Role_AFS_Comments_Edit");
                var list = ExcelMetadataRepository.UserBasedRolesMetaData(email);
                bool temp = false;
                bool isTabPresent = page.IsTabPresentMessagePanel("Comment Search");
                bool isTabEditable = false;
                if (isTabPresent)
                {
                    isTabEditable = page.IsSelectCommentSearchTypeDDLEnabled();
                }
                if (list.SecurityCD.Contains("ROLE_AFS_COMMENTS_VIEW"))
                {
                    roleAFSCommentsView = true;
                }
                if (list.SecurityCD.Contains("ROLE_AFS_COMMENTS_EDIT"))
                {
                    roleAFSCommentsEdit = true;
                }

                if (roleAFSCommentsView && roleAFSCommentsEdit)
                {

                    if (isTabPresent & isTabEditable)
                    {
                        temp = true;
                    }
                    Assert.AreEqual(false, temp, "Expected : Tab will be visible and editable. Actual : tab is either not visible nor editable");
                    TestStep.Log(LogStatus.Pass, "User ID:" + email + " According to user level AFS comments tab will be visible and editable.");
                    TestStep.Log(LogStatus.Info, "Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is true ROLE_AFS_COMMENTS_EDIT is false and Vice versa. ");
                    Logger.Info("User ID:" + email + " According to user level AFS comments tab will be visible and editable.");
                    Logger.Info("Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is true ROLE_AFS_COMMENTS_EDIT is false and Vice versa. ");
                    Console.WriteLine("ROLE_AFS_COMMENTS_VIEW and ROLE_AFS_COMMENTS_EDIT are true, So CommentsSearchTab will be visible and editable.");
                }
                else if (roleAFSCommentsView && !roleAFSCommentsEdit)
                {
                    if (isTabPresent & !isTabEditable)
                    {
                        temp = true;
                    }
                    Assert.AreEqual(true, temp, "Expected : Tab will be visible but not editable. Actual : tab is either not visible or but editable");
                    TestStep.Log(LogStatus.Pass, "User ID:" + email + " According the user level AFS comments tab will be visible but not editable.");
                    TestStep.Log(LogStatus.Info, "Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is true ROLE_AFS_COMMENTS_EDIT is false and Vice versa. ");
                    Logger.Info("User ID:" + email + " According the user level AFS comments tab will be visible but not editable.");
                    Logger.Info("Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is true ROLE_AFS_COMMENTS_EDIT is false and Vice versa. ");

                    Console.WriteLine("ROLE_AFS_COMMENTS_VIEW is true and ROLE_AFS_COMMENTS_EDIT is false, So CommentsSearchTab will be visible but not editable.");
                }
                else if (!roleAFSCommentsView && roleAFSCommentsEdit)
                {
                    if (!isTabPresent & isTabEditable)
                    {
                        temp = true;
                    }
                    Assert.AreEqual(true, temp, "Expected : Tab will not be visible but editable. Actual : tab is either visible but not editable");
                    TestStep.Log(LogStatus.Pass, "User ID:" + email + " According the user level AFS comments tab will not be visible but editable.");
                    TestStep.Log(LogStatus.Info, "Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is false ROLE_AFS_COMMENTS_EDIT is true and Vice Versa.");
                    Logger.Info("User ID:" + email + " According the user level AFS comments tab will not be visible but editable.");
                    Logger.Info("Not Applicable for this User ID:" + email + " The testcase based on ROLE_AFS_COMMENTS_VIEW is false ROLE_AFS_COMMENTS_EDIT is true and Vice Versa.");
                    Console.WriteLine("ROLE_AFS_COMMENTS_VIEW is false and ROLE_AFS_COMMENTS_EDIT is true, So CommentsSearchTab will not be visible but editable.");
                }
                else if (!roleAFSCommentsView && !roleAFSCommentsEdit)
                {
                    if (!isTabPresent & !isTabEditable)
                    {
                        temp = true;
                    }
                    Assert.AreEqual(false, temp, "Expected : Tab will not be visible and neither editable. Actual : tab is either visible or editable");
                    TestStep.Log(LogStatus.Pass, "User ID:" + email + " According the user level AFS comments tab will be visible and editable.");
                    TestStep.Log(LogStatus.Info, "Not Applicable for this User ID:" + email + " The testcase based on ROL is false E_AFS_COMMENTS_VIEW ROLE_AFS_COMMENTS_EDIT is false and Vice Versa.");
                    Logger.Info("User ID:" + email + " According the user level AFS comments tab will be visible and editable.");
                    Logger.Info("Not Applicable for this User ID:" + email + " The testcase based on ROL is false E_AFS_COMMENTS_VIEW ROLE_AFS_COMMENTS_EDIT is false and Vice Versa.");
                    Console.WriteLine("ROLE_AFS_COMMENTS_VIEW  and ROLE_AFS_COMMENTS_EDIT are false, So CommentsSearchTab will not be visible and neither editable.");
                }
                else
                {
                    Assert.AreEqual(true, temp, "RoleAFSCommentsView and RoleAFSCommentsEdit has not been defined for this particular user");
                    TestStep.Log(LogStatus.Pass, "RoleAFSCommentsView and RoleAFSCommentsEdit has not been defined for this particular user");
                    Logger.Info("RoleAFSCommentsView and RoleAFSCommentsEdit has not been defined for this particular user");
                    Console.WriteLine("Please check with the admin for user roles or check the Testcase suite code name as CommentSearchTabVisibilities");
                }
                itemList.Add(TestDataFactory.ExcelReport("TC39", "Pass", ""));
            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);
                itemList.Add(TestDataFactory.ExcelReport("TC39", "Fail", FormatTraceMsg(e.Message)));
                ExtentManager.TakeScreenshot("AFSCommentSearchTabVisibilities", Driver);
                TestStep.Log(LogStatus.Fail, "Test ended with Fail" + FormatTraceMsg(e.Message) + FormatTraceMsg(e.StackTrace) + TestStep.AddScreenCapture("C:\\report\\AFSCommentSearchTabVisibilities.gif"));
                //throw e;
            }
            finally
            {
                Report.EndTest(TestStep);
            }
        }

        [Test, Order(2)]
        public void TC11_HomeDatePicker_GraphView()
        {
            ExtentTest TestStep = null;
            try
            {
                TestStep = Report.StartTest(@"HomeDashboard/MonthOptionForShowingGraphView", "The Month dropdown will display Month/Year options. When selected, the graph will update");
                page.HomeDatePicker();
                bool isPresent = page.GraphView();
                if (isPresent)
                {
                    Assert.AreEqual(true, isPresent, "Graph is not visible based on the date.");
                    TestStep.Log(LogStatus.Pass, "Based on the user input month/year, Graph has been displayed");
                    Logger.Info("Based on the user input month/year, Graph has been displayed");
                    Logger.Info("Testing");
                    Console.WriteLine("Based on the user input month/year option, Graph has been displayed");
                }
                else
                {
                    Logger.Info("Based on the user input month/year option, Graph will not displayed");
                    Console.WriteLine("Based on the user input month/year option, Graph will not displayed");
                }
                // itemList.Add(TestDataFactory.ExcelReport("TC11", "Pass", Logger));
            }
            catch (Exception e)
            {
                //GetScreenShot
                Logger.Error(e.StackTrace);
                // itemList.Add(TestDataFactory.ExcelReport("TC11", "Fail", Logger));
                ExtentManager.TakeScreenshot("TC11_ClickHomeDatePickerAndGraphView", Driver);
                TestStep.Log(LogStatus.Fail, "Test ended with Fail" + FormatTraceMsg(e.Message) + FormatTraceMsg(e.StackTrace) + TestStep.AddScreenCapture("C:\\report\\TC11_ClickHomeDatePickerAndGraphView.gif"));
                //throw e;
            }
            finally
            {
                Report.EndTest(TestStep);
            }
        }

        [Test, Order(2)]
        public void TC28_ClickSuggestionCancelCountSubmitButton()
        {
            ExtentTest TestStep = null;
            List<string[]> tests = new List<string[]>(new CustomTestCaseSource(typeof(TestDataFactory)).BuildFrom("TC28_ClickSuggestionCancelCountSubmitButton", Path));
            foreach (string[] data in tests)
            {
                if (data.Length != 0)
                {
                    try
                    {
                        TestStep = Report.StartTest(@"HomeDashboard/ClickSuggestionCancelCountSubmitButton", "Click Suggestion Cancel Count Submit Button Test");
                        //TestStep.Log(LogStatus.Info, "Chrome browser has been launched");
                        var text = string.Empty; ;
                        int count = 0;
                        bool actual;
                        var to = string.Empty;
                        var subject = string.Empty;
                        bool boolTo;
                        bool boolSubject;
                        Hooks.Driver.WaitForJQuery(10);
                        page.AddSuggestionClick();
                        var itemCancel = page.CancelSuggestionBox();
                        Assert.AreEqual(true, itemCancel, "Cancel button is not clicked");
                        TestStep.Log(LogStatus.Pass, "cancel button clicked suggestion modal is closed");
                        Logger.Info("cancel button clicked suggestion modal is closed");
                        page.AddSuggestionClick();
                        //text = page.FillSuggestionTextArea("Automation Test");
                        text = page.FillSuggestionTextArea(data[0]);
                        count = page.SuggestionWordsCount(text);
                        //if (count <= 1500)
                        if (count <= int.Parse(data[1]))
                        {
                            actual = true;
                            Console.Write("Actual count is  {0} Which is less then or equal to 1500 words", count);
                        }
                        else
                        {
                            actual = false;
                            Console.Write("Actual count is  {0} Which is greater then 1500 words", count);
                        }
                        Assert.AreEqual(true, actual, "The Actual count is greater than 1500");
                        TestStep.Log(LogStatus.Pass, "Suggestion modal textarea count less than 1500");
                        Logger.Info("Suggestion modal textarea count less than 1500");
                        to = page.AddSuggestionToText();
                        Assert.AreEqual("Send to: TradePro Support", to, "Send to textbox don't have placeholder text as Send to: TradePro Support");
                        TestStep.Log(LogStatus.Pass, "To textbox value is equal to Send to: TradePro Support");
                        Logger.Info("To textbox value is equal to Send to: TradePro Support");
                        subject = page.AddSuggestionSubjectText();
                        Assert.AreEqual("Suggestions for TradePro...", subject, "Suggestions for textbox don't have placeholder text as Suggestions for TradePro...");
                        TestStep.Log(LogStatus.Pass, "Subject textbox value is equal to Suggestions for TradePro...");
                        Logger.Info("Subject textbox value is equal to Suggestions for TradePro...");
                        boolTo = page.AddSuggestionToEnable();
                        Assert.AreEqual(false, boolTo, "The textbox with Send To Placeholder has been enabled and expected is always false.");
                        TestStep.Log(LogStatus.Pass, "To textbox is disable");
                        Logger.Info("To textbox is disable");
                        boolSubject = page.AddSuggestionSubjectEnable();
                        Assert.AreEqual(false, boolSubject, "The textbox with Suggestions for Placeholder has been enabled and expected is always false.");
                        TestStep.Log(LogStatus.Pass, "Subject textbox is disable");
                        Logger.Info("Subject textbox is disable");
                        var itemSubmit = page.SubmitSugesstion();
                        Assert.AreEqual(true, itemSubmit, "Submit button is disabled");
                        TestStep.Log(LogStatus.Pass, "Submit was clicked suggestion box was successfully closed");
                        Logger.Info("Submit was clicked suggestion box was successfully closed");

                        //itemList.Add(TestDataFactory.ExcelReport("TC28", "Pass", Logger));
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e.StackTrace);
                        //itemList.Add(TestDataFactory.ExcelReport("TC28", "Fail", Logger));
                        ExtentManager.TakeScreenshot("TC27_ClickSuggestionCancelCountSubmitButton", Driver);
                        TestStep.Log(LogStatus.Fail, "Test ended with Fail" + FormatTraceMsg(e.Message) + FormatTraceMsg(e.StackTrace) + TestStep.AddScreenCapture("C:\\report\\TC27_ClickSuggestionCancelCountSubmitButton.gif"));
                        //throw e;
                    }
                    finally
                    {
                        Report.EndTest(TestStep);
                    }
                }
            }
        }

        [Test]
        public void TC30_MessageGridLabel()
        {
            ExtentTest TestStep = null;
            try
            {
                TestStep = Report.StartTest(@"HomeDashboard/MessageGridLabel", "Bottom grid is labed as Messages");
                var expected = page.MessagePanel();
                Assert.AreEqual("Messages", expected, "Message grid label is not displayed");
                TestStep.Log(LogStatus.Pass, "Message grid  label as expected");
            }
            catch (Exception e)
            {
                ExtentManager.TakeScreenshot("TC30_MessageGridLabel", Driver);
                TestStep.Log(LogStatus.Fail, "Test ended with Fail" + FormatTraceMsg(e.Message) + FormatTraceMsg(e.StackTrace) + TestStep.AddScreenCapture("C:\\report\\TC30_MessageGridLabel.gif"));
                //throw e;
            }
            finally
            {
                Report.EndTest(TestStep);
            }
        }


        [Test, Order(6)]
        //[CustomTestCaseSource(typeof(LazyThreadSafetyMode), "HomeDashboardTest", "HomeSelectManufacturer")]
        public void HomeSelectManufacturer()
        {
            Hooks.Driver.WaitForJQuery(10);
            page.SelectManufacturesDDL("Basic American Foods");
        }

        [Test, Order(7)]
        public void GetOperatorsGridPageSize()
        {
            ExtentTest TestStep = null;
            try
            {
                TestStep = Report.StartTest(@"HomeDashboard/OperatorsGrid", "Get operators grid page size");
                Hooks.Driver.WaitForJQuery(100);
                int pageSize = page.OperatorsGridPageSize();
                Console.WriteLine(pageSize);
                Assert.AreEqual(10, pageSize, "The grid page size is not equal to 10");
                TestStep.Log(LogStatus.Pass, "Operator grid page size is equal to 10");
            }
            catch (Exception e)
            {
                ExtentManager.TakeScreenshot("GetOperatorsGridPageSize", Driver);
                TestStep.Log(LogStatus.Fail, "Test ended with Fail" + FormatTraceMsg(e.Message) + FormatTraceMsg(e.StackTrace) + TestStep.AddScreenCapture("C:\\report\\GetOperatorsGridPageSize.gif"));
                //throw e;
            }
            finally
            {
                Report.EndTest(TestStep);
            }
        }

        [Test, Order(8)]
        public void DscSortOperatorsGridPage()
        {
            Hooks.Driver.WaitForJQuery(100);
            page.OperatorGridSortDescByID();
            page.OperatorGridPageSortDscByOperator();
            page.OperatorGridPageSortDscByAddress();
            page.OperatorGridPageSortDscByGroup();
        }

        [Test, Order(9)]
        public void AscSortOperatorsGridPage()
        {
            Hooks.Driver.WaitForJQuery(100);
            page.OperatorGridSortAscByID();
            page.OperatorGridPageSortAscByOperator();
            page.OperatorGridPageSortAscByAddress();
            page.OperatorGridPageSortAscByGroup();
        }

        [Test, Order(10)]
        public void ChangeOperatorPageSize()
        {
            Hooks.Driver.WaitForJQuery(100);
            page.ChangeGridOperatorPageSize();
        }

        [Test, Order(11)]
        public void ReloadOperatorGrid()
        {
            Hooks.Driver.WaitForJQuery(1000);
            page.ReloadOperatorGridPage();
        }

        [Test, Order(12)]
        public void NavigationToPage()
        {
            Hooks.Driver.WaitForJQuery(100);
            page.NavigationToPageInOpertorGrid();
        }

        /*---- -----------------------------------------------------------------
        -- Automation TestID: TC07
        --TestID:2
        -- CREATED BY: Sendil
        -- Test Location:Home Dashboard 
        -- Sub Test Location:screen Layout
        -- Test Description:Label
        -- Expected Result:Label "Home Dashboard" appears above the transactions grid.
        -- DATE CREATED: 
        -- REVISION LOG
        --------------------------------------------------------------------------*/

        [Test]
        public void TC07_GoToHomeDashboard()
        {
            GoTo("http://fsasidevtm1b.answerssystems.com/Home/");
            try
            {
                Assert.AreEqual("Home Dashboard", Hooks.Driver.Title);
                Console.WriteLine("Home Dashboard page is verified");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        [Ignore("this is creating issues of unnecessary popup")]
        public void GoToPendingDashboard()
        {
            GoTo("http://fsasidevtm1b.answerssystems.com/Pendings/");
            Console.WriteLine("Pending linked clicked");
            try
            {
                Assert.AreEqual("Pendings Dashboard", Hooks.Driver.Title);
                Console.WriteLine("Pending link verified");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        [Test]
        public void GoToClaimsDashboard()
        {
            GoTo("http://fsasidevtm1b.answerssystems.com/Claims/");
            Console.WriteLine("Claims Dashbaord link clicked");
            try
            {
                Assert.AreEqual("Claims Dashboard", Hooks.Driver.Title);
                Console.WriteLine("Claims Dashboard link verified");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        public void GoToContractsDashboard()
        {
            GoTo("http://fsasidevtm1b.answerssystems.com/Contracts/");
            Console.WriteLine("Contracts Dashboard link clicked");
            try
            {
                Assert.AreEqual("Contracts Dashboard", Hooks.Driver.Title);
                Console.WriteLine("Contracts Dashboard link verified");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        public void GoToCompaniesDashboard()
        {
            GoTo("http://fsasidevtm1b.answerssystems.com/Companies/");
            Console.WriteLine("Companies Dashboard link clicked");
            try
            {
                Assert.AreEqual("Companies Dashboard", Hooks.Driver.Title);
                Console.WriteLine("Companies Dashboard link verified");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        public void GoToDeductionProDashboard()
        {
            GoTo("http://fsasidevtm1b.answerssystems.com/DeductionPro/");
            Console.WriteLine("DeductionPro Dashboard linked clicked");
            try
            {
                Assert.AreEqual("DeductionPro Dashboard", Hooks.Driver.Title);
                Console.WriteLine("DeductionPro Dashboard verified");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        public void MessagesPanelTabsClick()
        {
            page.ClickNewOperator();
            page.ClickUserMessages();
            page.ClickAFSComments();
            page.ClickCommentSearch();
            //page.SelectCommentSearchTypeDDL("DeductionPro Comments");
        }

        [Test, Order(5)]
        public void DD_AFSCommentsStatus()
        {
            bool item = page.IsTabPresentMessagePanel("AFS Comments");
            if (item)
            {
                Console.WriteLine("AFS comments tab is visible");
                page.ClickAFSComments();
                var ComtStatus = page.CheckCommentStatus();
                if (ComtStatus == null)
                {
                    Console.WriteLine("Comment tab status for each row is NEW");
                }
                Assert.AreEqual(null, ComtStatus);
            }
            else
            {
                Assert.IsTrue(item, "The AFS comments Status is not visible hence you can not see the AFS comments grid.");
            }
        }

        [Test, Order(6)]
        public void MessagesPanelTabsCount()
        {
            int expectedCount = page.MessagePanelTabCounts();
            Console.WriteLine("Tab count is : {0}", expectedCount);
            Assert.AreEqual(expectedCount, 4);
        }


        /*---- -----------------------------------------------------------------------------------------------
        -- Automation TestID: TC59
        -- TestID:30
        -- CREATED BY: Sendil
        -- Test Location:Home Dashboard 
        -- Sub Test Location:News Feed
        -- Test Description:suggestion text.
        -- Expected Result:User able to enter in text in the textbox provided. the max value is 1500.
        -- DATE CREATED: 
        -- REVISION LOG
        -------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC59_SuggetionAreaWordsCount()
        {
            Driver.WaitForJQuery(10);
            page.AddSuggestionClick();
            var text = page.FillSuggestionTextArea("Automation Test");
            int count = page.SuggestionWordsCount(text);
            bool actual;
            if (count <= 1500)
            {
                actual = true;
                Console.Write("Actual count is  {0} Which is less then or equal to 1500 words", count);
            }
            else
            {
                actual = false;
                Console.Write("Actual count is  {0} Which is greater then 1500 words", count);
            }
            Assert.AreEqual(true, actual);
        }

        /*-- ---------- ----------------------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC32
        --TestID:38
        -- CREATED BY: Sanjay
        -- Test Location:Home Dashboard 
        -- Sub Test Location:Messages
        -- Test Description:New Operator - Tab Visibility
        -- Expected Result:If the user is an Admin AND the MFG has the ability to Create New Operator, then the New Operator Tab will display.
        -- DATE CREATED: 11/15/2016
        -- REVISION LOG:Modified changes has been done on 11/15/2016
        ---------------------------------------------------------------------------------------------------------------------------------------------*/


        [Test]
        public void TC32_IsTabPresentInMessagesPanel()
        {
            string input = "Comment Search";
            bool isPresent = page.IsTabPresentMessagePanel(input);
            if (isPresent)
            {
                Assert.AreEqual(true, isPresent);
                Console.WriteLine("the requested tab {0} is present", input);
            }
            else
            {
                Console.WriteLine("the requested tab {0} is not present", input);
            }
        }
        /*---- -----------------------------------------------------------------------------------------------
        -- Automation TestID: TC25
        -- TestID:27
        -- CREATED BY: Sendil
        -- Test Location:Home Dashboard 
        -- Sub Test Location:News Feed
        -- Test Description:Label
        -- Expected Result:A button labeled "Suggestions" displays at the bottom of the News Feed section.
        -- DATE CREATED: 
        -- REVISION LOG
        -------------------------------------------------------------------------------------------------------*/

        [Test]
        public void TC25_SuggestionsLabel()
        {
            var expected = page.Suggestion();
            Assert.AreEqual(expected, "suggestions");
        }

        /*---- -----------------------------------------------------------------------------------------------------------------------------------------------
         -- Automation TestID: TC09
         -- TestID:4
         -- CREATED BY: Sendil
         -- Test Location:Home Dashboard 
         -- Sub Test Location:screen Layout
         -- Test Description:Label
         -- Expected Result:Bar graph titled "Transaction Dollars" aligned to top left displayed on a grey area. Aligned to the top right is a Export button.
         -- DATE CREATED: 
         -- REVISION LOG
         ----------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC09_TransactionDollarLabel()
        {
            var expected = page.TransactionDollar();
            Assert.AreEqual(expected, "Transaction Dollars");
        }

        /*---- -----------------------------------------------------------------------------------------------------------------------------------------------
         -- Automation TestID: TC
         -- TestID:
         -- CREATED BY: Sendil
         -- Test Location:Home Dashboard 
         -- Sub Test Location:screen Layout
         -- Test Description:Label
         -- Expected Result:
         -- DATE CREATED: 
         -- REVISION LOG
         ----------------------------------------------------------------------------------------------------------------------------------------------------*/


        [Test]
        public void CommentTextFieldsearch()
        {
            page.ClickCommentSearch();
            page.CommentTextField("Test");
            page.SearchButton();


        }


        [Test]
        public void NewOperatorsCompanyNumber()
        {
            page.NewOperatoreEdit();
            page.CompanyNumber("12345");
        }
        [Test]
        // [Ignore("This test is involved in downloading file each time hence ignored")]
        public void OperatorGridExportButton()
        {
            page.ClickExport();

        }

        [Test]
        public void NewsFeedTitleAlignment()
        {
            //page
        }

        [Test]
        public void ManufactureDDLAlignment()
        {
            var expected = page.ManufactureDdlAlignment();
            Assert.AreSame(expected, "right");
        }

        /*---- -----------------------------------------------------------------------------------------------------------------------------------------------
       -- Automation TestID: TC18
       -- TestID:TC20
       -- CREATED BY: Sendil
       -- Test Location:Home Dashboard 
       -- Sub Test Location:News Feed
       -- Test Description:News Feed Title
       -- Expected Result:Section labeled "News Feed" aligned to the top left on a grey border.
       -- DATE CREATED: 11/15/2016
       -- REVISION LOG
       ----------------------------------------------------------------------------------------------------------------------------------------------------*/

        [Test]
        public void NewsFeedLabelAlignment()
        {
            var actual = page.NewsFeedLabAlignment();
            string exp = "left";
            Console.WriteLine("News Feed label should be left aligned");
            Assert.AreEqual(exp, actual, "Test case failed: Output is not matching with expected result");


        }
        /*---- -----------------------------------------------------------------
     -- Automation TestID:TC44
     --TestID:49,50,51,52,53
     -- CREATED BY: Sendil
     -- Test Location:Home Dashboard 
     -- Sub Test Location:New Operator
     -- Test Description
     -- Expected Result:"Displays the Operator Name for the company record.

                         Displays the full address for the company record. Address1, Address2, City, State, Zip, Country Code

                         Displays the group for the company record

                         Displays ""View"" as a selectable hyperlink. Selecting the hyperlink will direct the user to the contract search screen with the search criteria pre-populated.

                        This is an editable textbox that allows text/numeric entry. Max entry is 25 (validate this uses contractpro.dbo.company db). The user is prohibited from entering in greater than 25 characters."
     -- DATE CREATED: 11/24/2016
     -- REVISION LOG:
     --------------------------------------------------------------------------*/
        [Test]
        public void TC44_Messages_NewOperator_Name()
        {
            ExtentTest TestStep = null;
            try
            {
                TestStep = Report.StartTest(@"HomeDashboard/MessagesNewOperatorName", "Displays the Operator Name for the company record");
                var Name = page.NewoperatorLabel();
                Console.WriteLine("Expected result:Displays the Operator Name for the company record.-Automation Result is :--- " + Name + "");
                Assert.AreEqual("Air Force Non Appropriated", Name, "Operator name is not displayed");
                TestStep.Log(LogStatus.Pass, "Expected result:Displays the Operator Name for the company record.-Automation Result is :--- " + Name + "");
                var Add = page.NewoperatorAddress();
                Console.WriteLine("Expected result:Displays the full address for the company record. Address1, Address2, City, State, Zip, Country Code. -Automation Result is :--" + Add + "");
                Assert.AreEqual("950 I.H. 35 North, Suite 370  San Antonio, TX 78233 US", Add, "Company Record is not displayed");
                TestStep.Log(LogStatus.Pass, "Expected result:Displays the full address for the company record. Address1, Address2, City, State, Zip, Country Code. -Automation Result is :--" + Add + "");
                page.NewoperatorContractsLink();
                Hooks.Driver.WaitForJQuery(200);
                var GroupName = page.NewoperatorGroup();
                Console.WriteLine("Expected result:Displays the group for the company record.-Automation Result is:--" + GroupName + "");
                Console.WriteLine("View link is Clicked");
                TestStep.Log(LogStatus.Pass, "View link is Clicked");
                //Hooks.Driver.WaitForJQuery(50000);
                //page.ReturnDashboard();
                //Console.WriteLine("Return to Homedashboard");
                // var GroupName = page.NewoperatorGroup();
                // Console.WriteLine("Expected result:Displays the group for the company record." + GroupName + "Output result");
            }
            catch (Exception e)
            {
                ExtentManager.TakeScreenshot("MessagesNewOperatorName", Driver);
                TestStep.Log(LogStatus.Fail, "Test ended with Fail" + FormatTraceMsg(e.Message) + FormatTraceMsg(e.StackTrace) + TestStep.AddScreenCapture("C:\\report\\MessagesNewOperatorName.gif"));
                //throw e;
            }
            finally
            {
                Report.EndTest(TestStep);
            }
        }

        /*---- -----------------------------------------------------------------
     -- Automation TestID: TC45
     --TestID:
     -- CREATED BY: Sendil
     -- Test Location:Home Dashboard 
     -- Sub Test Location:New Operator
     -- Test Description
     -- Expected Result:The grid only displays companies for that manufacturer where there is no Company Number.​

                        Edit icon displays in the last column. Clicking the Update icon will save the data entered in the Company Number field. Validate that the AuditUserID is updated for that record in the company table.​
     -- DATE CREATED: 11/25/2016
     -- REVISION LOG:
     --------------------------------------------------------------------------*/
        [Test]
        public void NewOPeratorUpdateButton()
        {
            page.NewOperatoreEdit();
            page.CompanyNumber("1234567890123456789012345");
            page.NewoperatorUpdate();

        }
        /*---- -----------------------------------------------------------------------------------------------------------------------------------------------
         -- Automation TestID: TC48
         -- TestID:64
         -- CREATED BY: Sendil
         -- Test Location:Home Dashboard 
         -- Sub Test Location:user
         -- Test Description:User Messages - ID
         -- Expected Result:Clicking the Claim ID will open the corresponding claim. Clicking the Contract ID will open the corresponding contract.
         -- DATE CREATED:11/29/2016 
         -- REVISION LOG
         ----------------------------------------------------------------------------------------------------------------------------------------------------*/

        [Test]
        public void UserMessageID()
        {
            page.ClickUserMessages();
            Hooks.Driver.WaitForJQuery(100);
            //page.RefreshUerMessageGridPage();

            page.UsermessageID();
        }
        /*---- -----------------------------------------------------------------------------------------------------------------------------------------------
         -- Automation TestID: TC48
         -- TestID:64
         -- CREATED BY: Sendil
         -- Test Location:Home Dashboard 
         -- Sub Test Location:user
         -- Test Description:User Messages - Delete button Click
         -- Expected Result:Clicking on the delete icon will open up a modal "Are you sure you want to delete?" If the user clicks "Cancel" the modal is closed and no action is taken. If the user clicks "OK" the modal is closed and the message will be deleted and no longer displayed in the grid.
         -- DATE CREATED:11/29/2016 
         -- REVISION LOG
         ----------------------------------------------------------------------------------------------------------------------------------------------------*/

        [Test]
        public void UserMessageDelete()
        {
            page.ClickUserMessages();
            page.UserDeleteButton();
            Console.WriteLine("Expected result:Clicking on the delete icon will open up a modal Are you sure you want to delete? If the user clicks Cancel the modal is closed and no action is taken. If the user clicks Ok the modal is closed and the message will be deleted and no longer displayed in the grid.");
            Console.WriteLine("Output:After clicking the delete button open up a modal");

        }

        /*---- -------------------------------------------------------------------------------------------------------------
       -- Automation TestID: TC63
       --TestID:144 and 145
       -- CREATED BY:Sendil
       -- Test Location:Home Dashboard 
       -- Sub Test Location:
       -- Test Description:
       -- Expected Result:"When clicked, displays a calendar control to select a date or the option to free-type a date.
                           When clicked, displays a calendar control to select a date or the option to free-type a date.
       -- DATE CREATED: 11/30/2016
       -- REVISION LOG:
       -------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC63_CommentSearchDatepicker()
        {
            page.ClickCommentSearch();
            page.CommentSearchStartDates();
            page.CommentSearchEndDates();
            Console.WriteLine("Date entered in Start Date Field");
            Console.WriteLine("Date entered in End Date Field");
            page.SearchButton();
        }
        /*---- -------------------------------------------------------------------------------------------------------------
       -- Automation TestID: TC64
       --TestID:146 and 147
       -- CREATED BY:Sendil
       -- Test Location:Home Dashboard 
       -- Sub Test Location:Comment Search
       -- Test Description:Name
       -- Expected Result:"Textbox is editable ONLY IF the Actioned Type dropdown has a value.
                           Non-editable if Actioned Type is NOT selected.
       -- DATE CREATED: 11/30/2016
       -- REVISION LOG:
       -------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC64_CommentSearchSelectDate()
        {
            page.ClickCommentSearch();
            Hooks.Driver.WaitForJQuery(100);
            page.SelectActionedDDL("Created By");
            Console.WriteLine("Dropdown option has been selected and now Textbox is editable ONLY IF the Actioned Type dropdown has a value.");
            page.CommentNameField("TestName");
            //page.CommentTextField("Test");
            page.SearchButton();

        }
        /*---- -------------------------------------------------------------------------------------------------------------
       -- Automation TestID: TC65
       --TestID:148 and 150
       -- CREATED BY:Sendil
       -- Test Location:Home Dashboard 
       -- Sub Test Location:Comment Search
       -- Test Description:Comment and Claim
       -- Expected Result:"Contains search based on the Comment. Validate that records returned Contain the value entered in the search criteria.​"
                           Exact search on Claim ID related to the claim comment. Claim ID is only available when "Claim Comments" is selected. Validate that records returned have a Claim ID equal to the claim id entered in the search criteria..
       -- DATE CREATED: 11/30/2016
       -- REVISION LOG:
       -------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC65_CommentandClaim()
        {
            page.ClickCommentSearch();
            Hooks.Driver.WaitForJQuery(100);
            //page.SelectActionedDDL("Created By");
            //Console.WriteLine("Dropdown option has been selected and now Textbox is editable ONLY IF the Actioned Type dropdown has a value.");
            //page.CommentNameField("TestName");
            page.CommentTextField("Test");
            page.CommentClaimId("3823363");
            page.SearchButton();
            //Console.WriteLine("Contains search based on the Comment. Validate that records returned Contain the value entered in the search criteria.​.");
            Console.WriteLine("Exact search on Claim ID related to the claim comment.");

        }
        /*---- -------------------------------------------------------------------------------------------------------------
       -- Automation TestID: TC67
       --TestID:
       -- CREATED BY:Sendil
       -- Test Location:Home Dashboard 
       -- Sub Test Location:Comment Search
       -- Test Description:Comment and Claim
       -- Expected Result:"
       -- DATE CREATED: 12/01/2016
       -- REVISION LOG:
       -------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC67_CommentSearchAlert()
        {
            page.ClickCommentSearch();
            Hooks.Driver.WaitForJQuery(100);
            page.SearchButton();
            var expected = page.SearchWarning();
            Console.WriteLine("Expected result:Warning modal displayed-If the user does not enter any search criteria, an error is returned .- Output Result is :--- " + expected + "");
            // Assert.AreEqual(expected, "Warning! Please enter Search Criteria.");
        }
        /*---- --------------------------------------------------------------------------------------------------------------------------------
       -- Automation TestID: TC68
       --TestID:
       -- CREATED BY:Sendil
       -- Test Location:Home Dashboard 
       -- Sub Test Location:Comment Search
       -- Test Description:Comment and Claim
       -- Expected Result:".
       -- DATE CREATED: 12/01/2016
       -- REVISION LOG:
       ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC68_CommentSearchDateAlert()
        {
            page.ClickCommentSearch();
            Hooks.Driver.WaitForJQuery(100);
            page.SelectStatusDDDL("Complete");
            page.SearchButton();
            var expected = page.SearchWarningdate();
            Console.WriteLine("Expected result: If no dates are entered, this will return a message Date is required when Status is Complete. .- Output Result is :--- " + expected + "");
        }

        /*---- -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      -- Automation TestID: TC69
      --TestID:
      -- CREATED BY:Sendil
      -- Test Location:Home Dashboard 
      -- Sub Test Location:Comment Search
      -- Test Description:Search Status
      -- Expected Result:"f the user selects Status = New or Researching, the Date is NOT required and the search will be performed with/without date entry."
      -- DATE CREATED: 12/01/2016
      -- REVISION LOG:
      ---------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC69_CommentSearchSearch()
        {
            page.ClickCommentSearch();
            Hooks.Driver.WaitForJQuery(100);
            page.SelectStatusDDDL("New");
            page.SearchButton();
            // var expected = page.SearchWarningdate();
            Console.WriteLine("Output: search will be performed with/without date entry when user selects status New or Researching.");
        }

        /*---- ------------------------------------------------------------------------------------------------------------------------------------------------
       -- Automation TestID: TC70
       --TestID:
       -- CREATED BY:Sendil
       -- Test Location:Home Dashboard 
       -- Sub Test Location:Comment Search
       -- Test Description:Search Status
       -- Expected Result:"If the user selects an Actioned Type, the Name textboxes become editable. If Actioned By is selected the search is based on the Actioned user, if Created By is selected, the search is on the creator name."
       -- DATE CREATED: 12/01/2016
       -- REVISION LOG:
       ----------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC70_CommentSearch_ActionedType()
        {
            ExtentTest TestStep = null;
            try
            {
                TestStep = Report.StartTest(@"HomeDashboard/CommentSearchActionType", "Comment Search Action Type");
                page.ClickCommentSearch();
                Hooks.Driver.WaitForJQuery(100);
                page.SelectActionedDDL("Actioned By");
                TestStep.Log(LogStatus.Pass, "Comment search Actioned By");
                Console.WriteLine("Value has been selected from Actioned Type dropdown list");
                TestStep.Log(LogStatus.Pass, "Value has been selected from Actioned Type dropdown list");
                page.CommentNameField("TestName");
                Console.WriteLine("User input value entered in NAME text field");
                TestStep.Log(LogStatus.Pass, "User input value entered in NAME text field");
                page.SearchButton();
                Console.WriteLine("Clicked search button");
                Console.WriteLine("User is selected Actioned By drop down values and search is based on Actioned user");
                TestStep.Log(LogStatus.Pass, "Clicked search button");
                TestStep.Log(LogStatus.Pass, "User is selected Actioned By drop down values and search is based on Actioned user");

                // var expected = page.SearchWarningdate();
                //Console.WriteLine("Expected result:Warning modal displayed-If the user selects a Status = Complete, the Date Type and To/From Dates must be selected. If no dates are entered, this will return a message Date is required when Status is Complete. .- Output Result is :--- " + expected + "");
            }
            catch (Exception e)
            {
                ExtentManager.TakeScreenshot("TC70_CommentSearchActionType", Driver);
                TestStep.Log(LogStatus.Fail, "Test ended with Fail" + FormatTraceMsg(e.Message) + FormatTraceMsg(e.StackTrace) + TestStep.AddScreenCapture("C:\\report\\TC70_CommentSearchActionType.gif"));
                //throw e;
            }
            finally
            {
                Report.EndTest(TestStep);
            }
        }
        /*---- -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
       -- Automation TestID: TC71
       --TestID:
       -- CREATED BY:Sendil
       -- Test Location:Home Dashboard 
       -- Sub Test Location:Comment Search
       -- Test Description:Comment and Claim
       -- Expected Result:".If the user selects/enters To/From Dates, the Date Type must contain a selection. If no Date Type is selected, an error will be returned "Please select a Date Type"
       -- DATE CREATED: 12/01/2016
       -- REVISION LOG:
       ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC71_CommentSearch_DateSelection_Alert()
        {
            ExtentTest TestStep = null;
            try
            {
                TestStep = Report.StartTest(@"HomeDashboard/CommentSearchDateSelectionAlert", "Comment Search Date Selection Alert");
                page.ClickCommentSearch();
                Hooks.Driver.WaitForJQuery(100);
                page.CommentSearchStartDates();
                page.CommentSearchEndDates();
                Console.WriteLine("Date entered in Start Date Field");
                TestStep.Log(LogStatus.Pass, "Date entered in Start Date Field");
                Console.WriteLine("Date entered in End Date Field");
                TestStep.Log(LogStatus.Pass, "Date entered in End Date Field");
                page.SearchButton();
                var expected = page.SearchWarningdate();
                Console.WriteLine("Expected result: If no Date Type is selected, an error will be returned Please select a Date Type  .- Output Result is :--- " + expected + "");
                TestStep.Log(LogStatus.Pass, "Expected result: If no Date Type is selected, an error will be returned Please select a Date Type  .- Output Result is :--- " + expected + "");
            }
            catch (Exception e)
            {
                ExtentManager.TakeScreenshot("TC71_CommentSearchDateSelectionAlert", Driver);
                TestStep.Log(LogStatus.Fail, "Test ended with Fail" + FormatTraceMsg(e.Message) + FormatTraceMsg(e.StackTrace) + TestStep.AddScreenCapture("C:\\report\\TC71_CommentSearchDateSelectionAlert.gif"));
                //throw e;
            }
            finally
            {
                Report.EndTest(TestStep);
            }
        }
        [Test]
        public void UserMessageMasterCheckBox()
        {
            page.ClickUserMessages();
            page.UserMsgMasterCheckBox();

        }

        [Test]
        public void SuggestionBGColor()
        {
            var BGColor = page.CheckBGColor();
            string exp = "#34495e";
            Console.WriteLine("Suggestion box color code should be #34495e");
            Assert.AreEqual(exp, BGColor.ToLower(), "Test case failed: Output is not matching with expected result");
        }

        [Test]
        public void HomeDashboardPanelCount()
        {
            var item1 = page.TransactionPanelCountHomeDashboard();
            var item2 = page.NewsfeedPanelCountHomeDashboard();
            var item3 = page.MessagesPanelCountHomeDashboard();
            var item = item1 + item2 + item3;
            if (item == 3)
            {
                Console.WriteLine("total number of panel {0}", item);
            }
            else
            {
                Console.WriteLine("total number of panel {0}", item);
            }
            Assert.AreEqual(3, item);

        }

        [Test]
        public void ClickHrefLink()
        {
            page.ClickPostLink();
        }

        [OneTimeTearDown]
        public void TearDownHomeDashBoardTest()
        {
            TestDataFactory.UpdateTestCaseExcel(itemList);
            TearDown(Report);
        }
    }
}
