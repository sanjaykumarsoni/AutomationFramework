using AutomationFramework.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace AutomationFramework.KendoWrapper
{
    public class KendoTab : KendoWidget
    {
        private readonly string tabStripId;
        private readonly IJavaScriptExecutor driver;
        private readonly string kendoTab;
        public KendoTab(IWebElement gridName)
            : base(gridName)
        {
            IWebDriver driver = Hooks.Driver;
            this.tabStripId = gridName.GetAttribute("id");
            this.driver = (IJavaScriptExecutor)driver;
            this.kendoTab = string.Format(CultureInfo.InvariantCulture, "var tabStrip = $('#{0}').kendoTabStrip().data('kendoTabStrip');", this.tabStripId);
        }
        
        public void TabSelect(int tabIndx)
        {
            var tabCount = TabCount();
            if (tabIndx <= tabCount)
            {
                try
                {
                    string jsToBeExecuted = this.kendoTab;
                    string initializedTbIndex = string.Format("tabStrip.select('{0}');", tabIndx);
                    jsToBeExecuted = string.Concat(jsToBeExecuted, initializedTbIndex);
                    var jsResult = this.driver.ExecuteScript(jsToBeExecuted);
                    TabNameList();
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Console.Write("tabIndx should be <= tabCount.");
            }
        }

        public int TabCount()
        {
            string jsToBeExecuted = this.kendoTab;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return tabStrip.tabGroup.children().length");
            var maxTabIndexCount = this.driver.ExecuteScript(jsToBeExecuted);
            var tabCount =Convert.ToInt32(maxTabIndexCount);
            return tabCount;
        }

        public List<string> TabNameList()
        {
            string jsToBeExecuted = this.kendoTab;
            List<string> tabNameArray = new List<string>();
            var tempString=string.Format("var formatString=[];");
            jsToBeExecuted = string.Concat(jsToBeExecuted, tempString);
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return tabStrip.tabGroup.children().each(function(){ formatString.push($(this).text());});");
            var jsResult = this.driver.ExecuteScript(jsToBeExecuted);
            ReadOnlyCollection<IWebElement> webElementList= (ReadOnlyCollection<IWebElement>)jsResult;
            foreach (IWebElement js in webElementList)
            {
                var jsStringResult = js.Text;
                tabNameArray.Add(jsStringResult);
            }
            return tabNameArray;
        }

        public bool IsTabNamePresent(string tabName)
        {
            //var item = tabName.Trim();
            bool isTabName=false;
            foreach (var item in TabNameList())
            {
                if (item.Contains(tabName))
                {
                    isTabName = true;
                }
            }
            return isTabName;

            //if (tabName.Trim() == "AFS Comments")
            //{
            //    //
            //    var item = tabName.Trim();
            //    return TabNameList().Contains(item);
            //}
            //else
            //{
            //    var item = tabName.Trim();
            //    return TabNameList().Contains(item);
            //}
        }

        protected override string KendoName
        {
            get { return "tabstrip"; }
        }
    }
}
