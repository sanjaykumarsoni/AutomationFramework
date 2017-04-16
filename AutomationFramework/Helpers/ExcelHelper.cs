using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Excel;
using System.Text.RegularExpressions;

namespace AutomationFramework.Helpers
{
    public static class ExcelHelper
    {
        public static List<Datacollection> dataCol = new List<Datacollection>();
        private static readonly IDictionary<string, string> DataDictionary = new Dictionary<string, string>();
        public static DataTable ExcelToDataTable(string fileName)
        {
            //open file and returns as Stream
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx
            //Set the First Row as Column Name
            excelReader.IsFirstRowAsColumnNames = true;
            //Return as DataSet
            DataSet result = excelReader.AsDataSet();
            //Get all the Tables
            DataTableCollection table = result.Tables;
            //Store it in DataTable
            DataTable resultTable = table["DataSet"];

            //return
            return resultTable;
        }

        //Populating Data into Collections
        public static List<Datacollection> PopulateInCollection(string fileName)
        {
            if (dataCol.Count == 0)
            {
                DataTable table = ExcelToDataTable(fileName);
                //Iterate through the rows and columns of the Table
                for (int row = 1; row <= table.Rows.Count; row++)
                {
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        Datacollection dtTable = new Datacollection()
                        {
                            RowNumber = row,
                            ColName = table.Columns[col].ColumnName,
                            ColValue = table.Rows[row - 1][col].ToString()
                        };
                        //Add all the details for each row
                        dataCol.Add(dtTable);
                    }
                }
            }
            return dataCol;
        }

        //Reading data from Collection based on row number and column name
        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in dataCol
                               where colData.ColName == columnName && colData.RowNumber == rowNumber
                               select colData.ColValue).SingleOrDefault();

                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                return data.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string ReadDataFromKey(string key)
        {
            DataTable table = new DataTable();
            var myColumn = table.Columns.Cast<DataColumn>().SingleOrDefault(col => col.ColumnName == key);
            if (myColumn != null)
            {
                // just some roww
                var tableRow = table.AsEnumerable().First();
                var myData = tableRow.Field<string>(myColumn);
                // or if above does not work
                return myData = tableRow.Field<string>(table.Columns.IndexOf(myColumn));
            }
            else
            {
                return null;
            }
            //try
            //{
            //    //Retriving Data using LINQ to reduce much of iterations
            //    string data = (from colData in dataCol
            //                   where colData.Key.Contains(key)
            //                   select colData.ColValue).SingleOrDefault();

            //    //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
            //    return data.ToString();
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
        }
        public static string[] Split(string value)
        {
            string[] items = new string[] { };
            if (!string.IsNullOrEmpty(value))
            {
                var regex = new Regex("(?:^|,)(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^,]*)");
                var collection = regex.Matches(value);
                items = new string[collection.Count];
                var i = 0;
                foreach (Match match in collection)
                {
                    items[i++] = match.Groups[0].Value.Trim('"').Trim(',').Trim('"').Trim();
                }
            }
            return items;
        }
    }

    public class Datacollection
    {
        public int RowNumber { get; set; }
        public string ColName { get; set; }
        public string ColValue { get; set; }
    }
    public class KeyData
    {
        public string Key { get; set; }
        public string KeyValue { get; set; }
    }

}
