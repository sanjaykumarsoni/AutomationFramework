using System;
using System.Collections.Generic;
using System.Linq;
using LinqToExcel;
using System.IO;


namespace TradeProTests.TestDataAccess
{
    public class ManufactureSettings
    {
        public bool Setting_Value { get; set; }
        public string Setting_CD { get; set; }
        public int Manufacturer_ID { get; set; }

    }
    public class UserSettings
    {
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public int GroupLevel { get; set; }
        public int ParentEntityID { get; set; }
        public int EntityID { get; set; }
        public int SecurityID { get; set; }
        public string EntityCD { get; set; }
        public string SecurityCD { get; set; }
    }
    public class UserInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Manufacturer_ID { get; set; }
        public string LastLoginDate { get; set; }
        public int LoginMigrated_IND { get; set; }
        public int Confirmed_IND { get; set; }
        public string PasswordExpDate { get; set; }
        public int LoginFailed_IND { get; set; }
        public int LockedOut_IND { get; set; }
        public int LockedOutMins { get; set; }
        public string PasswordExpNotifyDate { get; set; }
        public string GUID { get; set; }
    }

    public class UserMfgMetaDataViewModel
    {
        public int UserID { get; set; }
        public List<string> SecurityCD { get; set; }
        public int Manufacturer_ID { get; set; }
    }

    public class ExcelMetadataRepository
    {
        public static UserMfgMetaDataViewModel UserBasedRolesMetaData(string userEmail)
        {
            Console.WriteLine("UserBasedRolesMetaData has been fetched");
            var excelUserEmail = userEmail; //ExcelHelper.ReadData(2, "UserName");
            var excel = new ExcelQueryFactory(Path.GetFullPath(Directory.GetCurrentDirectory() + FilePath.ExcelFileMetaDataLocation));
            string fileName = excel.FileName;
           
            //Since Join() does not support with LinqToExcel so type cast.
            var manufactureSettings = (List<ManufactureSettings>)excel.Worksheet<ManufactureSettings>("Manufacturer_Settings").ToList();
            var userInfo = (List<UserInfo>)excel.Worksheet<UserInfo>("User_Info").Where(x => x.Email.Equals(excelUserEmail)).ToList();
            var userSettings = (List<UserSettings>)excel.Worksheet<UserSettings>("User_Settings").ToList();
           
            var userMfgMetaData = (from user in userInfo
                                   join mfg in manufactureSettings on user.Manufacturer_ID equals mfg.Manufacturer_ID 
                                   join usrSet in userSettings on user.UserID equals usrSet.UserID //into mfgUserSettings
                                   //from x in mfgUserSettings.DefaultIfEmpty()
                                   select new UserMfgMetaDataViewModel
                                   {
                                       UserID = user.UserID,
                                       Manufacturer_ID = mfg.Manufacturer_ID,
                                       SecurityCD = userSettings.Where(x => x.UserID == user.UserID).Select(y => y.SecurityCD).ToList()
                                   }).Where(x => x.Manufacturer_ID == userInfo.FirstOrDefault().Manufacturer_ID).FirstOrDefault();

            return userMfgMetaData;
        }

    }
}
