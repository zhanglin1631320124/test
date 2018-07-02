namespace SocoShop.Web.Admin
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ActivityPlugins : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadActivityPlugins", PowerCheckType.Single);
                base.BindControl(SocoShop.Common.ActivityPlugins.ReadActivityPluginsList(), this.RecordList);
            }
        }
    }
}

