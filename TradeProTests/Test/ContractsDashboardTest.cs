using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TradeProTests.Pages;
using AutomationFramework.Base;
using AutomationFramework.Extensions;
using AutomationFramework.Helpers;
using System.IO;
using TradeProTests.TestDataAccess;
using System.Threading;

namespace TradeProTests.Test
{
    [TestFixture]
    [Category("Contracts Dashboard Test")]
    public class ContractsDashboardTest : Hooks
    {
        private ContractsDashboardPage page;
        public ContractsDashboardTest()
            : base(AutomationSetting.BrowserToRunWith)
        {
            ExcelHelper.PopulateInCollection(Path.GetFullPath(Directory.GetCurrentDirectory() + FilePath.DataDrivenExcelFileLocation));
            GoTo("https://authasidevtm1b.answerssystems.com/");
            Console.WriteLine("Application has been loaded");
            string[] data = new[] { "Automation.tester@afsi.com", "Testing1" };
            PageGenerator.ApplicationLoginPage.LoginToApplication(data);
            Console.WriteLine("Login to the application");
            GoTo("http://fsasidevtm1b.answerssystems.com/Contracts/");
            Console.WriteLine("Navigate to the contracts dasboard page");
            page = PageGenerator.ContractsDashBoardPage;
        }
        /*----------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC93
        --TestID:428
        -- CREATED BY:Sendil
        -- Test Location:Contract Dashboard 
        -- Sub Test Location:Contract Dashboard
        -- Test Description:Create Contract
        -- Expected Result:"Button. When clicked, the user is directed to the Select Template screen that displays all templates the user has visibility to. This grid has as a last column "New Contract" with an icon that displays. When clicked, this will begin the Create Contract process.
        -- DATE CREATED: 12/09/2016
        -- REVISION LOG:
        -----------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC93_ContractDashboard_CreateContract()
        {
            Hooks.Driver.WaitForJQuery(20);
            page.CreateContractBtn();
            Console.WriteLine("After clicking the create button the user is directed to the Select Template screen that displays all templates the user has visibility");
        }

        /*----------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC100
        --TestID:428
        -- CREATED BY:Sendil
        -- Test Location:Contract Dashboard 
        -- Sub Test Location:Search
        -- Test Description:Search
        -- Expected Result:"Section labeled "Search" that has the following search options in top-to-bottom order: Dropdown defaulting to Contract ID, Contract ID (textbox), Contract Name, Template (dropdown), Contract Status (dropdown).​​​Program Types(Dropdown), Label Has Renewal with three radio buttons Yes,No,Both, Search and Clear buttons and Advance Search Hyperlink.
        -- DATE CREATED: 12/08/2016
        -- REVISION LOG:
        -----------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC100_ContractDashboard_Search_FieldNames()
        {
            page.SelectContractIdDDLSearch("Contract ID");
            Console.WriteLine("Contract ID default Dropdown value is Contract ID");
            var expected = page.ContractIdwatermark();
            Console.WriteLine("Expected Result: Contract Id text field WaterMark value should be Contract ID .- Automation Result is : " + expected + "");
            var Result = page.ContractNamewatermark();
            Console.WriteLine("Expected result:Contract Name Text field WaterMark value should be  Contract Name .- Automation Result is : " + Result + "");
            page.SelectTemplateDDLSearch("Template");
            page.SelectStatusDDLSearch("Contract Status");
            page.SelectProgramDDLSearch("Program Types");
            page.HasRenewalRadioBtn();
            var Searchname = page.ContractSearchbuttonName();
            Console.WriteLine("Search button named as.Automation Result is :" + Searchname + "");
            var Clearname = page.ContractClearbuttonName();
            Console.WriteLine("Clear button named as.Automation Result is :" + Clearname + "");
            var link = page.ContractAdvancesearchHyperlink();
            Console.WriteLine("Hyperlink named as.Automation Result is :" + link + "");
          
        }
        /*----------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC101
        --TestID:429
        -- CREATED BY:Sendil
        -- Test Location:Contract Dashboard 
        -- Sub Test Location:Search
        -- Test Description:Dropdown
        -- Expected Result:"Dropdown with options Contract ID and Client Contract ID. The label of the dropdown is the selected value. Default value is Contract ID..
        -- DATE CREATED: 12/08/2016
        -- REVISION LOG:
        -----------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC101_ContractDashboard_Search_Dropdown()
        {
            page.SelectContractIdDDLSearch("Contract ID");
            Console.WriteLine("Contract ID default Dropdown value is Contract ID");
        }
        /*----------------------------------------------------------------------------------------------------------------
       -- Automation TestID: TC102
       --TestID:429
       -- CREATED BY:Sendil
       -- Test Location:Contract Dashboard 
       -- Sub Test Location:Search
       -- Test Description:Dropdown
       -- Expected Result:"Dropdown with options Contract ID and Client Contract ID. The label of the dropdown is the selected value. Default value is Contract ID..
       -- DATE CREATED: 12/08/2016
       -- REVISION LOG:
       -----------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC102_ContractDashboard_Search_ContractName()
        {
            page.ContractNameSearch("Test 123456678910 Test");
            Console.WriteLine("Alphanumeric values allowed in Contract name Text field");
        }
        /*----------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC104
        --TestID:432 433 and 434
        -- CREATED BY:Sendil
        -- Test Location:Contract Dashboard 
        -- Sub Test Location:
        -- Test Description:
        -- Expected Result:""
        -- DATE CREATED: 12/08/2016
        -- REVISION LOG:
        -------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC104_ContractDashboard_Search()
        {
            Hooks.Driver.WaitForJQuery(10);
            page.SelectStatusDDLSearch("Contract Status");
            Console.WriteLine("Dropdown labeled Contract Status");
            page.ContractIdSearch("5079868");
            Console.WriteLine("Contract id entered in Contract id text field");
            page.ContractNameSearch("Green Leaf");
            page.SelectStatusDDLSearch("DRAFT");
            page.SelectProgramDDLSearch("Program Types");
            page.HasRenewalRadioBtn();
            page.SearchBtn();
            Console.WriteLine("search based on criteria entered");
        }
      /*-----------------------------------------------------------------------------------------------------------------------------------------------
      -- Automation TestID: TC105
      --TestID:435
      -- CREATED BY:Sendil
      -- Test Location:Contract Dashbaord
      -- Sub Test Location:Search
      -- Test Description:Clear button
      -- Expected Result:"There is a Clear button. Clicking this will clear all data entered on the search form as well as remove the results grid.".
      -- DATE CREATED: 12/08/2016
      -- REVISION LOG:
      -------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC105_ContractDashboard_Clear_Button()
        {
            Hooks.Driver.WaitForJQuery(20);
            page.SelectStatusDDLSearch("Contract Status");
            Console.WriteLine("Dropdown labeled named as Contract Status");
            page.ContractIdSearch("5079868");
            Console.WriteLine("Contract id entered in Contract id text field");
            page.ContractNameSearch("Green Leaf");
            page.SelectStatusDDLSearch("SUBMIT");
            page.ClearSearchBtn();
            Console.WriteLine("After clicking Clear button clears all data entered on the search form as well as remove the results grid.");
        }
        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC106
        --TestID:436
        -- CREATED BY:Sendil
        -- Test Location:Contract Dashbaord
        -- Sub Test Location:Search
        -- Test Description:Advanced Search
        -- Expected Result:There is a hyperlink labeled "Advanced Search" on the bottom right of the search section. Clicking this hyperlink will direct the user to the current Contract Search screen opened in the same window..".
        -- DATE CREATED: 12/08/2016
        -- REVISION LOG:
        ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC106_ContractDashboard_AdvancedSearch_Hyperlink()
        {
            Hooks.Driver.WaitForJQuery(20);
            page.AdvancedSearchlink();
            Console.WriteLine("After clicking hyperlink direct the user to the current Contract Search screen opened in the same window..");
        }
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC107
        --TestID:436
        -- CREATED BY:Sendil
        -- Test Location:Contract Dashbaord
        -- Sub Test Location:Search Results Grid
        -- Test Description:No Data
        -- Expected Result:If the user performs a search that returns no results, the results grid will display with the message "No items to Display" on the bottom right of the grid.
        -- DATE CREATED: 12/09/2016
        -- REVISION LOG:
        -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC107_ContractDashboard_Search_NoData()
        {
            Hooks.Driver.WaitForJQuery(20);
            page.ContractIdSearch("123");
            page.SearchBtn();
            Hooks.Driver.WaitForJQuery(50000);
            //var Names = page.ContractTempalteNames()
           // var msg = page.SearchNoData();
            //Console.WriteLine("If the user performs a search that returns no results, the results grid will display with the message No items to Display, Automation  "");
        }
        [Test]
        //[Ignore("for advance Scenario")]
        public void ContractAdvaceSearchBySavedSearch()
        {
            Hooks.Driver.WaitForJQuery(10);
            page.AdvancedSearchlink();
            page.SelectSavedSearchDDL("TestConTest");
            page.AdvanceSearchBtn();
        }
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC122
        --TestID:632,633 and 634
        -- CREATED BY:Sendil
        -- Test Location:Contract Dashbaord
        -- Sub Test Location:Advanced Contract Search
        -- Test Description:"By Identifier - Contract ID and Name
        -- Expected Result:"Textbox. If Contract ID was selected as the Contract ID type, all contracts with the Contract ID entered will be returned on the search.

                            Textbox. If Client Contract ID was selected as the Contract ID Type, all contracts with the Client Contract ID entered will be returned on the search.  This should accept alpha-numeric values.​

                            Textbox. All contracts that contains the Contract Name entered will be returned on the search."
        -- DATE CREATED: 12/16/2016
        -- REVISION LOG:
        -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC122_ContractAdvancedSearch_ContractID_ContractName()
        {
            Hooks.Driver.WaitForJQuery(10);
            page.AdvancedSearchlink();
            Hooks.Driver.WaitForJQuery(70000);
            page.SelectAdContractidDDL("Contract ID");
            Console.WriteLine("Contract Id value selected from Contract id dropdown list ");
            page.ContractsIdAdvanceSearch("5399373");
            Console.WriteLine("Contract Id Entered in Contract id field ");
            page.ContractNameAdvanceSearch("Test_con_10102016");
            Console.WriteLine("Contract Name Entered in Contract id field ");
           // page.SelectContractTypeDDL("MFG to Oper");
            page.AdvanceSearchBtn();
           // page.ReturnDashboard();
           // page.AdvanceClearBtn();
        }
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC124
        --TestID:636
        -- CREATED BY:Sendil
        -- Test Location:Contract Dashbaord
        -- Sub Test Location:Advanced Contract Searc
        -- Test Description:"By Identifier - Contract ID and Name
        -- Expected Result:Dropdown. All contracts that have the specific contract type selected will be returned on the search.
        -- DATE CREATED: 12/16/2016
        -- REVISION LOG:
        -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC124_ContractAdvancedSearch_ContractType()
        {
            Hooks.Driver.WaitForJQuery(10);
            page.AdvancedSearchlink();
            Hooks.Driver.WaitForJQuery(70000);
            page.SelectContractTypeDDL("MFG to Oper");
            //List<string> Contracttype = page.SelectContractTypeDDL("MFG to Oper");
            Console.WriteLine("Contract type value selected from Contract type dropdown list ");
            page.AdvanceSearchBtn();
        }
        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        -- Automation TestID: TC129
        --TestID:641,642 and 643
        -- CREATED BY:Sendil
        -- Test Location:Contract Dashbaord
        -- Sub Test Location:Advanced Contract Searc
        -- Test Description:"By Company, By Product Name and SKU
        -- Expected Result:"Textbox. All contracts that contain the Distributor Name entered will be returned on the search.

                            Textbox. All contracts that contain the Product Name entered will be returned on the search.

                            Textbox. All contracts that contain the Product SKU entered will be returned on the search.  This search should perform for alpha-numeric values entered.".
        -- DATE CREATED: 12/16/2016
        -- REVISION LOG:
        -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        [Test]
        public void TC129_ContractAdvancedSearch_ByCompany()
        {
            Hooks.Driver.WaitForJQuery(10);
            page.AdvancedSearchlink();
            Hooks.Driver.WaitForJQuery(70000);
            page.CompanyDistributorName("US FOODS - LIVERMORE");
            Console.WriteLine("Distributor Name entered in Distributor Text field ");
            page.ProductNam("SM 1/2 OZ ORANGE MARM PLAS");
            Console.WriteLine("Product Name entered in Product name text field ");
            page.Productsku("5150000766");
            Console.WriteLine("Product sku entered in Product sku text field ");

            //List<string> Contracttype = page.SelectContractTypeDDL("MFG to Oper");
           // Console.WriteLine("Contract type value selected from Contract type dropdown list ");
            page.AdvanceSearchBtn();
        }

        [Test]
        [Ignore("for advance Scenario")]
        public void ContractAdvancedSearchClearBtn()
        {
            Hooks.Driver.WaitForJQuery(100);
            page.AdvancedSearchlink();

            page.ContractsIdAdvanceSearch("5016827");

            page.ContractNameAdvanceSearch("A&H VENDING");

            page.SelectContractTypeDDL("MFG to Oper");

            page.AdvanceClearBtn();
        }

        [Test]
        public void HasRenewalDefault()
        {
            bool temp = page.HasRenewalDefaultRadioBtn();
            Assert.AreEqual(temp, true);
        }
       
        //[OneTimeTearDown]
        public void TearDownHomeDashBoardTest()
        {
            TearDown();
        }

    }
}
