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
    /// window Session storage to store data temporary and allow to execute crud operation untill session expired.
    /// </summary>
    public static class SessionStorage
    {
        static IJavaScriptExecutor js;
        static SessionStorage()
        {
            js = (IJavaScriptExecutor)Hooks.Driver;
        }
        public static void RemoveItemFromSessionStorage(String item)
        {
            js.ExecuteScript(String.Format(
                    "window.sessionStorage.removeItem('{0}');", item));
        }

        public static bool IsItemPresentInSessionStorage(String item)
        {
            if (js.ExecuteScript(String.Format(
                    "return window.sessionStorage.getItem('{0}');", item)) == null)
                return false;
            else
                return true;
        }

        public static String GetItemFromSessionStorage(String key)
        {
            return (String)js.ExecuteScript(String.Format(
                    "return window.sessionStorage.getItem('{0}');", key));
        }

        public static String GetKeyFromSessionStorage(int key)
        {
            return (String)js.ExecuteScript(String.Format(
                    "return window.sessionStorage.key('{0}');", key));
        }

        public static int GetSessionStorageLength()
        {
            return (int)js.ExecuteScript("return window.sessionStorage.length;");
        }

        public static void SetItemInSessionStorage(String item, String value)
        {
            js.ExecuteScript(String.Format(
                    "window.sessionStorage.setItem('{0}','{1}');", item, value));
        }

        public static void ClearSessionStorage()
        {
            js.ExecuteScript(String.Format("window.sessionStorage.clear();"));
        }
    }
}
