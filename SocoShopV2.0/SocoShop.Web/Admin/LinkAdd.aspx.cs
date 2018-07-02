namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class LinkAdd : AdminBasePage
    {
        protected int classID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.classID = RequestHelper.GetQueryString<int>("ClassID");
                this.LinkClass.Text = this.classID.ToString();
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadLink", PowerCheckType.Single);
                    LinkInfo info = LinkBLL.ReadLinkCache(queryString);
                    if (info.LinkClass == 1)
                        this.TextDisplay.Text = info.Display;
                    else
                        this.PictureDisplay.Text = info.Display;
                    this.URL.Text = info.URL;
                    this.Remark.Text = info.Remark;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            LinkInfo link = new LinkInfo();
            link.ID = RequestHelper.GetQueryString<int>("ID");
            link.LinkClass = Convert.ToInt32(this.LinkClass.Text);
            if (link.LinkClass == 1)
                link.Display = this.TextDisplay.Text;
            else
                link.Display = this.PictureDisplay.Text;
            link.URL = this.URL.Text;
            link.Remark = this.Remark.Text;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (link.ID == -2147483648)
            {
                base.CheckAdminPower("AddLink", PowerCheckType.Single);
                int id = LinkBLL.AddLink(link);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Link"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateLink", PowerCheckType.Single);
                LinkBLL.UpdateLink(link);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Link"), link.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

