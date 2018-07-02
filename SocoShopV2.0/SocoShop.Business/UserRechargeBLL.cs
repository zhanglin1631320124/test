namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class UserRechargeBLL
    {
        private static readonly IUserRecharge dal = FactoryHelper.Instance<IUserRecharge>(Global.DataProvider, "UserRechargeDAL");

        public static int AddUserRecharge(UserRechargeInfo userRecharge)
        {
            userRecharge.ID = dal.AddUserRecharge(userRecharge);
            return userRecharge.ID;
        }

        public static void DeleteUserRecharge(string strID, int userID)
        {
            if (userID != 0) strID = dal.ReadUserRechargeIDList(strID, userID);
            dal.DeleteUserRecharge(strID, userID);
        }

        public static UserRechargeInfo ReadUserRecharge(int id, int userID)
        {
            return dal.ReadUserRecharge(id, userID);
        }

        public static UserRechargeInfo ReadUserRechargeByNumber(string number, int userID)
        {
            return dal.ReadUserRechargeByNumber(number, userID);
        }

        public static List<UserRechargeInfo> SearchUserRechargeList(UserRechargeSearchInfo userRecharge)
        {
            return dal.SearchUserRechargeList(userRecharge);
        }

        public static List<UserRechargeInfo> SearchUserRechargeList(int currentPage, int pageSize, UserRechargeSearchInfo userRecharge, ref int count)
        {
            return dal.SearchUserRechargeList(currentPage, pageSize, userRecharge, ref count);
        }

        public static void UpdateUserRecharge(UserRechargeInfo userRecharge)
        {
            dal.UpdateUserRecharge(userRecharge);
        }
    }
}

