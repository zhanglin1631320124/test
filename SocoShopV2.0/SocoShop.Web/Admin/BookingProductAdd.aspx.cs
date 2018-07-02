namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class BookingProductAdd : AdminBasePage
    {
        protected BookingProductInfo bookingProduct = new BookingProductInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadBookingProduct", PowerCheckType.Single);
                    this.bookingProduct = BookingProductBLL.ReadBookingProduct(queryString, 0);
                    this.HandlerNote.Text = this.bookingProduct.HandlerNote;
                    this.IsHandler.Text = this.bookingProduct.IsHandler.ToString();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            BookingProductInfo bookingProduct = new BookingProductInfo();
            bookingProduct.ID = RequestHelper.GetQueryString<int>("ID");
            bookingProduct.IsHandler = Convert.ToInt32(this.IsHandler.Text);
            bookingProduct.HandlerDate = RequestHelper.DateNow;
            bookingProduct.HandlerAdminID = Cookies.Admin.GetAdminID(false);
            bookingProduct.HandlerAdminName = Cookies.Admin.GetAdminName(false);
            bookingProduct.HandlerNote = this.HandlerNote.Text;
            string alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            base.CheckAdminPower("UpdateBookingProduct", PowerCheckType.Single);
            BookingProductBLL.UpdateBookingProduct(bookingProduct);
            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("BookingProduct"), bookingProduct.ID);
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

