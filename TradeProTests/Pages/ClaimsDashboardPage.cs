using AutomationFramework.KendoWrapper;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeProTests.Pages
{
    public class ClaimsDashboardPage
    {
        #region WebElements
        [FindsBy(How = How.Id, Using = "advanceclaimsearchlink")]
        public IWebElement AdvancedSearch { get; set; }
        [FindsBy(How = How.Id, Using = "ClaimType")]
        public IWebElement ClaimType { get; set; }
        #endregion

        #region Webelement Methods
        public void AdvancedSearchlink()
        {
            AdvancedSearch.Click();
            Console.WriteLine("Clicked Advanced search link");
        }
        public void ClaimTypeDropDown()
        {
            KendoMultiSelectDropDownList objKendoMultiSelectDropDownList = new KendoMultiSelectDropDownList(ClaimType);
           // a.SelectByText("Billback");
            //List<string> temString = new List<string>();
            //temString.Add("Billback");
            //temString.Add("HQ Rebate");
            //objKendoMultiSelectDropDownList.Select(temString);
            objKendoMultiSelectDropDownList.Select("Billback");
        }

        #endregion
    }
}
