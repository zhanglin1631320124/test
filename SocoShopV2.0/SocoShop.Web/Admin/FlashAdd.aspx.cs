namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class FlashAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadFlash", PowerCheckType.Single);
                    FlashInfo info = FlashBLL.ReadFlash(queryString);
                    this.txtTitle.Text = info.Title;
                    this.Introduce.Text = info.Introduce;
                    this.Width.Text = info.Width.ToString();
                    this.Height.Text = info.Height.ToString();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            FlashInfo flash = new FlashInfo();
            flash.ID = RequestHelper.GetQueryString<int>("ID");
            flash.Title = this.txtTitle.Text;
            flash.Introduce = this.Introduce.Text;
            flash.Width = Convert.ToInt32(this.Width.Text);
            flash.Height = Convert.ToInt32(this.Height.Text);
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (flash.ID == -2147483648)
            {
                base.CheckAdminPower("AddFlash", PowerCheckType.Single);
                int id = FlashBLL.AddFlash(flash);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Flash"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateFlash", PowerCheckType.Single);
                FlashBLL.UpdateFlash(flash);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Flash"), flash.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

