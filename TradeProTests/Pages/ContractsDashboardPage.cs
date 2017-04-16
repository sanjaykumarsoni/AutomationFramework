using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using AutomationFramework.Extensions;
using AutomationFramework.KendoWrapper;
using AutomationFramework.Base;

namespace TradeProTests.Pages
{
    public class ContractsDashboardPage
    {
        #region WebElements and Selectors
        [FindsBy(How = How.Id, Using = "contractiddropdown")]
        public IWebElement Contractsddl { get; set; }

        [FindsBy(How = How.Id, Using = "contractid")]
        public IWebElement ContractID { get; set; }

        [FindsBy(How = How.Id, Using = "contractname")]
        public IWebElement ContractName { get; set; }

        [FindsBy(How = How.Id, Using = "templatedropdown")]
        public IWebElement templateddl { get; set; }

        [FindsBy(How = How.Id, Using = "statusdropdown")]
        public IWebElement Statusddl { get; set; }

        [FindsBy(How = How.Id, Using = "programtypesdropdown")]
        public IWebElement Programtypesddl { get; set; }

        [FindsBy(How = How.Name, Using = "hasrenewal")]
        public IList<IWebElement> HasRenewal { get; set; }

        [FindsBy(How = How.Id, Using = "clearsearch")]
        public IWebElement ClearButton { get; set; }

        [FindsBy(How = How.Id, Using = "contractsearch")]
        public IWebElement SearchButton { get; set; }

        [FindsBy(How = How.Id, Using = "advance-contractsearch-link")]
        public IWebElement AdvancedSearch { get; set; }

        [FindsBy(How = How.Id, Using = "msgModalBody")]
        public IWebElement SearchAlert { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".k-pager-info.k-label")]
        public IWebElement NoData { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".k-link")]
        public IWebElement TableName { get; set; }

        [FindsBy(How = How.Id, Using = "contractcreate-link")]
        public IWebElement CreateContractButton { get; set; }




        //-----------------------Test case for Advanced Search---------------------------------------------- //
       
        //Testcase for Identifier section//
        [FindsBy(How = How.Id, Using = "ddladv_savedsearch")]
        public IWebElement SavedSearchddl { get; set; }

        [FindsBy(How = How.Id, Using = "ddladv_contractidtype")]
        public IWebElement Contractidtype { get; set; }

        [FindsBy(How = How.Id, Using = "txtadv_contractid")]
        public IWebElement ContractsIDAdSearch { get; set; }

        [FindsBy(How = How.Id, Using = "txtadv_contractname")]
        public IWebElement ContractNameAdSearch { get; set; }

        [FindsBy(How = How.Id, Using = "ddladv_contracttype")]
        public IWebElement ContractTypeAdSearch { get; set; }

        [FindsBy(How = How.Id, Using = "btnadvcontractssearch")]
        public IWebElement AdSearchBtn { get; set; }

        [FindsBy(How = How.Id, Using = "btnadvclearsearch")]
        public IWebElement AdClearBtn { get; set; }

        //Test case for By Company//
        [FindsBy(How = How.Id, Using = "ddladv_contractidtype")]
        public IWebElement TradeProIdDDL { get; set; }

        [FindsBy(How = How.Id, Using = "txtadv_contracteeid")]
        public IWebElement ContracteeIdValue { get; set; }

        [FindsBy(How = How.Id, Using = "txtadv_contracteename")]
        public IWebElement ContracteeName { get; set; }

        [FindsBy(How = How.Id, Using = "chkadv_mycontracteeonly")]
        public IWebElement MyContracteeCb { get; set; }

        [FindsBy(How = How.Id, Using = "ddladv_distributoridtype")]
        public IWebElement DistributorIdValue { get; set; }
        
        [FindsBy(How = How.Id, Using = "txtadv_distributorname")]
        public IWebElement DistributorName { get; set; }

        [FindsBy(How = How.Id, Using = "")]
        public IWebElement CreateContract { get; set; }

        //Test Case for By Product//

        [FindsBy(How = How.Id, Using = "txtadv_productname")]
        public IWebElement ProductName { get; set; }

        [FindsBy(How = How.Id, Using = "txtadv_productsku")]
        public IWebElement ProductSKU { get; set; }

        //Test Case for By user//

        [FindsBy(How = How.Id, Using = "ddladv_creator")]
        public IWebElement UserDDL { get; set; }

        [FindsBy(How = How.Id, Using = "ddladv_accountowner")]
        public IWebElement UserACDDL { get; set; }

        //Test Case for By others//

        [FindsBy(How = How.Id, Using = "ddladv_templatename")]
        public IWebElement TemplateN { get; set; }

        [FindsBy(How = How.Id, Using = "ddladv_contractstatus")]
        public IWebElement ContractSt { get; set; }
       
        [FindsBy(How = How.Id, Using = "ddladv_contracteepaytype")]
        public IWebElement ContracteePt { get; set; }

        [FindsBy(How = How.Id, Using = "ddladv_distributorpaytype")]
        public IWebElement DistributorPt { get; set; }

        [FindsBy(How = How.Id, Using = "ddladv_programtypes")]
        public IWebElement ProgramTypes { get; set; }

        [FindsBy(How = How.Id, Using = "ddladv_Claims")]
        public IWebElement Claims { get; set; }

        //Test case for Return Dashbaord//

        [FindsBy(How = How.Id, Using = "advance-search-close")]
        public IWebElement Return { get; set; }


        #endregion

        #region Methods to interact With WebElements.
       
        public void SelectContractIdDDLSearch(string ContractID)
        {
            var kendoDDL = new KendoDropDownList(Contractsddl);
            kendoDDL.Select(ContractID);
        }

        public void ContractIdSearch(string id)
        {
            Hooks.Driver.WaitForJQuery(100);
            ContractID.WaitForElementToLoad(100);
            ContractID.EnterText(id);
            Console.WriteLine("Contract ID Entered");
        }
        public string ContractIdwatermark()
        {
            var item = ContractID.GetTextByPlaceholder();
            return item;
        }
        public string ContractNamewatermark()
        {
            var item= ContractName.GetTextByPlaceholder();
            return item;
        }

        public void ContractNameSearch(string name)
        {
            ContractName.EnterText(name);
            Console.WriteLine("Contract Name Entered");
        }

        public void SelectTemplateDDLSearch(string template)
        {
            var kendoDDL = new KendoDropDownList(templateddl);
            kendoDDL.Select(template);
            Console.WriteLine("Template is the default value");
        }

        public void SelectStatusDDLSearch(string status)
        {
            var kendoDDL = new KendoDropDownList(Statusddl);
            kendoDDL.Select(status);
            Console.WriteLine("Contract Status is the default value");
        }

        public void SelectProgramDDLSearch(string programTypes)
        {
            var kendoDDL = new KendoDropDownList(Programtypesddl);
            kendoDDL.Select(programTypes);
            Console.WriteLine("Program Types is the default value");
        }

        public void HasRenewalRadioBtn()
        {
            HasRenewal.SelectCheckboxOrRadioBtn(2);
            Console.WriteLine("Radio button Yes selected");
        }

        public void ClearSearchBtn()
        {
            ClearButton.Click();
            Console.WriteLine("Clicked Clear button");
        }

        public void SearchBtn()
        {
            Hooks.Driver.ActionDrivers("contractsearch");
            SearchButton.Click();
            Console.WriteLine("Clicked Search button");
        }

        public string SearchWarning()
        {
            var item = SearchAlert.Text;
            return item; 
        }

        public string SearchNoData()
        {
            var item = NoData.GetText();
            return item;
        }

        public string ContractSearchbuttonName()
        {
            var item = SearchButton.Text;
            return item;
        }

        public string ContractClearbuttonName()
        {
            var item = ClearButton.Text;
            return item;
        }

       // public string ContractTempalteNames()
       // {
          //  var item = TableName;
           // return item;
       // }

        public string ContractAdvancesearchHyperlink()
        {
            var item = AdvancedSearch.Text;
            return item;
        }

        public void CreateContractBtn()
        {
            CreateContractButton.Click();
            Console.WriteLine("Clicked Create Contract button");
        }

        //-----------------------Test case for Advanced Search---------------------------------------------- //
       //----------- -------------By Identifier section-----------------------------------------------------//
       
        //Method for Advanced Search by Identifier//
        public void AdvancedSearchlink()
        {
            AdvancedSearch.Click();
            Console.WriteLine("Clicked Advanced search link");
        }

        //Method for SavedSearch Dropdownlist//
        public void SelectSavedSearchDDL(string savedsearch)
        {
            Hooks.Driver.WaitForJQuery(100);
            SavedSearchddl.WaitForElementToLoad(100);
            var kendoDDL = new KendoDropDownList(SavedSearchddl);
            kendoDDL.Select(savedsearch);
            Console.WriteLine(" Select the option from dropdown list");
        }

        //Method for Contract id dropdown//
        public void SelectAdContractidDDL(string ContractID)
        {
            var kendoDDL = new KendoDropDownList(Contractidtype);
            kendoDDL.Select(ContractID);
            Console.WriteLine("Option selected from contract dropdown list");

        }

        //Method for Contract ID Textboxx//
        public void ContractsIdAdvanceSearch(string id)
        {
            Hooks.Driver.WaitForJQuery(100);
            ContractsIDAdSearch.WaitForElementToLoad(100);
            ContractsIDAdSearch.EnterText(id);
            Console.WriteLine("Contract ID Entered");
        }

        //Method for Contract Name Textboxx//
        public void ContractNameAdvanceSearch(string id)
        {
            ContractNameAdSearch.EnterText(id);
            Console.WriteLine("Contract Name Entered");
        }

        //Method for Contract Name Textboxx//
        public void SelectContractTypeDDL(string contracttype)
        {
            var kendoDDL = new KendoDropDownList(ContractTypeAdSearch);
            kendoDDL.Select(contracttype);
            Console.WriteLine("Option selected from contract dropdown list");

        }

        ////Method for ContractType Dropdownlistvalue//
        //public List<string> SelectContractTypeDDL(string contracttype)
        //{
        //    List<string> Contracttype = new List<string>();
        //    Hooks.Driver.WaitForJQuery(100);
        //    var kendoDDL = new KendoDropDownList(ContractTypeAdSearch);
        //   List<string> Data= kendoDDL.Select(contracttype);
        //   while (Data.Count()> 0)
        //   {

        //       Contracttype.Add(Data[0]);
        //   }
        //    Console.WriteLine(" Contract type option has been selected");
        //    return Contracttype;
             
           
            
        //}
        //Method for By Company  DistributorName//
        public void CompanyDistributorName(string name)
        {
            DistributorName.EnterText(name);           
        }

        //Method for By product//
        public void ProductNam(string name)
        {
           ProductName.EnterText(name);
            Console.WriteLine("Product Name Entered");
        }

        public void Productsku(string id)
        {
            ProductSKU.EnterText(id);
            Console.WriteLine("Product sku Entered");
        }

        //Method for By User//
        public void SelectUserDDLValue(string UserValue)
        {
            var kendoDDL = new KendoDropDownList(UserDDL);
            kendoDDL.Select(UserValue);
            Console.WriteLine(" Selected the option from dropdown list");
        }

        public void SelectUserDDLValue2(string UserValue2)
        {
            var kendoDDL = new KendoDropDownList(SavedSearchddl);
            kendoDDL.Select(UserValue2);
            Console.WriteLine(" Select the option from dropdown list");
        }

        //Method for By Others//
        public void TemplateDDL(string TempName)
        {
            var kendoDDL = new KendoDropDownList(TemplateN);
            kendoDDL.Select(TempName);
            Console.WriteLine(" Selected the option from dropdown list");
        }

       public void StatusDDL(string StatusValue)
        {
            var kendoDDL = new KendoDropDownList(ContractSt);
            kendoDDL.Select(StatusValue);
            Console.WriteLine(" Selected the option from dropdown list");
        }

       public void SelectContracteeDDL(string ContracteeValue)
       {
           var kendoDDL = new KendoDropDownList(ContracteePt);
           kendoDDL.Select(ContracteeValue);
           Console.WriteLine(" Selected the option from dropdown list");
       }

       public void SelectDistributorDDL(string DistributorValue)
       {
           var kendoDDL = new KendoDropDownList(DistributorPt);
           kendoDDL.Select(DistributorValue);
           Console.WriteLine(" Selected the option from dropdown list");
       }

       public void SelectProgramTypesDDL(string ProgramValue)
       {
           var kendoDDL = new KendoDropDownList(ProgramTypes);
           kendoDDL.Select(ProgramValue);
           Console.WriteLine(" Selected the option from dropdown list");
       }

       public void SelectClaimsDDL(string ClaimsValue)
       {
           var kendoDDL = new KendoDropDownList(Claims);
           kendoDDL.Select(ClaimsValue);
           Console.WriteLine(" Selected the option from dropdown list");
       }

        //Method for Search button//
        public void AdvanceSearchBtn()
        {
            AdSearchBtn.Click();
            Console.WriteLine("Clicked Search button");
        }

        public void AdvanceClearBtn()
        {
            AdClearBtn.Click();
            Console.WriteLine("Clicked Clear button");
        }

        //Method for Return Dashboard//
        public void ReturnDashboard()
        {
            Return.Click();
            Console.WriteLine("Clicked ReturnDashboard button");
        }

        //Methods for By Company//
       // public void SelectAdTradeProidDDL(string TradeProId)
       // {
          //  var kendoDDL = new KendoDropDownList(ddladv_contractidtype);
           // kendoDDL.Select(TradeProId);
           // Console.WriteLine("Option selected from contract dropdown list");

        //}

        public bool HasRenewalDefaultRadioBtn()
        {
            bool temp = HasRenewal.IsCheckboxOrRadiobtnSelectedByDefault(2);
            return temp;
        }

        #endregion
    }
       
}
