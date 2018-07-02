namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;

    public class BookingProductAdd : CommonBasePage
    {
        protected int productID = 0;
        protected string productName = string.Empty;
        protected UserInfo user = new UserInfo();

        protected override void PageLoad()
        {
            base.PageLoad();
            this.productName = RequestHelper.GetQueryString<string>("ProductName");
            this.productID = RequestHelper.GetQueryString<int>("ProductID");
            this.user = UserBLL.ReadUser(base.UserID);
            base.Title = this.productName + "缺货登记";
        }

        protected override void PostBack()
        {
            BookingProductInfo bookingProduct = new BookingProductInfo();
            bookingProduct.ProductID = RequestHelper.GetForm<int>("ProductID");
            bookingProduct.ProductName = StringHelper.AddSafe(RequestHelper.GetForm<string>("ProductName"));
            bookingProduct.RelationUser = StringHelper.AddSafe(RequestHelper.GetForm<string>("RelationUser"));
            bookingProduct.Email = StringHelper.AddSafe(RequestHelper.GetForm<string>("Email"));
            bookingProduct.Tel = StringHelper.AddSafe(RequestHelper.GetForm<string>("Tel"));
            bookingProduct.UserNote = StringHelper.AddSafe(RequestHelper.GetForm<string>("UserNote"));
            bookingProduct.BookingDate = RequestHelper.DateNow;
            bookingProduct.BookingIP = ClientHelper.IP;
            bookingProduct.IsHandler = 0;
            bookingProduct.HandlerDate = RequestHelper.DateNow;
            bookingProduct.HandlerAdminID = 0;
            bookingProduct.HandlerAdminName = string.Empty;
            bookingProduct.HandlerNote = string.Empty;
            bookingProduct.UserID = base.UserID;
            bookingProduct.UserName = base.UserName;
            int num = BookingProductBLL.AddBookingProduct(bookingProduct);
            ScriptHelper.Alert("登记成功", "/ProductDetail.aspx?ID=" + bookingProduct.ProductID.ToString());
        }
    }
}

