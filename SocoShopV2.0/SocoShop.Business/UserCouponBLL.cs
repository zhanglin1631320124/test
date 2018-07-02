namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;
    using SkyCES.EntLib;

    public sealed class UserCouponBLL
    {
        private static readonly IUserCoupon dal = FactoryHelper.Instance<IUserCoupon>(Global.DataProvider, "UserCouponDAL");

        public static int AddUserCoupon(UserCouponInfo userCoupon)
        {
            userCoupon.ID = dal.AddUserCoupon(userCoupon);
            return userCoupon.ID;
        }

        public static void DeleteUserCoupon(string strID, int userID)
        {
            dal.DeleteUserCoupon(strID, userID);
        }

        public static void DeleteUserCouponByCouponID(string strCouponID)
        {
            dal.DeleteUserCouponByCouponID(strCouponID);
        }

        public static UserCouponInfo ReadTopUserCoupon(int couponID)
        {
            return dal.ReadTopUserCoupon(couponID);
        }

        public static UserCouponInfo ReadUserCoupon(int id, int userID)
        {
            return dal.ReadUserCoupon(id, userID);
        }

        public static UserCouponInfo ReadUserCouponByNumber(string number, string password)
        {
            return dal.ReadUserCouponByNumber(number, password);
        }

        public static UserCouponInfo ReadUserCouponByOrder(int orderID)
        {
            return dal.ReadUserCouponByOrder(orderID);
        }

        public static List<UserCouponInfo> ReadUserCouponCanUse(int userID)
        {
            return dal.ReadUserCouponCanUse(userID);
        }

        public static List<UserCouponInfo> SearchUserCouponList(UserCouponSearchInfo userCoupon)
        {
            return dal.SearchUserCouponList(userCoupon);
        }

        public static List<UserCouponInfo> SearchUserCouponList(int currentPage, int pageSize, UserCouponSearchInfo userCoupon, ref int count)
        {
            return dal.SearchUserCouponList(currentPage, pageSize, userCoupon, ref count);
        }

        public static void UpdateUserCoupon(UserCouponInfo userCoupon)
        {
            dal.UpdateUserCoupon(userCoupon);
        }
    }
}

