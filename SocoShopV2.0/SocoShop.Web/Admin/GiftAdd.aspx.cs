namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class GiftAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadGift", PowerCheckType.Single);
                    GiftInfo info = GiftBLL.ReadGift(queryString);
                    this.Name.Text = info.Name;
                    this.Photo.Text = info.Photo;
                    this.Description.Text = info.Description;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            GiftInfo gift = new GiftInfo();
            gift.ID = RequestHelper.GetQueryString<int>("ID");
            gift.Name = this.Name.Text;
            gift.Photo = this.Photo.Text;
            gift.Description = this.Description.Text;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (gift.ID == -2147483648)
            {
                base.CheckAdminPower("AddGift", PowerCheckType.Single);
                int id = GiftBLL.AddGift(gift);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Gift"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateGift", PowerCheckType.Single);
                GiftBLL.UpdateGift(gift);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Gift"), gift.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

