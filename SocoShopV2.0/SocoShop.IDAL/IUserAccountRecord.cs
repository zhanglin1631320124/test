namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IUserAccountRecord
    {
        int AddUserAccountRecord(UserAccountRecordInfo userAccountRecord);
        void DeleteUserAccountRecord(string strID, int userID);
        void DeleteUserAccountRecordByUserID(string strUserID);
        decimal ReadMoneyLeftBeforID(int id, int userID);
        int ReadPointLeftBeforID(int id, int userID);
        UserAccountRecordInfo ReadUserAccountRecord(int id, int userID);
        List<UserAccountRecordInfo> ReadUserAccountRecordList(int userID);
        List<UserAccountRecordInfo> ReadUserAccountRecordList(int currentPage, int pageSize, ref int count, int userID, int accountType);
    }
}

