namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class TaobaoProduct : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("TaobaoProduct", PowerCheckType.Single);
                this.AppKey.Text = ShopConfig.ReadConfigInfo().AppKey;
                this.AppSecret.Text = ShopConfig.ReadConfigInfo().AppSecret;
                this.NickName.Text = ShopConfig.ReadConfigInfo().NickName;
                this.DeleteProductClass.Text = ShopConfig.ReadConfigInfo().DeleteProductClass.ToString();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("TaobaoProduct", PowerCheckType.Single);
            ShopConfigInfo config = ShopConfig.ReadConfigInfo();
            config.AppKey = this.AppKey.Text;
            config.AppSecret = this.AppSecret.Text;
            config.NickName = this.NickName.Text;
            config.DeleteProductClass = Convert.ToInt32(this.DeleteProductClass.Text);
            ShopConfig.UpdateConfigInfo(config);
            ScriptHelper.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
        }
    }
}

