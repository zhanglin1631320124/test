namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class CouponBLL
    {
        private static readonly ICoupon dal = FactoryHelper.Instance<ICoupon>(Global.DataProvider, "CouponDAL");

        public static int AddCoupon(CouponInfo coupon)
        {
            coupon.ID = dal.AddCoupon(coupon);
            return coupon.ID;
        }

        public static void DeleteCoupon(string strID)
        {
            UserCouponBLL.DeleteUserCouponByCouponID(strID);
            dal.DeleteCoupon(strID);
        }

        public static CouponInfo ReadCoupon(int id)
        {
            return dal.ReadCoupon(id);
        }

        public static CouponInfo ReadCouponByCouponList(List<CouponInfo> couponList, int couponID)
        {
            CouponInfo info = new CouponInfo();
            foreach (CouponInfo info2 in couponList)
            {
                if (info2.ID == couponID) info = info2;
            }
            return info;
        }

        public static string ReadCouponType(int couponType)
        {
            string str = string.Empty;
            switch (couponType)
            {
                case 1:
                    return "在线发放";

                case 2:
                    return "线下发放";
            }
            return str;
        }

        public static List<CouponInfo> SearchCouponList(CouponSearchInfo coupon)
        {
            return dal.SearchCouponList(coupon);
        }

        public static List<CouponInfo> SearchCouponList(int currentPage, int pageSize, CouponSearchInfo coupon, ref int count)
        {
            return dal.SearchCouponList(currentPage, pageSize, coupon, ref count);
        }

        public static void UpdateCoupon(CouponInfo coupon)
        {
            dal.UpdateCoupon(coupon);
        }
    }
}

