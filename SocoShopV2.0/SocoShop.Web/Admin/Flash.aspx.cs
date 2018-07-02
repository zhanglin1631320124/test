namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Flash : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadFlash", PowerCheckType.Single);
                base.BindControl(FlashBLL.ReadFlashList(base.CurrentPage, base.PageSize, ref this.Count), this.RecordList, this.MyPager);
            }
        }
    }
}

