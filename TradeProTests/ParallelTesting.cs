using AutomationFramework.Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradeProTests.Pages;

namespace TradeProTests
{
    [TestFixture]
    [Parallelizable]
    public class FirfoxTest : Hooks
    {
        public FirfoxTest()
            : base(BrowserType.Firfox)
        {

        }
        //[Test]
        public void FirefoxLoginTest()
        {
            Driver.Navigate().GoToUrl("https://authasidevtm1b.answerssystems.com/");
            Thread.Sleep(5000);
        }
    }

    [TestFixture]
    [Parallelizable]
    public class GoolgleTest : Hooks
    {
        public GoolgleTest():base(BrowserType.Chrome)
        {

        }
        //[Test]
        public void GoogleLoginTest()
        {
            Driver.Navigate().GoToUrl("https://authasidevtm1b.answerssystems.com/");
            Thread.Sleep(5000);
        }
    }
    [TestFixture]
    [Parallelizable]
    public class IETest : Hooks
    {
        public IETest():base(BrowserType.IE)
        {
                   
        }
        //[Test]
        public void IELoginTest()
        {
            Driver.Navigate().GoToUrl("https://authasidevtm1b.answerssystems.com/");
        }
    }

}
