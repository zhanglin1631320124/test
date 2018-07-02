namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Sitemap : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("Sitemap", PowerCheckType.Single);
                this.Domain.Text = ShopConfig.ReadConfigInfo().Domain;
                this.Frequency.Text = ShopConfig.ReadConfigInfo().Frequency;
                this.Priority.Text = ShopConfig.ReadConfigInfo().Priority;
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("Sitemap", PowerCheckType.Single);
            ShopConfigInfo config = ShopConfig.ReadConfigInfo();
            config.Domain = this.Domain.Text;
            config.Frequency = this.Frequency.Text;
            config.Priority = this.Priority.Text;
            ShopConfig.UpdateConfigInfo(config);
            ScriptHelper.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
        }
    }
}

