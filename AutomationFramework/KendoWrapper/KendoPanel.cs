using AutomationFramework.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.KendoWrapper
{
    public class KendoPanel
    {
        private readonly IJavaScriptExecutor driver;
        public KendoPanel()
        {
            IWebDriver driver = Hooks.Driver;
            this.driver = (IJavaScriptExecutor)driver;
        }
        public int IsChildToParentCssClassPresent(IWebElement element)
        {
            int count = 0;
            string panelId = element.GetAttribute("id");
            string temp = string.Empty;
            if (panelId == "transactionschartWatermark")
            {
                temp = string.Format(CultureInfo.InvariantCulture, "var panel = $('#{0}').parent().parent().attr('class'); return panel;", panelId);
            }
            else if (panelId == "newsfeed-panel")
            {
                temp = string.Format(CultureInfo.InvariantCulture, "var panel = $('#{0}').parent().attr('class'); return panel;", panelId);
            }
            var item = (string)driver.ExecuteScript(temp);
            if (item.Contains("panel panel-default"))
            {
                count++;
            }
            return count; ;

        }
    }
}
