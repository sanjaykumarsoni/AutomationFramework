using AutomationFramework.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Helpers
{
    /// <summary>
    /// window Local storage to store data temporary and allow to execute crud operation.
    /// </summary>
    public static class LocalStorage
    {
        static IJavaScriptExecutor js;

        static LocalStorage()
        {
            js = (IJavaScriptExecutor)Hooks.Driver;
        }

        public static void RemoveItemFromLocalStorage(String item)
        {
            js.ExecuteScript(String.Format(
                "window.localStorage.removeItem('{0}');", item));
        }

        public static bool IsItemPresentInLocalStorage(String item)
        {
            return !(js.ExecuteScript(String.Format(
                "return window.localStorage.getItem('{0}');", item)) == null);
        }

        public static String GetItemFromLocalStorage(String key)
        {
            return (String)js.ExecuteScript(String.Format(
                "return window.localStorage.getItem('{0}');", key));
        }

        public static String GetKeyFromLocalStorage(int key)
        {
            return (String)js.ExecuteScript(String.Format(
                "return window.localStorage.key('{0}');", key));
        }

        public static int GetLocalStorageLength()
        {
            return (int)js.ExecuteScript("return window.localStorage.length;");
        }

        public static void SetItemInLocalStorage(String item, String value)
        {
            js.ExecuteScript(String.Format(
                "window.localStorage.setItem('{0}','{1}');", item, value));
        }

        public static void ClearLocalStorage()
        {
            js.ExecuteScript(String.Format("window.localStorage.clear();"));
        }

    }
}
