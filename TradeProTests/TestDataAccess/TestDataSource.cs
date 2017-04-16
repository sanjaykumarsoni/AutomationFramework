using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeProTests.TestDataAccess
{
    public class TestDataSource
    {
        public static IEnumerable<String> BrowserToRunWith()
        {
            String[] browsers = AutomationSetting.BrowserToRunWith.Split(',');
            foreach (String b in browsers)
            {
                yield return b;
            }
        }
    }
}
