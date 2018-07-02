namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ProductCommentAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadProductComment", PowerCheckType.Single);
                    ProductCommentInfo info = ProductCommentBLL.ReadProductComment(queryString, 0);
                    this.Name.Text = ProductBLL.ReadProduct(info.ProductID).Name;
                    this.txtTitle.Text = info.Title;
                    this.Content.Text = info.Content;
                    this.UserIP.Text = info.UserIP;
                    this.PostDate.Text = info.PostDate.ToString();
                    this.Support.Text = info.Support.ToString();
                    this.Against.Text = info.Against.ToString();
                    this.Status.Text = info.Status.ToString();
                    this.Rank.Text = info.Rank.ToString();
                    this.ReplyCount.Text = info.ReplyCount.ToString();
                    this.AdminReplyContent.Text = info.AdminReplyContent;
                    this.UserName.Text = info.UserName;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ProductCommentInfo productComment = new ProductCommentInfo();
            productComment.ID = RequestHelper.GetQueryString<int>("ID");
            productComment.Status = Convert.ToInt32(this.Status.Text);
            productComment.AdminReplyContent = this.AdminReplyContent.Text;
            productComment.AdminReplyDate = RequestHelper.DateNow;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (productComment.ID > 0)
            {
                base.CheckAdminPower("UpdateProductComment", PowerCheckType.Single);
                ProductCommentBLL.UpdateProductComment(productComment);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("ProductComment"), productComment.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

