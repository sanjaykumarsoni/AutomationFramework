using AutomationFramework.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.KendoWrapper
{
    public class KendoMultiSelectDropDownList: KendoWidget
    {
        private readonly string ddlmultiselectId;
        private readonly IJavaScriptExecutor driver;
        /// <summary>
        ///     Initializes a new instance of the <see cref="KendoComboBox" /> class.
        /// </summary>
        /// <param name="webElement">The webElement</param>
        public KendoMultiSelectDropDownList(IWebElement ddlmultiselectId)
            : base(ddlmultiselectId)
        {
            IWebDriver driver = Hooks.Driver;
            this.ddlmultiselectId = ddlmultiselectId.GetAttribute("id");
            this.driver = (IJavaScriptExecutor)driver;
        }
        protected override string KendoName
        {
            get { return "kendoMultiSelect"; }
        }
        //private KendoMultiSelectDropDownList Select(string dataTextFieldName, string valueOfDataTextField)
        //{
        //    ScriptExecute(string.Format("$k.select(function(dataItem) {{ return dataItem.{0}.trim() === '{1}';}});$k.trigger('change');", dataTextFieldName, valueOfDataTextField));
        //    return this;
        //}
        public void Select(string valueOfDataTextField)
        {
            //return Select(GetDataTextFieldName(), valueOfDataTextField);
             ((IJavaScriptExecutor)driver).ExecuteScript(String.Format("$('#{0}').data('kendoMultiSelect').value('{1}');", this.ddlmultiselectId, valueOfDataTextField));
        }

        //private string GetDataTextFieldName()
        //{
        //    string dataRoleElement = ScriptQuery<string>(string.Format(" return $('#{0}').data('kendoMultiSelect').options.dataTextField", this.ddlmultiselectId));
        //    return dataRoleElement;
        //}
    }
}
