namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class URLRewriterAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadURLRewriter", PowerCheckType.Single);
                    URLInfo info = URLClass.ReadURL(queryString);
                    this.RealPath.Text = info.RealPath;
                    this.VitualPath.Text = info.VitualPath;
                    if (info.IsEffect)
                    {
                        int num2 = 1;
                        this.IsEffect.Text = num2.ToString();
                    }
                    else
                        this.IsEffect.Text = 0.ToString();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            URLInfo url = new URLInfo();
            url.ID = RequestHelper.GetQueryString<int>("ID");
            url.RealPath = this.RealPath.Text;
            url.VitualPath = this.VitualPath.Text;
            int num2 = 1;
            if (this.IsEffect.Text == num2.ToString())
                url.IsEffect = true;
            else
                url.IsEffect = false;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (url.ID == -2147483648)
            {
                base.CheckAdminPower("AddURLRewriter", PowerCheckType.Single);
                int id = URLClass.AddURL(url);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("URLRewriter"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateURLRewriter", PowerCheckType.Single);
                URLClass.UpdateURL(url);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("URLRewriter"), url.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

