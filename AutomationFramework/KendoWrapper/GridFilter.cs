using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.KendoWrapper
{
    /// <summary>
    /// Grid filter
    /// </summary>
    public class GridFilter
    {
        public GridFilter(string columnName, FilterOperator filterOperator, string filterValue)
        {
            this.ColumnName = columnName;
            this.FilterOperator = filterOperator;
            this.FilterValue = filterValue;
        }

        public string ColumnName { get; set; }

        public FilterOperator FilterOperator { get; set; }

        public string FilterValue { get; set; }
    }
}
