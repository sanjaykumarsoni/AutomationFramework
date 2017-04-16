using AutomationFramework.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Drawing;

namespace AutomationFramework.Extensions
{
    public static class WebElementExtensions
    {
        /// <summary>
        /// Wait for elment to load based on GetAttribute By Id.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="timeoutInSeconds"></param>
        public static void WaitForElementToLoad(this IWebElement element, int? timeoutInSeconds = null)
        {
            TimeSpan timeout = new TimeSpan(0, 0, 60);

            if (timeoutInSeconds.HasValue)
            {
                int timeoutInSecs = timeoutInSeconds.Value;
                timeout = new TimeSpan(0, 0, timeoutInSecs);
            }
            WebDriverWait wait = new WebDriverWait(Hooks.Driver, timeout);
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id(element.GetAttribute("Id"))));
        }

        public static void DefaultWait(this IWebElement element, int waitInSec)
        {
            DefaultWait<IWebElement> wait = new DefaultWait<IWebElement>(element);
            wait.Timeout = TimeSpan.FromSeconds(waitInSec);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);

            Func<IWebElement, bool> waiter = new Func<IWebElement, bool>((IWebElement ele) =>
            {
                String id = element.GetAttribute("id");
                if (!string.IsNullOrEmpty(id))
                {
                    return true;
                }
                Console.WriteLine("Element {0} is still not clickable" + id);
                return false;
            });
            wait.Until(waiter);
        }

        public static void WaitForElementVisible(By by, int timeOutInSeconds)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var wait = new WebDriverWait(Hooks.Driver, TimeSpan.FromSeconds(timeOutInSeconds));
                wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception)
            {
                Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed.Seconds);
            }
            finally
            {
                stopwatch.Stop();
            }
        }
      
        #region Layouts
        /// <summary>
        /// Get the text alignment of particular controls.
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>returns start, end. start is nothing but left and end is right alignment. for further info read abut css text alignment for kendo</returns>
        public static string TextAlign(this IWebElement element)
        {
            return element.GetCssValue("text-align");
        }

        /// <summary>
        /// Get the font size of text.
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>return font size</returns>
        public static string FontSize(this IWebElement element)
        {
            return element.GetCssValue("font-size");
        }

        /// <summary>
        /// Returns the colour code.
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>the the color code</returns>
        public static string Colour(this IWebElement element)
        { 
            var clr  = element.GetCssValue("background-color");
            string[] rgb = clr.Replace("rgba(", "").Replace(")", "").Split(',');
            Color myColor = Color.FromArgb(Convert.ToInt32(rgb[3]), Convert.ToInt32(rgb[0]), Convert.ToInt32(rgb[1]), Convert.ToInt32(rgb[2]));
            string hex = string.Format("#{0:X2}{1:X2}{2:X2}", myColor.R, myColor.G, myColor.B);
            return hex;        
        }
    
        /// <summary>
        /// Font weight
        /// </summary>
        /// <param name="element">elment</param>
        /// <returns>font weight</returns>
        public static string Fontweight(this IWebElement element)
        {
            return element.GetCssValue("font-weight");
        }
        /// <summary>
        /// verticle alignment
        /// </summary>
        /// <param name="element">elment</param>
        /// <returns> verticle alignment</returns>
        public static string VerticalAlign(this IWebElement element)
        {
            return element.GetCssValue("vertical-align");
        }


        #endregion

        /// <summary>
        /// Select by name of radio buttons. Select the deselected Radio Button/Check Box in the group of same name by giving postionTobeClicked. 
        /// </summary>
        /// <param name="elementList">Store all the elements of same category in the list of WebLements.</param>
        /// <param name="postion">postion to be clicked. Example : 0, 1 , 2 etc</param>
        public static void SelectCheckboxOrRadioBtn(this IList<IWebElement> elementList, int postionTobeClicked)
        {
            // Create a boolean variable which will hold the value (True/False)
            bool bValue = false;

            // This statement will return True, in case of the postionTobeClicked Radio/Checkbox button is already selected
            bValue = elementList.ElementAt(postionTobeClicked).Selected;

            // This will check that if the bValue is True means if the postionTobeClicked radio button is selected
            if (bValue == true)
            {
                //don't select since it has been already selected.
            }
            else
            {
                // If the postionTobeClicked radio button is not selected by default, then postionTobeClicked will be selected
                elementList.ElementAt(postionTobeClicked).Click();
            }
        }

        /// <summary>
        /// This method will check that whether the default value has been selected on radio btn or checkbox at the default position or not.
        /// </summary>
        /// <param name="elementList">Store all the elements of same category in the list of WebLements.</param>
        /// <param name="defaultPosition">default position to be selected. 
        /// Example: if Group RadiobuttonName contains three radio button then the position will be repectively 0, 1 and 2  </param>
        /// <returns>boolean value whether the default position holding boolean value as same as expected ?.</returns>
        public static bool IsCheckboxOrRadiobtnSelectedByDefault(this IList<IWebElement> elementList, int defaultPosition)
        {
            // Create a boolean variable which will hold the value (True/False)
            bool bValue = false;
            // This statement will return True, in case of the default Radio/Checkbox button is selected
            bValue = elementList.ElementAt(defaultPosition).Selected;
            return bValue;
        }
        /// <summary>
        /// Clear the text controls. fill into text and Get the filled text back from control.
        /// </summary>
        /// <param name="element">element to be interact</param>
        /// <param name="value"> value to be text</param>
        public static string EnterText(this IWebElement element, string value)
        {
            //In Selenium 2, text fields are not cleared unless explicitly cleared.
            if (!String.IsNullOrEmpty(element.GetText()))
            {
                element.Clear();
            }
            element.SendKeys(value);
            return element.GetText();
        }

        /// <summary>
        /// Click into button, checkbox, radio button.
        /// </summary>
        /// <param name="element"></param>
        public static void WaitAndClick(this IWebElement element, int timeInSec)
        {
            element.WaitForElementToLoad(timeInSec);
            element.Click();
        }

        /// <summary>
        /// Click on element using java script. it will be hold good if your element is not visible at the time element interactions.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public static void JavaScriptClick(this IWebElement webElement)
        {
            IWrapsDriver wrappedElement = webElement as IWrapsDriver;
            if (wrappedElement == null)
                throw new ArgumentException("element", "Element must wrap a web driver");

            IWebDriver driver = wrappedElement.WrappedDriver;
            IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;

            if (javascript == null)
            {
                throw new ArgumentException("Element must wrap a web driver that supports javascript execution");
            }

            javascript.ExecuteScript("arguments[0].click();", webElement);
        }

        /// <summary>
        /// Select DropDown List given value from list of element and fix the timeout if required.
        /// </summary>
        /// <param name="ElementList">List of Elements which hold the value</param>
        /// <param name="value">Value to be selected</param>
        /// <param name="timeoutInSeconds">Default timeout in seconds if required. Exception throws after time out</param>
        public static void SelectDropDown(this IList<IWebElement> ElementList, string value, int? timeoutInSeconds = null)
        {
            //default timespan
            TimeSpan timeout = new TimeSpan(0, 15, 60);

            if (timeoutInSeconds.HasValue)
            {
                int timeoutInSecs = timeoutInSeconds.Value;
                timeout = new TimeSpan(0, 0, timeoutInSecs);
            }

            WebDriverWait wait = new WebDriverWait(Hooks.Driver, timeout);

            foreach (var thisElement in ElementList)
            {
                IWebElement webElement = thisElement;
                // try/Catch block to handel the exception if the define timespan timeout
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(webElement));
                }
                catch (InvalidOperationException e)
                {
                    //Window is no longer available
                    e.Message.ToLower().Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    //Browser is no longer available
                    e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {

                }
                if (webElement.Text.Equals(value))
                {
                    webElement.Click();
                    break;
                }
            }
        }

       /// <summary>
       /// Get text based on attrbiute input value. it can be value, cssClass, Id etc.
       /// </summary>
       /// <param name="element"></param>
        /// <param name="getAttributeBy">get Attribute By id, cssClass, value etc. it is optional parameter.</param>
       /// <returns></returns>
        public static string GetText(this IWebElement element, string getAttributeBy="")
        {
            if (string.IsNullOrEmpty(getAttributeBy))
            {
                element.GetAttribute("value");
            }
            return element.GetAttribute(getAttributeBy);
        }
        /// <summary>
        /// Get from the text placehoder.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetTextByPlaceholder(this IWebElement element)
        {
            return element.GetAttribute("placeholder");
        }
        /// <summary>
        /// Get selected value from drop down list.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetTextFromDDL(this IWebElement element)
        {
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text;
        }

        /// <summary>
        /// The function returns the text in the entire page without any HTML code
        /// </summary>
        /// <param name="driver">driver</param>
        /// <param name="by">Element Type</param>
        /// <returns>true/false</returns>
        public static bool HasElement(this IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// allow us to test for the visibility of the elements without stopping execution of the entire program.
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>return bool as true if item has been displayed</returns>
        public static bool IsDisplayed(this IWebElement element)
        {
            bool result;
            try
            {
                result = element.Displayed;
            }
            catch (Exception)
            {
                result = false;
            }
            // Log the Action
            return result;
        }


        /// <summary>
        /// Set Attribute on element with attribute name and value.
        /// </summary>
        /// <param name="element">element</param>
        /// <param name="attributeName">Attribute name</param>
        /// <param name="value">value to be set</param>
        public static void SetAttribute(this IWebElement element, string attributeName, string value)
        {
            IWrapsDriver wrappedElement = element as IWrapsDriver;
            if (wrappedElement == null)
                throw new ArgumentException("element", "Element must wrap a web driver");

            IWebDriver driver = wrappedElement.WrappedDriver;
            IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("element", "Element must wrap a web driver that supports javascript execution");

            javascript.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])", element, attributeName, value);
        }

        /// <summary>
        /// SelectByText of Select Class
        /// </summary>
        /// <param name="element">element</param>
        /// <param name="text">text to be selected</param>
        public static void SelectByText(this IWebElement element, string text)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(text);
        }

        /// <summary>
        /// Click with href url partial name which holds the unique url always.
        /// </summary>
        /// <param name="e">element </param>
        /// <param name="href">href text name which contains partial name</param>
        /// <param name="position">if there is more then 1 name presents in url</param>
        public static void ClickLinkByHref(this IWebElement e, String href, int? position = null)
        {
            ReadOnlyCollection<IWebElement> anchors = e.FindElements(By.TagName("a"));
            for (int i = 0; i < anchors.Count(); i++)
            {
                int j = 0;
                IWebElement anchor = anchors[i];
                if (position == null)
                {
                    position = 0;
                }
                else if (anchor.GetAttribute("href").Contains(href))
                {
                    j++;
                }
                // Don't remove below commented code untill tested for all other scenario - Sanjay Soni.

                //if (anchor.GetAttribute("href").Contains(href)
                //           && j == position)
                //{
                //    anchor.Click();
                //    break;
                //}
                //if (anchor.GetAttribute("href").Contains(href) && j == position)
                //{
                    anchor.Click();
                    break;
                //}
            }
        }

        /// <summary>
        /// Returns the textual content of the specified node, and all its descendants regardless element is visible or not.
        /// </summary>
        /// <param name="webElement">The web element</param>
        /// <returns>The attribute</returns>
        /// <exception cref="ArgumentException">Element must wrap a web driver
        /// or
        /// Element must wrap a web driver that supports java script execution</exception>
        public static string GetTextContent(this IWebElement webElement)
        {
            var javascript = webElement.ToDriver() as IJavaScriptExecutor;
            if (javascript == null)
            {
                throw new ArgumentException("Element must wrap a web driver that supports javascript execution");
            }

            var textContent = (string)javascript.ExecuteScript("return arguments[0].textContent", webElement);
            return textContent;
        }

        /// <summary>
        /// parent element to fetch the value from calander or date picker. Howerver,  we can use the below method on any control if there is parent-chld relations.
        /// </summary>
        /// <param name="e">element</param>
        /// <returns>Parent web elements. Example : Kendo DatePicker or Kendo Calander Parant year.</returns>
        public static IWebElement Parent(this IWebElement e)
        {
            return e.FindElement(By.XPath(".."));
        }

        /// <summary>
        /// Next sibling of parant element. 
        /// </summary>
        /// <param name="e">element</param>
        /// <returns>Return next sibiling element. Example Kendo DatePicker or Kendo Calander</returns>
        public static IWebElement NextSibling(this IWebElement e)
        {
            return e.NextSibling("*");
        }

        /// <summary>
        /// Next sibiling with xpath
        /// </summary>
        /// <param name="e">element</param>
        /// <param name="xpath">xpathof calender next button</param>
        /// <returns>Return next sibiling element. Example Kendo DatePicker or Kendo Calander</returns>
        public static IWebElement NextSibling(this IWebElement e, string xpath)
        {
            return e.FindElement(By.XPath(xpath));
        }

        /// <summary>
        /// Previous sibiling of parent element.
        /// </summary>
        /// <param name="e">element</param>
        /// <returns>Return previous sibiling element. Example Kendo DatePicker or Kendo Calander</returns>
        public static IWebElement PreviousSibling(this IWebElement e)
        {
            return e.PreviousSibling("*");
        }

        /// <summary>
        /// Previous sibiling of parent element with xpath
        /// </summary>
        /// <param name="e">element</param>
        /// <param name="xpath">xpath of previous calander button</param>
        /// <returns>eturn previous sibiling element. Example Kendo DatePicker or Kendo Calander </returns>
        public static IWebElement PreviousSibling(this IWebElement e, string xpath)
        {
            return e.FindElement(By.XPath("..//preceding-sibling::{xpath}"));
        }
    }
}
