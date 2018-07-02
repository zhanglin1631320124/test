namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Tags : AdminBasePage
    {

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteTags", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                TagsBLL.DeleteTags(intsForm, 0);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("Tags"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadTags", PowerCheckType.Single);
                TagsSearchInfo tags = new TagsSearchInfo();
                tags.ProductName = RequestHelper.GetQueryString<string>("ProductName");
                tags.Word = RequestHelper.GetQueryString<string>("Word");
                tags.IsTop = RequestHelper.GetQueryString<int>("IsTop");
                this.ProductName.Text = tags.ProductName;
                this.Word.Text = tags.Word;
                this.IsTop.Text = tags.IsTop.ToString();
                base.BindControl(TagsBLL.SearchTagsList(base.CurrentPage, base.PageSize, tags, ref this.Count), this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((("Tags.aspx?Action=search&" + "ProductName" + this.ProductName.Text + "&") + "Word=" + this.Word.Text + "&") + "IsTop=" + this.IsTop.Text + "&");
        }
    }
}

