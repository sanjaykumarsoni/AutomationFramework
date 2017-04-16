using AutomationFramework.Base;
using AutomationFramework.Enums;
using AutomationFramework.Helpers;
using Mono.Collections.Generic;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.Extensions;

namespace AutomationFramework.KendoWrapper
{
    public abstract class KendoSelect 
    {
        private readonly ElementLocator kendoSelect = new ElementLocator(
            Locator.XPath,
            "./ancestor-or-self::span[contains(@class, 'k-widget')]//input[@id]");

        private readonly IWebElement webElement;
        private readonly IJavaScriptExecutor driver;

        /// <summary>
        ///     Initializes a new instance of the <see cref="KendoSelect" /> class.
        /// </summary>
        /// <param name="webElement">The webElement</param>
        public KendoSelect(IWebElement element)
        {
            driver = (IJavaScriptExecutor)Hooks.Driver;
            this.webElement = element;
            var id = element.GetAttribute("id");// this.webElement.GetElement(this.kendoSelect, e => e.Displayed == false).GetAttribute("id");
            this.ElementCssSelector = string.Format(CultureInfo.InvariantCulture, "#{0}", id);
        }

        /// <summary>
        ///     Gets the driver.
        /// </summary>
        //public IWebDriver Driver
        //{
        //    get
        //    {
        //        return this.webElement.ToDriver();
        //    }
        //}

        /// <summary>Gets the unordered list.</summary>
        /// <value>The unordered list.</value>
        public IWebElement UnorderedList
        {
            get
            {
                var element =
                    (IWebElement)Hooks.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "return $('{0}').data('{1}').ul.toArray()[0];",
                                this.ElementCssSelector,
                                this.SelectType));

                return element;
            }
        }

        /// <summary>
        ///     Gets the options.
        /// </summary>
        public Collection<string> Options
        {
            get
            {
                var elements =
                    (ReadOnlyCollection<IWebElement>)Hooks.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "return $('{0}').data('{1}').ul.children().toArray();",
                                this.ElementCssSelector,
                                this.SelectType));
                var options = elements.Select(element => element.GetTextContent()).ToList();
                return new Collection<string>(options);
            }
        }

        /// <summary>Gets the selected option.</summary>
        /// <value>The selected option.</value>
        public string SelectedOption
        {
            get
            {
                var option =
                    (string)Hooks.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "return $('{0}').data('{1}').text();",
                                this.ElementCssSelector,
                                this.SelectType));
                return option;
            }
        }

        /// <summary>
        /// Gets or sets the element selector.
        /// </summary>
        protected string ElementCssSelector { get; set; }

        /// <summary>Gets the selector.</summary>
        /// <value>The selector.</value>
        protected abstract string SelectType { get; }

        /// <summary>Select by text.</summary>
        /// <param name="text">The text.</param>
        public void SelectByText(string text)
        {
            Hooks.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "$('{0}').data('{1}').select(function(dataItem) {{return dataItem.text === '{2}';}});",
                        this.ElementCssSelector,
                        this.SelectType,
                        text));
        }

        /// <summary>Closes this object.</summary>
        public void Close()
        {
            Hooks.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "$('{0}').data('{1}').close();",
                        this.ElementCssSelector,
                        this.SelectType));
        }

        /// <summary>Opens this object.</summary>
        public void Open()
        {
            Hooks.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "$('{0}').data('{1}').open();",
                        this.ElementCssSelector,
                        this.SelectType));
        }
    }
}
