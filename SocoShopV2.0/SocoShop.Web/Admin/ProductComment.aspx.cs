namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ProductComment : AdminBasePage
    {

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteProductComment", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ProductCommentBLL.DeleteProductComment(intsForm, 0);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("ProductComment"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void NoHandlerButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("UpdateProductComment", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ProductCommentBLL.ChangeProductCommentStatus(intsForm, 1);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("OperateOK"), RequestHelper.RawUrl);
            }
        }

        protected void NoShowButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("UpdateProductComment", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ProductCommentBLL.ChangeProductCommentStatus(intsForm, 3);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("OperateOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadProductComment", PowerCheckType.Single);
                this.Name.Text = RequestHelper.GetQueryString<string>("Name");
                this.txtTitle.Text = RequestHelper.GetQueryString<string>("Title");
                this.StartPostDate.Text = RequestHelper.GetQueryString<string>("StartPostDate");
                this.EndPostDate.Text = RequestHelper.GetQueryString<string>("EndPostDate");
                this.Status.Text = RequestHelper.GetQueryString<string>("Status");
                ProductCommentSearchInfo productComment = new ProductCommentSearchInfo();
                productComment.ProductName = RequestHelper.GetQueryString<string>("Name");
                productComment.Title = RequestHelper.GetQueryString<string>("Title");
                productComment.Content = RequestHelper.GetQueryString<string>("Content");
                productComment.UserIP = RequestHelper.GetQueryString<string>("UserIP");
                productComment.StartPostDate = RequestHelper.GetQueryString<DateTime>("StartPostDate");
                productComment.EndPostDate = RequestHelper.GetQueryString<DateTime>("EndPostDate");
                productComment.Status = RequestHelper.GetQueryString<int>("Status");
                base.BindControl(ProductCommentBLL.SearchProductCommentInnerList(base.CurrentPage, base.PageSize, productComment, ref this.Count), this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((((("ProductComment.aspx?Action=search&" + "Name=" + this.Name.Text + "&") + "Title=" + this.txtTitle.Text + "&") + "StartPostDate=" + this.StartPostDate.Text + "&") + "EndPostDate=" + this.EndPostDate.Text + "&") + "Status=" + this.Status.Text);
        }

        protected void ShowButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("UpdateProductComment", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                ProductCommentBLL.ChangeProductCommentStatus(intsForm, 2);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("OperateOK"), RequestHelper.RawUrl);
            }
        }
    }
}

