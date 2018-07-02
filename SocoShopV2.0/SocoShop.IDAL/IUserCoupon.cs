namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IUserCoupon
    {
        int AddUserCoupon(UserCouponInfo userCoupon);
        void DeleteUserCoupon(string strID, int userID);
        void DeleteUserCouponByCouponID(string strCouponID);
        UserCouponInfo ReadTopUserCoupon(int couponID);
        UserCouponInfo ReadUserCoupon(int id, int userID);
        UserCouponInfo ReadUserCouponByNumber(string number, string password);
        UserCouponInfo ReadUserCouponByOrder(int orderID);
        List<UserCouponInfo> ReadUserCouponCanUse(int userID);
        List<UserCouponInfo> SearchUserCouponList(UserCouponSearchInfo userCoupon);
        List<UserCouponInfo> SearchUserCouponList(int currentPage, int pageSize, UserCouponSearchInfo userCoupon, ref int count);
        void UpdateUserCoupon(UserCouponInfo userCoupon);
    }
}

