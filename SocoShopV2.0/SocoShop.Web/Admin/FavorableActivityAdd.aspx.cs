namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class FavorableActivityAdd : AdminBasePage
    {
        protected FavorableActivityInfo favorableActivity = new FavorableActivityInfo();
        protected List<GiftInfo> giftList = new List<GiftInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.UserGrade.DataSource = UserGradeBLL.ReadUserGradeCacheList();
                this.UserGrade.DataTextField = "Name";
                this.UserGrade.DataValueField = "ID";
                this.UserGrade.DataBind();
                this.RegionID.DataSource = RegionBLL.ReadRegionUnlimitClass();
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadFavorableActivity", PowerCheckType.Single);
                    this.favorableActivity = FavorableActivityBLL.ReadFavorableActivity(queryString);
                    this.Photo.Text = this.favorableActivity.Photo;
                    this.Name.Text = this.favorableActivity.Name;
                    this.Content.Text = this.favorableActivity.Content;
                    this.StartDate.Text = this.favorableActivity.StartDate.ToString("yyyy-MM-dd");
                    this.EndDate.Text = this.favorableActivity.EndDate.ToString("yyyy-MM-dd");
                    ControlHelper.SetCheckBoxListValue(this.UserGrade, this.favorableActivity.UserGrade);
                    this.OrderProductMoney.Text = this.favorableActivity.OrderProductMoney.ToString();
                    this.RegionID.ClassIDList = this.favorableActivity.RegionID;
                    this.ReduceMoney.Text = this.favorableActivity.ReduceMoney.ToString();
                    this.ReduceDiscount.Text = this.favorableActivity.ReduceDiscount.ToString();
                    if (this.favorableActivity.GiftID != string.Empty)
                    {
                        GiftSearchInfo gift = new GiftSearchInfo();
                        gift.InGiftID = this.favorableActivity.GiftID;
                        this.giftList = GiftBLL.SearchGiftList(gift);
                    }
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            FavorableActivityInfo favorableActivity = new FavorableActivityInfo();
            favorableActivity.ID = RequestHelper.GetQueryString<int>("ID");
            favorableActivity.Name = this.Name.Text;
            favorableActivity.Photo = this.Photo.Text;
            favorableActivity.Content = this.Content.Text;
            favorableActivity.StartDate = Convert.ToDateTime(this.StartDate.Text);
            favorableActivity.EndDate = Convert.ToDateTime(this.EndDate.Text).AddDays(1.0).AddSeconds(-1.0);
            favorableActivity.UserGrade = ControlHelper.GetCheckBoxListValue(this.UserGrade);
            favorableActivity.OrderProductMoney = Convert.ToDecimal(this.OrderProductMoney.Text);
            int form = RequestHelper.GetForm<int>("ShippingWay");
            string classIDList = string.Empty;
            if (form == 1) classIDList = this.RegionID.ClassIDList;
            favorableActivity.RegionID = classIDList;
            favorableActivity.ShippingWay = form;
            int num2 = RequestHelper.GetForm<int>("ReduceWay");
            decimal num3 = 0M;
            decimal num4 = 0M;
            if (num2 == 1)
                num3 = Convert.ToDecimal(this.ReduceMoney.Text);
            else if (num2 == 2) num4 = Convert.ToDecimal(this.ReduceDiscount.Text);
            favorableActivity.ReduceWay = num2;
            favorableActivity.ReduceMoney = num3;
            favorableActivity.ReduceDiscount = num4;
            favorableActivity.GiftID = RequestHelper.GetIntsForm("GiftList");
            string alertMessage = string.Empty;
            if (FavorableActivityBLL.ReadFavorableActivity(favorableActivity.StartDate, favorableActivity.EndDate, favorableActivity.ID).ID > 0)
                alertMessage = ShopLanguage.ReadLanguage("OneTimeManyFavorableActivity");
            else
            {
                alertMessage = ShopLanguage.ReadLanguage("AddOK");
                if (favorableActivity.ID == -2147483648)
                {
                    base.CheckAdminPower("AddFavorableActivity", PowerCheckType.Single);
                    int id = FavorableActivityBLL.AddFavorableActivity(favorableActivity);
                    AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("FavorableActivity"), id);
                }
                else
                {
                    base.CheckAdminPower("UpdateFavorableActivity", PowerCheckType.Single);
                    FavorableActivityBLL.UpdateFavorableActivity(favorableActivity);
                    AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("FavorableActivity"), favorableActivity.ID);
                    alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
                }
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

