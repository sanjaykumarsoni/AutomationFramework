using AutomationFramework.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Mono.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using AutomationFramework.Extensions;
using System.Globalization;

namespace AutomationFramework.KendoWrapper
{
    /// <summary>
    /// Kendo grid Wrappers. Sample hass been implemented.
    /// </summary>
    public class KendoGrid : KendoWidget
    {
        private readonly string gridId;
        private readonly IJavaScriptExecutor driver;
        private readonly string kendoGrid;
        private readonly string kendoGridForHref; 
        public KendoGrid(IWebElement gridName)
            : base(gridName)
        {
            IWebDriver driver = Hooks.Driver;
            this.gridId = gridName.GetAttribute("id");
            this.driver = (IJavaScriptExecutor)driver;
            this.kendoGridForHref = string.Format(CultureInfo.InvariantCulture, "$('#{0}').data('kendoGrid')", this.gridId);
            this.kendoGrid= string.Format(CultureInfo.InvariantCulture, "var grid = $('#{0}').data('kendoGrid');", this.gridId);
        }

        public void RemoveFilters()
        {
            string jsToBeExecuted = this.kendoGrid;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.filter([]);");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public int TotalNumberRows()
        {
            string jsToBeExecuted = this.kendoGrid;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.total();");
            var jsResult = this.driver.ExecuteScript(jsToBeExecuted);
            return int.Parse(jsResult.ToString());
        }

        public void Reload()
        {
            string jsToBeExecuted = this.kendoGrid;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.read();");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public int GetPageSize()
        {
            string jsToBeExecuted = this.kendoGrid;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.pageSize();");
            var currentResponse = this.driver.ExecuteScript(jsToBeExecuted);
            int pageSize = int.Parse(currentResponse.ToString());
            return pageSize;
        }

        public void ChangePageSize(int newSize)
        {
            string jsToBeExecuted = this.kendoGrid;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.pageSize(", newSize, ");");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public void NavigateToPage(int pageNumber)
        {
            string jsToBeExecuted = this.kendoGrid;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.page(", pageNumber, ");");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public void Sort(string dataField, SortType sortType)
        {
            string jsToBeExecuted = this.kendoGrid;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.sort({field: '", dataField, "', dir: '", sortType.ToString().ToLower(), "'});");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public List<T> GetItems<T>() where T : class
        {
            string jsToBeExecuted = this.kendoGrid;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return JSON.stringify(grid.dataSource.data());");
            var jsResults = this.driver.ExecuteScript(jsToBeExecuted);
            var items = JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
            return items;
        }

        public void Filter(string columnName, FilterOperator filterOperator, string filterValue)
        {
            this.Filter(new GridFilter(columnName, filterOperator, filterValue));
        }

        public void Filter(params GridFilter[] gridFilters)
        {
            string jsToBeExecuted = this.kendoGrid;
            StringBuilder sb = new StringBuilder();
            sb.Append(jsToBeExecuted);
            sb.Append("grid.dataSource.filter({ logic: \"and\", filters: [");
            foreach (var currentFilter in gridFilters)
            {
                DateTime filterDateTime;
                bool isFilterDateTime = DateTime.TryParse(currentFilter.FilterValue, out filterDateTime);
                string filterValueToBeApplied =
                                                isFilterDateTime ? string.Format("new Date({0}, {1}, {2})", filterDateTime.Year, filterDateTime.Month - 1, filterDateTime.Day) :
                                                string.Format("\"{0}\"", currentFilter.FilterValue);
                string kendoFilterOperator = this.ConvertFilterOperatorToKendoOperator(currentFilter.FilterOperator);
                sb.Append(string.Concat("{ field: \"", currentFilter.ColumnName, "\", operator: \"", kendoFilterOperator, "\", value: ", filterValueToBeApplied, " },"));
            }
            sb.Append("] });");
            jsToBeExecuted = sb.ToString().Replace(",]", "]");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public int GetCurrentPageNumber()
        {
            string jsToBeExecuted = this.kendoGrid;
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.page();");
            var result = this.driver.ExecuteScript(jsToBeExecuted);
            int pageNumber = int.Parse(result.ToString());
            return pageNumber;
        }

        private string ConvertFilterOperatorToKendoOperator(FilterOperator filterOperator)
        {
            string kendoFilterOperator = string.Empty;
            switch (filterOperator)
            {
                case FilterOperator.EqualTo:
                    kendoFilterOperator = "eq";
                    break;
                case FilterOperator.NotEqualTo:
                    kendoFilterOperator = "neq";
                    break;
                case FilterOperator.LessThan:
                    kendoFilterOperator = "lt";
                    break;
                case FilterOperator.LessThanOrEqualTo:
                    kendoFilterOperator = "lte";
                    break;
                case FilterOperator.GreaterThan:
                    kendoFilterOperator = "gt";
                    break;
                case FilterOperator.GreaterThanOrEqualTo:
                    kendoFilterOperator = "gte";
                    break;
                case FilterOperator.StartsWith:
                    kendoFilterOperator = "startswith";
                    break;
                case FilterOperator.EndsWith:
                    kendoFilterOperator = "endswith";
                    break;
                case FilterOperator.Contains:
                    kendoFilterOperator = "contains";
                    break;
                case FilterOperator.NotContains:
                    kendoFilterOperator = "doesnotcontain";
                    break;
                case FilterOperator.IsAfter:
                    kendoFilterOperator = "gt";
                    break;
                case FilterOperator.IsAfterOrEqualTo:
                    kendoFilterOperator = "gte";
                    break;
                case FilterOperator.IsBefore:
                    kendoFilterOperator = "lt";
                    break;
                case FilterOperator.IsBeforeOrEqualTo:
                    kendoFilterOperator = "lte";
                    break;
                default:
                    throw new ArgumentException("The specified filter operator is not supported.");
            }

            return kendoFilterOperator;
        }

        public KendoGrid Select(int index)
        {
            ScriptExecute("$k.select('tr:eq(" + index + ")');");
            return this;
        }

        public ReadOnlyCollection<IWebElement> Select()
        {
            return ScriptQuery<ReadOnlyCollection<IWebElement>>(@"var selected = $k.select();var result = [];for(var i =0; i<selected.length; i++){	result.push(selected[i]);}return result;");
        }

        //protected override string KendoName
        //{
        //    get { return "kendoGrid"; }
        //}

        public KendoGrid InvokeCreate()
        {
            ScriptExecute("$k.addRow();");
            return this;
        }

        public int Total()
        {
            var total = ScriptQuery<int>("return $k.dataSource.total();", (retrived, cycles) => retrived == 0 && cycles < 10);

            return total;
        }

        public int TotalPage()
        {
            return ScriptQuery<int>("return $k.dataSource.totalPages();", (retrived, cycles) => retrived == 0 && cycles < 10);
        }

        //public T DataItem<T>(IWebElement tr)
        //{
        //    var result = this.driver.ScriptQuery<string>("return JSON.stringify(  $(arguments[0]).data('" + KendoName + "').dataItem($(arguments[1]))  );", KendoWidgetHtmlElement(), tr);
        //    T model = JsonConvert.DeserializeObject<T>(result);
        //    return model;
        //}

        public IWebElement GetTableRowByModelId(int id)
        {
            var modelId = string.Format("{0}", id);
            Func<bool> dataItemExistsOnCurrentPage = () =>
            {
                var dataItemExists = ScriptQuery<bool>("return $k.dataSource.get(" + modelId + ") != null;");
                return dataItemExists;
            };

            Func<IWebElement> getTableRow = () =>
            {
                var dataItemUid = ScriptQuery<string>("return $k.dataSource.get(" + modelId + ").uid;");
                var row = ScriptQuery<IWebElement>("return $k.tbody.find(\"tr[data-uid='" + dataItemUid + "']\").get(0);");
                return row;
            };

            if (dataItemExistsOnCurrentPage())
            {
                return getTableRow();
            }

            IWebElement foundItem = null;

            // Item not found on current page, start searching from page 1
            DoPerPage(x =>
            {
                // Attempt to find item on this page
                if (!dataItemExistsOnCurrentPage()) return;

                x.Cancel = true;
                foundItem = getTableRow();
            });

            if (foundItem != null)
                return foundItem;

            return null;
        }

        public IWebElement GetTableRowByModelId(string id)
        {
            var modelId = string.Format("'{0}'", id);
            Func<bool> dataItemExistsOnCurrentPage = () =>
            {
                var dataItemExists = ScriptQuery<bool>("return $k.dataSource.get(" + modelId + ") != null;");
                return dataItemExists;
            };

            Func<IWebElement> getTableRow = () =>
            {
                var dataItemUid = ScriptQuery<string>("return $k.dataSource.get(" + modelId + ").uid;");
                var row = ScriptQuery<IWebElement>("return $k.tbody.find(\"tr[data-uid='" + dataItemUid + "']\").get(0);");
                return row;
            };

            if (dataItemExistsOnCurrentPage())
            {
                return getTableRow();
            }

            IWebElement foundItem = null;

            // Item not found on current page, start searching from page 1
            DoPerPage(x =>
            {
                // Attempt to find item on this page
                if (!dataItemExistsOnCurrentPage()) return;

                x.Cancel = true;
                foundItem = getTableRow();
            });

            if (foundItem != null)
                return foundItem;

            return null;
        }

        public void DoPerPage(Action<DoPerPageContext> doWork)
        {
            // Item not found on current page, start searching from page 1
            for (int page = 1; page <= TotalPage(); page++)
            {
                // Go to page 
                ScriptExecute("$k.dataSource.page(" + page + ")");

                ScriptExecute("isBrowserBusy() == false");

                var ctx = new DoPerPageContext { PageNumber = page, TotalItems = Total() };

                doWork(ctx);
                if (ctx.Cancel)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        public long Page
        {
            get
            {
                return
                    (long)this.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(CultureInfo.InvariantCulture, "return {0}.pager.page();", this.kendoGrid));
            }
        }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        public long TotalPages
        {
            get
            {
                return
                    (long)this.Driver.JavaScripts()
                        .ExecuteScript(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                "return {0}.pager.totalPages();",
                                this.kendoGrid));
            }
        }

        /// <summary>
        /// The set page.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        public void SetPage(int page)
        {
            this.Driver.JavaScripts()
                .ExecuteScript(
                    string.Format(CultureInfo.InvariantCulture, "{0}.pager.page({1});", this.kendoGrid, page));
            this.Driver.WaitForAjax(60);
        }

        /// <summary>
        /// The search row with text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        public IWebElement SearchRowWithText(string text)
        {
            var row = this.GetRowWithText(text);
            if (row != null)
            {
                return row;
            }

            for (var i = 1; i < this.TotalPages + 1; i++)
            {
                this.SetPage(i);
                row = this.GetRowWithText(text);
                if (row != null)
                {
                    return row;
                }
            }

            return null;
        }

        /// <summary>
        /// The search row with text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="timeoutInSeconds">
        /// The timeout in seconds.
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        /// <exception cref="NotFoundException">
        /// When row with text was not found in specific time
        /// </exception>
        public IWebElement SearchRowWithText(string text, double timeoutInSeconds)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(x => this.SearchRowWithText(text) != null);
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }

            return this.SearchRowWithText(text);
        }

        /// <summary>
        /// The get row with text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="IWebElement"/>.
        /// </returns>
        private IWebElement GetRowWithText(string text)
        {
            return
                (IWebElement)this.Driver.JavaScripts()
                    .ExecuteScript(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "return {0}.tbody.find('tr:contains(\"{1}\")').get(0);",
                            this.kendoGrid,
                            text));
        }

        public IWebElement SelectByGridColumn(string text, int columnIndex)
        {
            return
                 (IWebElement)this.Driver.JavaScripts()
                     .ExecuteScript(
                         string.Format(
                             CultureInfo.InvariantCulture,

                             "return {0}.tbody.find('td:nth-child({2}):not(:contains(\"{1}\"))').get(0);",
                             this.kendoGridForHref,
                             text,
                             columnIndex
                            ));

        }
        public IWebElement SelectByGridColumn(int columnIndex)
        {
            return
                 (IWebElement)this.Driver.JavaScripts()
                     .ExecuteScript(
                         string.Format(
                             CultureInfo.InvariantCulture,
                             //"return  $('#operatorsgrid').data('kendoGrid').tbody.find('td:nth(4)').get(0);"
                              "return {0}.tbody.find('td:nth({1})').get(0);",
                               this.kendoGridForHref,
                               columnIndex
                            ));

        }
    }

}
