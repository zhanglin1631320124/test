namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class BookingProduct : AdminBasePage
    {

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteBookingProduct", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                BookingProductBLL.DeleteBookingProduct(intsForm, 0);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("BookingProduct"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadBookingProduct", PowerCheckType.Single);
                this.RelationUser.Text = RequestHelper.GetQueryString<string>("RelationUser");
                this.ProductName.Text = RequestHelper.GetQueryString<string>("ProductName");
                this.IsHandler.Text = RequestHelper.GetQueryString<string>("IsHandler");
                List<BookingProductInfo> dataSource = new List<BookingProductInfo>();
                BookingProductSearchInfo bookingProduct = new BookingProductSearchInfo();
                bookingProduct.RelationUser = RequestHelper.GetQueryString<string>("RelationUser");
                bookingProduct.ProductName = RequestHelper.GetQueryString<string>("ProductName");
                bookingProduct.IsHandler = RequestHelper.GetQueryString<int>("IsHandler");
                dataSource = BookingProductBLL.SearchBookingProductList(base.CurrentPage, base.PageSize, bookingProduct, ref this.Count);
                base.BindControl(dataSource, this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((("BookingProduct.aspx?Action=search&" + "RelationUser=" + this.RelationUser.Text + "&") + "ProductName=" + this.ProductName.Text + "&") + "IsHandler=" + this.IsHandler.Text);
        }
    }
}

