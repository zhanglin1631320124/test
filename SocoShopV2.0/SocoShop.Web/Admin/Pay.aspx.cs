namespace SocoShop.Web.Admin
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Pay : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadPay", PowerCheckType.Single);
                base.BindControl(PayPlugins.ReadPayPluginsList(), this.RecordList);
            }
        }
    }
}

