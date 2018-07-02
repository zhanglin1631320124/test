namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Gift : AdminBasePage
    {

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteGift", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                GiftBLL.DeleteGift(intsForm);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("Gift"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadGift", PowerCheckType.Single);
                GiftSearchInfo gift = new GiftSearchInfo();
                gift.Name = RequestHelper.GetQueryString<string>("Name");
                base.BindControl(GiftBLL.SearchGiftList(base.CurrentPage, base.PageSize, gift, ref this.Count), this.RecordList, this.MyPager);
            }
        }
    }
}

