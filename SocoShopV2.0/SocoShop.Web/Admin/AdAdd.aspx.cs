namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class AdAdd : AdminBasePage
    {
        protected int classID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.classID = RequestHelper.GetQueryString<int>("ClassID");
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadAd", PowerCheckType.Single);
                    AdInfo info = AdBLL.ReadAd(queryString);
                    this.txtTitle.Text = info.Title;
                    this.Introduction.Text = info.Introduction;
                    this.AdClass.Text = info.AdClass.ToString();
                    if (info.AdClass == 1)
                    {
                        this.TextDisplay.Text = info.Display;
                        this.TextURL.Text = info.Url;
                    }
                    else if (info.AdClass == 2)
                    {
                        this.PictureDisplay.Text = info.Display;
                        this.PictureURL.Text = info.Url;
                    }
                    else if (info.AdClass == 3)
                        this.FlashDisplay.Text = info.Display;
                    else
                        this.CodeDisplay.Text = info.Display;
                    this.Width.Text = info.Width.ToString();
                    this.Height.Text = info.Height.ToString();
                    this.StartDate.Text = info.StartDate.ToString("yyyy-MM-dd");
                    this.EndDate.Text = info.EndDate.ToString("yyyy-MM-dd");
                    this.Remark.Text = info.Remark;
                    this.IsEnabled.Text = info.IsEnabled.ToString();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            AdInfo ad = new AdInfo();
            ad.ID = RequestHelper.GetQueryString<int>("ID");
            ad.Title = this.txtTitle.Text;
            ad.Introduction = this.Introduction.Text;
            ad.AdClass = Convert.ToInt32(this.AdClass.Text);
            if (ad.AdClass == 1)
            {
                ad.Display = this.TextDisplay.Text;
                ad.Url = this.TextURL.Text;
            }
            else if (ad.AdClass == 2)
            {
                ad.Display = this.PictureDisplay.Text;
                ad.Url = this.PictureURL.Text;
            }
            else if (ad.AdClass == 3)
            {
                ad.Display = this.FlashDisplay.Text;
                ad.Url = string.Empty;
            }
            else
            {
                ad.Display = this.CodeDisplay.Text;
                ad.Url = string.Empty;
            }
            ad.Width = Convert.ToInt32(this.Width.Text);
            ad.Height = Convert.ToInt32(this.Height.Text);
            ad.StartDate = Convert.ToDateTime(this.StartDate.Text);
            ad.EndDate = Convert.ToDateTime(this.EndDate.Text).AddDays(1.0).AddSeconds(-1.0);
            ad.Remark = this.Remark.Text;
            ad.IsEnabled = Convert.ToInt32(this.IsEnabled.Text);
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (ad.ID == -2147483648)
            {
                base.CheckAdminPower("AddAd", PowerCheckType.Single);
                int id = AdBLL.AddAd(ad);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Ad"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateAd", PowerCheckType.Single);
                AdBLL.UpdateAd(ad);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Ad"), ad.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

