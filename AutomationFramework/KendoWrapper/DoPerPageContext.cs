using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.KendoWrapper
{
    /// <summary>
    /// Action on total number of pages in grids.
    /// </summary>
    public class DoPerPageContext
    {
        public DoPerPageContext()
        {
            PageNumber = 0;
            Cancel = false;
            TotalItems = 0;
        }
        public int PageNumber { get; set; }
        public bool Cancel { get; set; }

        public int TotalItems { get; set; }
    }
}
