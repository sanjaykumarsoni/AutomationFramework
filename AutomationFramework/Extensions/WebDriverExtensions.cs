using AutomationFramework.Base;
using AutomationFramework.Helpers;
using AutomationFramework.WebElements;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutomationFramework.Extensions
{
    /// <summary>
    /// Web Driver Extension methods 
    /// </summary>
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
            }
            return driver.FindElements(by);
        }

        public static IWebElement FocusedElement(this IWebDriver driver)
        {
            return driver.ScriptQuery<IWebElement>("return document.activeElement;");
        }

        public static IWebElement FindElement(this IWebDriver driver, ISearchContext searchContext, By by,
                                              int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv => searchContext.FindElement(by));
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, ISearchContext searchContext, By by,
                                                                   int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv => (searchContext.FindElements(by).Count > 0) ? searchContext.FindElements(by) : null);
        }

        public static void ScriptExecute(this IWebDriver driver, string script, params object[] args)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
        }

        public static T ScriptQuery<T>(this IWebDriver driver, string script, params object[] args)
        {
            return (T)((IJavaScriptExecutor)driver).ExecuteScript(script, args);
        }

        public static void WaitFor(this IWebDriver driver, string conditionExpression, int seconds = 15, params object[] args)
        {
            int cnt = 0;
            bool result;
            do
            {
                if (cnt >= seconds)
                {
                    throw new TimeoutException("Wait until true exceeded wait limit.");
                }

                if (cnt++ > 0)
                {
                    Thread.Sleep(1000);
                }

                string script = string.Format(@"return {0};", conditionExpression);
                result = driver.ScriptQuery<bool>(script, args);
            } while (result == false);
        }

        public static void ActionDrivers(this IWebDriver driver, string attribute)
        {
            //driver.WaitForElementToLoad("suggestion", 60);
            IWebElement element = driver.FindElement(By.Id(attribute));

            Actions actions = new Actions(driver);

            actions.MoveToElement(element).Click().Perform();
        }

        public static void SwitchToWindow(this IWebDriver driver, Expression<Func<IWebDriver, bool>> predicateExp)
        {
            var predicate = predicateExp.Compile();
            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                if (predicate(driver))
                {
                    return;
                }
            }

            throw new ArgumentException(string.Format("Unable to find window with condition: '{0}'", predicateExp.Body));
        }
       
        /// <summary>
        /// Wait For Element To Load
        /// </summary>
        /// <param name="driver">driver</param>
        public static void WaitForElementToLoad(this IWebDriver driver, int? timeoutInSeconds=null)
        {
            TimeSpan timeout = new TimeSpan(0, 0, 60);

            if (timeoutInSeconds.HasValue)
            {
                int timeoutInSecs = timeoutInSeconds.Value;
                timeout = new TimeSpan(0, 0, timeoutInSecs);
            }

            WebDriverWait wait = new WebDriverWait(driver, timeout);

            IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("driver", "Driver must support javascript execution");

            wait.Until((d) =>
            {
                try
                {
                    string readyState = javascript.ExecuteScript(
                    "if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower() == "complete";
                }
                catch (InvalidOperationException e)
                {
                    //Window is no longer available
                    return e.Message.ToLower().Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    //Browser is no longer available
                    return e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public static void WaitForElementToLoad(this IWebDriver driver, string attribute, int? timeoutInSeconds = null)
        {
            TimeSpan timeout = new TimeSpan(0, 0, 100);

            if (timeoutInSeconds.HasValue)
            {
                int timeoutInSecs = timeoutInSeconds.Value;
                timeout = new TimeSpan(0, 0, timeoutInSecs);
            }
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id(attribute)));
        }

        public static void WaitForJQuery(this IWebDriver driver, int timeoutInSeconds = 60)
        {
            var js = driver as IJavaScriptExecutor;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            if (js != null)
            {
                try
                {
                    wait.Until(d => (bool)js.ExecuteScript("return jQuery.active == 0"));
                }
                catch (Exception e)
                {
                }
            }
        }

        public static void InjectSeleniumExt(this IWebDriver driver)
        {
            var type = typeof(WebDriverExtensions);
            var assembly = type.Assembly;
            string[] resourceNames = assembly.GetManifestResourceNames().Where(x => Regex.IsMatch(x, @"AutomationFramework\.js")).OrderBy(x => x).ToArray();
            foreach (string resourceName in resourceNames)
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    ((IJavaScriptExecutor)driver).ExecuteScript(result);
                }
            }
        }

        public static IJavaScriptExecutor JavaScripts(this IWebDriver webDriver)
        {
            return (IJavaScriptExecutor)webDriver;
        }

        /// <summary>
        /// Handler for simple use JavaScriptAlert.
        /// </summary>
        /// <example>Sample confirmation for java script alert: <code>
        /// this.Driver.JavaScriptAlert().ConfirmJavaScriptAlert();
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <returns>JavaScriptAlert Handle</returns>
        public static JavaScriptAlert JavaScriptAlert(this IWebDriver webDriver)
        {
            return new JavaScriptAlert(webDriver);
        }

       
        public static void ImplicitWait(this IWebDriver driver, int waitSeconds)
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(waitSeconds));
        }

        /// <summary>
        /// Waits for all ajax actions to be completed.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="timeout">The timeout.</param>
        public static void WaitForAjax(this IWebDriver webDriver, double timeout)
        {
            try
            {
                new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout)).Until(
                    driver =>
                    {
                        var javaScriptExecutor = driver as IJavaScriptExecutor;
                        return javaScriptExecutor != null
                               && (bool)javaScriptExecutor.ExecuteScript("return jQuery.active == 0");
                    });
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// Wait for element to be displayed for specified time
        /// </summary>
        /// <example>Example code to wait for login Button: <code>
        /// this.Driver.IsElementPresent(this.loginButton, BaseConfiguration.ShortTimeout);
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="locator">The locator.</param>
        /// <param name="customTimeout">The timeout.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public static bool IsElementPresent(this IWebDriver webDriver, ElementLocator locator, double customTimeout)
        {
            try
            {
                webDriver.GetElement(locator, customTimeout, e => e.Displayed);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is page title] equals [the specified page title].
        /// </summary>
        /// <example>Sample code to check page title: <code>
        /// this.Driver.IsPageTitle(expectedPageTitle, BaseConfiguration.MediumTimeout);
        /// </code></example>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="pageTitle">The page title.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        /// Returns title of page
        /// </returns>
        public static bool IsPageTitle(this IWebDriver webDriver, string pageTitle, double timeout)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));

            try
            {
                wait.Until(d => d.Title.ToLower(CultureInfo.CurrentCulture) == pageTitle.ToLower(CultureInfo.CurrentCulture));
            }
            catch (WebDriverTimeoutException)
            {
                //Logger.Error(CultureInfo.CurrentCulture, "Actual page title is {0};", webDriver.Title);
                return false;
            }

            return true;
        }
    }
}

