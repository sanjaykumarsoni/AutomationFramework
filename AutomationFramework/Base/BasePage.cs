using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Base
{
   /// <summary>
   /// To intitantiate any class Instances using factory pattern. return GetPage<YourPageToIntanciate>()>
   /// </summary>
    public abstract class BasePage
    {
        //Page generator using genrics and constraint to avoid constructor invocation every time to create instance for each page class.
        public static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(Hooks.Driver, page);
            return page;
        }
    }
}
