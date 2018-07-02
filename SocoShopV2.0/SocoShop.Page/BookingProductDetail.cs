namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;

    public class BookingProductDetail : UserBasePage
    {
        protected BookingProductInfo bookingProduct = new BookingProductInfo();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.bookingProduct = BookingProductBLL.ReadBookingProduct(queryString, base.UserID);
        }
    }
}

