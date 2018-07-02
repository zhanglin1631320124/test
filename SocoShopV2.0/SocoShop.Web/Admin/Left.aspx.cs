namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;

    public partial class Left : AdminBasePage
    {
        protected List<MenuInfo> menuList = new List<MenuInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAdminPower("ReadMenu", PowerCheckType.Single);
            int queryString = RequestHelper.GetQueryString<int>("ID");
            if (queryString == -2147483648) queryString = 1;
            this.menuList = MenuBLL.ReadMenuChildList(queryString);
        }
    }
}

