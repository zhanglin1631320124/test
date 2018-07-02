namespace SocoShop.Web.Admin
{
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class Region : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAdminPower("ReadRegion", PowerCheckType.Single);
        }
    }
}

