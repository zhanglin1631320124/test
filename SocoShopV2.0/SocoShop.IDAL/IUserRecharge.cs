namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IUserRecharge
    {
        int AddUserRecharge(UserRechargeInfo userRecharge);
        void DeleteUserRecharge(string strID, int userID);
        UserRechargeInfo ReadUserRecharge(int id, int userID);
        UserRechargeInfo ReadUserRechargeByNumber(string number, int userID);
        string ReadUserRechargeIDList(string strID, int userID);
        List<UserRechargeInfo> SearchUserRechargeList(UserRechargeSearchInfo userRecharge);
        List<UserRechargeInfo> SearchUserRechargeList(int currentPage, int pageSize, UserRechargeSearchInfo userRecharge, ref int count);
        void UpdateUserRecharge(UserRechargeInfo userRecharge);
    }
}

