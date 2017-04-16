using AutomationFramework.Extensions;
using AutomationFramework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace TradeProTests.Pages
{
    public class LoginPage
    {
        [FindsBy(How = How.Name, Using = "ctl00$ContentPlaceHolder1$txtLogin")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Name, Using = "ctl00$ContentPlaceHolder1$txtPassword")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Name, Using = "ctl00$ContentPlaceHolder1$AsiButton1")]
        public IWebElement LoginBtn { get; set; }

        [FindsBy(How = How.Id, Using = "ContentPlaceHolder1_txtEmail")]
        public IWebElement EmailText { get; set; }

        [FindsBy(How = How.Name, Using = "ctl00$ContentPlaceHolder1$btnRequest")]
        public IWebElement Okbtn { get; set; }

        
        
        /// <summary>
        /// TDD implementaion through Excel.
        /// </summary>
        public string LoginToApplication(string[] data)
        {
            //var item = ExcelHelper.ReadDataFromKey("default");
            string email =string.Empty;
            //if (item.ToLower().Contains("default"))
            //{
                //email = UserName.EnterText(ExcelHelper.ReadData(2, "UserName"));
                //Password.EnterText(ExcelHelper.ReadData(2, "Password"));
                email = UserName.EnterText(data[0]);
                Password.EnterText(data[1]);
                LoginBtn.Click();
                Console.WriteLine("Login To Application Passed");
            //}
            //else
            //{
            //    Console.WriteLine("Login To Application failed. please seleect the default credential from Excel");
            //}
           
            return email;
        }

        //Method for Forget password link//
        public void ForgetPassword(string value)
        {
            EmailText.EnterText(value);
            Console.WriteLine("Clicked Forget Password link");
        }

        //Method for Click event on forget password page//
        public void ClickOkBtn()
        {
            Okbtn.Click();
            Console.WriteLine("Ok button Clicked on Forget password page");
        }


    }
}
