using AutomationFramework.Base;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using AutomationFramework.Extensions;

namespace AutomationFramework.KendoWrapper
{
    public class DatePicker : KendoWidget
    {
        private readonly string dtp;
        private readonly IJavaScriptExecutor driver;
        private IWebElement element;
        public DatePicker(IWebElement dtp)
            : base(dtp)
        {
            IWebDriver driver = Hooks.Driver;
            this.dtp = dtp.GetAttribute("id");
            this.driver = (IJavaScriptExecutor)driver;
            element = dtp;
        }

        /// <summary>
        /// It will select any datepicker. Select the hard code date in date picker. 
        /// Make sure to select the correct date format else you will experince UI issues.
        /// </summary>
        /// <param name="stringDatePickerValue">correct date format value</param>
        public void KendoDatePicker(string stringDatePickerValue)
        {
            string js = string.Format("var dateTemp = document.getElementById('{0}');", this.dtp);
            js = string.Concat(js, "dateTemp.value = '", stringDatePickerValue, "';");
            js = string.Concat(js, "datepicker_change();");
            this.driver.ExecuteScript(js);
        }
        
        public Calendar GetCalendar()
        {
            var div = (ReadOnlyCollection<IWebElement>)this.driver.ExecuteScript(Scripts.DatePicker_dateView_div, FindElement());
            var calendarContainer = div[0];
            var calendarElement = calendarContainer.FindElement(By.XPath(".//div[@class='k-widget k-calendar']"));

            Calendar calendar = new Calendar(element, By.Id(calendarElement.GetAttribute("id")), "kendoCalendar");
            return calendar;
        }
        public void ClickCalendarIcon()
        {
            var element = FindElement().NextSibling("..//span[@class='k-select']");
            element.Click();
            //bool PopupCalendarVisible = (bool)this.driver.ExecuteScript(Scripts.DatePicker_dateView_popup_visible, FindElement());
            //string Format = (string)this.driver.ExecuteScript(Scripts.DatePicker_options_format, FindElement());
            //string Depth = (string)this.driver.ExecuteScript(Scripts.DatePicker_options_depth, FindElement());
            //string Start = (string)this.driver.ExecuteScript(Scripts.DatePicker_options_start, FindElement());
            //string Footer = (string)this.driver.ExecuteScript(Scripts.DatePicker_options_footer, FindElement());
            //string Prefix = (string)this.driver.ExecuteScript(Scripts.DatePicker_options_prefix, FindElement());
        }   

        /// <summary>
        ///Select Kendo Date
        /// </summary>
        /// <param name="date">date in string</param>
        /// <param name="format">date format</param>
        /// <returns>kendo date</returns>
        public DatePicker SelectKendoDate(String date, string format)
        {
            DateTime tempDate = Convert.ToDateTime(date).Date;
            string jsToBeExecuted = this.GetCalenderReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "datePicker.kendoDatePicker({value:'", date, "', format:'", format, "', start:'", "year", "', depth:'", "year", "'});");
            this.driver.ExecuteScript(jsToBeExecuted);
            return this;
        }

        protected override string KendoName
        {
            get { return "kendoDatePicker"; }
        }

        private string FormatDate(DateTime value, string format)
        {
            var formatted = value.ToString(format);
            return formatted;
        }

        private string FormatDate(DateTime value, string format, IFormatProvider provider)
        {
            var formatted = value.ToString(format, provider);
            return formatted;
        }

        private string GetCalenderReference()
        {
            string initializeKendoDatePicker = string.Format("var datePicker = $('#{0}').kendoDatePicker();", this.dtp);
            return initializeKendoDatePicker;
        }
    }
}
