using AutomationFramework.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.Extensions;

namespace AutomationFramework.KendoWrapper
{
    /// <summary>
    /// Kendo controls which has 1 or 2 methods and need not required to create separate file for each controls.
    /// </summary>
    public class KendoCommonUtilities
    {
        public static bool IsModelPopUpClosed()
        {
            var jsTobeExecuted = Hooks.Driver.ScriptQuery<bool>(string.Format("return $('#window').parent().is(':visible');"));
            return jsTobeExecuted;
        }
    }
}
