using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using LinqToExcel;
using System.IO;
using AutomationFramework.Helpers;
using NUnit.Framework.Interfaces;
using OfficeOpenXml;

namespace AutomationFramework.TestDataDriven
{
    /// <summary>
    /// Hold the insert/update /get data operations from/to excel sheet. Need to refatored to reuse this class for any excel sheets.
    /// </summary>
    public class TestDataFactory
    {
        [ThreadStatic]
        public static IEnumerable<TestExcelDataVM> TestExcelData = null;
        public static string methodName = string.Empty;
        static List<ITestCaseData> list = new List<ITestCaseData>();
        public static string filepath = string.Empty;

        /// <summary>
        /// TestUploadData worksheet properties
        /// </summary>
        public class TestUploadData
        {
            public int SNo { get; set; }
            public string Automation_TestcaseID { get; set; }
            public string Test_Location { get; set; }
            public string SubTest_Location { get; set; }
            public string Test { get; set; }
            public string Expected_Result { get; set; }
            public string Automation_Status { get; set; }
            public string Test_Case_Type { get; set; }
            public string Test_data1 { get; set; }
            public string Test_data2 { get; set; }
            public string Test_data3 { get; set; }

        }

        /// <summary>
        /// TestUploadData worksheet model properties that required to fetch data.
        /// </summary>
        public class TestExcelDataVM
        {
            public string TestCaseID { get; set; }
            public string TestData1 { get; set; }
            public string TestData2 { get; set; }
            public string TestData3 { get; set; }
        }

        //Method for fetching Raw data. this method should be refactored. since it has been hard coded with worksheet name. 
        public static void GetExcelData()
        {
            var excel = new ExcelQueryFactory(Directory.GetCurrentDirectory() + filepath);
            var Testuploaddata = (List<TestUploadData>)excel.Worksheet<TestUploadData>("TestUploadData").ToList();
            TestExcelData = (from TestData in Testuploaddata
                             where (TestData.Automation_TestcaseID != null)
                             select new TestExcelDataVM { TestData1 = TestData.Test_data1, TestData2 = TestData.Test_data2, TestData3 = TestData.Test_data3, TestCaseID = TestData.Automation_TestcaseID });

        }

        /// <summary>
        /// Read test cases based on the define Excel test data column. 
        /// it is tightly coupled with Test construction excel sheets. 
        /// Any column changes in excel test data will directly impact in output.
        /// Could be more refactored to achive decoupling.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string[]> ReadTestCases()
        {
            List<string> passdata = new List<string>();
            string[] splitmethod = methodName.Split('_');
            var Testdata = (from t in TestExcelData
                            where (t.TestCaseID == splitmethod[0])
                            select new TestExcelDataVM { TestData1 = t.TestData1, TestData2 = t.TestData2, TestData3 = t.TestData3 }).ToList();
            if (!string.IsNullOrEmpty(Testdata[0].TestData1))
            {
                passdata.Add(Testdata[0].TestData1);
            }
            if (!string.IsNullOrEmpty(Testdata[0].TestData2))
            {
                passdata.Add(Testdata[0].TestData2);
            }
            if (!string.IsNullOrEmpty(Testdata[0].TestData3))
            {
                passdata.Add(Testdata[0].TestData3);
            }

            for (int i = 0; i < passdata.Count(); i++)
            {
                string[] test = ExcelHelper.Split(passdata[i].ToString());
                yield return test;
            }
        }

        /// <summary>
        /// To update the excel info for each log. Should be more generic and need to look back.
        /// </summary>
        /// <param name="id">test id</param>
        /// <param name="logStatus">test status</param>
        /// <param name="logDetails">log if its failed </param>
        /// <returns></returns>
        public static ExcelModel ExcelReport(string id, string logStatus, string logDetails)
        {
            ExcelModel excelModel = new ExcelModel();
            excelModel.TestId = id;
            excelModel.Status = logStatus;
            excelModel.LogDetails = logDetails;
            return excelModel;
        }

        /// <summary>
        /// To update the excel column. Need to refactor into more genric. Excel Path, Worksheet name, and required Cells column  should input as a parameters.
        /// </summary>
        /// <param name="excelReport">excel report model to be updated in excel.</param>
        public static void UpdateTestCaseExcel(List<ExcelModel> excelReport)
        {

            FileInfo fi = new FileInfo(@"C:\Development\NitikaGoyal\TFS_Tradepro7.6\TradePro 7.6 UI\TradeProTests\TestDataAccess\TPMFS_TestCaseReconstruction.xlsx");
            using (ExcelPackage package = new ExcelPackage(fi))
            {
                var sheet = package.Workbook.Worksheets["TestUploadData"];
                var objs = from cell in sheet.Cells["l:m"]
                           select
                           new ExcelModel
                           {
                               TestId = sheet.Cells[cell.Start.Row, 2].Value != null ? sheet.Cells[cell.Start.Row, 2].Value.ToString() : "",
                               Status = sheet.Cells[cell.Start.Row, 12].Value != null ? sheet.Cells[cell.Start.Row, 12].Value.ToString() : "",
                               LogDetails = sheet.Cells[cell.Start.Row, 13].Value != null ? sheet.Cells[cell.Start.Row, 13].Value.ToString() : "",
                               RowIndex = cell.Start.Row,
                           };
                foreach (var item in excelReport)
                {
                    var objV = objs.Where(o => o.TestId == item.TestId).FirstOrDefault();
                    if (objV != null)
                    {
                        objV.Status = item.Status;
                        objV.LogDetails = item.LogDetails.ToString();
                        sheet.Cells[objV.RowIndex, 12].Value = objV.Status;
                        sheet.Cells[objV.RowIndex, 13].Value = objV.LogDetails;
                    }
                }
                package.SaveAs(fi);
            }
        }
    }
    public class ExcelModel
    {
        public string TestId { get; set; }
        public string Status { get; set; }
        public string LogDetails { get; set; }
        public int RowIndex { get; set; }
    }
}

