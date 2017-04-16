using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeProTests.TestDataAccess
{
    public class FilePath
    {
        private static string _DataDrivenExcelFileLocation = string.Empty;
        private static string _ExcelFileMetaDataLocation = string.Empty;
        private static string _TestCaseExcel = string.Empty;
        public static string DataDrivenExcelFileLocation
        {
            get
            {
                try
                {
                    string str = ConfigurationManager.AppSettings["DataDrivenExcelFilePath"].ToString();
                    _DataDrivenExcelFileLocation = str;
                    return _DataDrivenExcelFileLocation;
                }
                catch (Exception)
                {
                    return _DataDrivenExcelFileLocation;
                }
            }
        }

        public static string ExcelFileMetaDataLocation
        {
            get
            {
                try
                {
                    string str = ConfigurationManager.AppSettings["ExcelFileMetaDataLocation"].ToString();
                    _ExcelFileMetaDataLocation = str;
                    return _ExcelFileMetaDataLocation;
                }
                catch (Exception)
                {
                    return _ExcelFileMetaDataLocation;
                }
            }
        }
        public static string TestCaseExcel
        {
            get
            {
                try
                {
                    string str = ConfigurationManager.AppSettings["TestCaseExcel"].ToString();
                    _TestCaseExcel = str;
                    return _TestCaseExcel;
                }
                catch (Exception)
                {
                    return _TestCaseExcel;
                }
            }
        }
    }
}
