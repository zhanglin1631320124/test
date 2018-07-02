namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;

    public sealed class UserAccountRecordBLL
    {
        private static readonly IUserAccountRecord dal = FactoryHelper.Instance<IUserAccountRecord>(Global.DataProvider, "UserAccountRecordDAL");

        private static int AddUserAccountRecord(UserAccountRecordInfo userAccountRecord)
        {
            userAccountRecord.ID = dal.AddUserAccountRecord(userAccountRecord);
            return userAccountRecord.ID;
        }

        public static int AddUserAccountRecord(decimal money, int point, string note, int userID)
        {
            string userName = UserBLL.ReadUser(userID).UserName;
            return AddUserAccountRecord(money, point, note, userID, userName);
        }

        public static int AddUserAccountRecord(decimal money, int point, string note, int userID, string userName)
        {
            UserAccountRecordInfo userAccountRecord = new UserAccountRecordInfo();
            userAccountRecord.Money = money;
            userAccountRecord.Point = point;
            userAccountRecord.Date = RequestHelper.DateNow;
            userAccountRecord.IP = ClientHelper.IP;
            userAccountRecord.Note = note;
            userAccountRecord.UserID = userID;
            userAccountRecord.UserName = userName;
            userAccountRecord.ID = AddUserAccountRecord(userAccountRecord);
            return userAccountRecord.ID;
        }

        public static void DeleteUserAccountRecord(string strID, int userID)
        {
            dal.DeleteUserAccountRecord(strID, userID);
        }

        public static void DeleteUserAccountRecordByUserID(string strUserID)
        {
            dal.DeleteUserAccountRecordByUserID(strUserID);
        }

        public static decimal ReadMoneyLeftBeforID(int id, int userID)
        {
            return dal.ReadMoneyLeftBeforID(id, userID);
        }

        public static int ReadPointLeftBeforID(int id, int userID)
        {
            return dal.ReadPointLeftBeforID(id, userID);
        }

        public static UserAccountRecordInfo ReadUserAccountRecord(int id, int userID)
        {
            return dal.ReadUserAccountRecord(id, userID);
        }

        public static List<UserAccountRecordInfo> ReadUserAccountRecordList(int userID)
        {
            return dal.ReadUserAccountRecordList(userID);
        }

        public static List<UserAccountRecordInfo> ReadUserAccountRecordList(int currentPage, int pageSize, ref int count, int userID, int accountType)
        {
            return dal.ReadUserAccountRecordList(currentPage, pageSize, ref count, userID, accountType);
        }
    }
}

