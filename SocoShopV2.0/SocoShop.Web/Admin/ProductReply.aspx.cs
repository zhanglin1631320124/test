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

    public partial class ProductReply : AdminBasePage
    {

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteProductReply", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ProductReplyBLL.DeleteProductReply(intsForm, 0);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("ProductReply"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadProductReply", PowerCheckType.Single);
                int queryString = RequestHelper.GetQueryString<int>("CommentID");
                base.PageSize = 8;
                List<ProductReplyInfo> dataSource = ProductReplyBLL.ReadProductReplyList(queryString, base.CurrentPage, base.PageSize, ref this.Count, -2147483648);
                base.BindControl(dataSource, this.RecordList, this.MyPager);
            }
        }
    }
}

