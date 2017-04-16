using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Reflection;

namespace TradeProTests.TestDataAccess
{
    public class ExcelDataAccess
    {
        public static string TestDataFileConnection()
        {
            var fileName = ConfigurationManager.AppSettings["TestDataSheetPath"];//@"C:\Development\TestData.xlsx";
            //var fileName1 = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings["TestDataSheetPath"];
            //var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;", fileName);
            var con = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=fileName;Extended Properties=Excel 12.0 Xml;HDR=YES;");
            return con;

        }

        //public static OleDbConnection ConnString()
        //{
        //    OleDbConnection connString = new OleDbConnection();
        //    connString.ConnectionString = TestDataFileConnection();
        //    return connString;
        //}
 

        public static UserData GetTestData(string keyName)
        {
            //OleDbConnection connString = ConnString();
            //connString.Open();
            using (var connection = new OleDbConnection(TestDataFileConnection()))
            {
                //connString.Open();
                var query = string.Format("select * from [DataSet$] where key='{0}'", keyName);
                var value = connection.Query<UserData>(query).FirstOrDefault();
                connection.Close();
                return value;
            }
        }
    }
}
