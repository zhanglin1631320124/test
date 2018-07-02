namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface ICoupon
    {
        int AddCoupon(CouponInfo coupon);
        void DeleteCoupon(string strID);
        CouponInfo ReadCoupon(int id);
        List<CouponInfo> SearchCouponList(CouponSearchInfo coupon);
        List<CouponInfo> SearchCouponList(int currentPage, int pageSize, CouponSearchInfo coupon, ref int count);
        void UpdateCoupon(CouponInfo coupon);
    }
}

