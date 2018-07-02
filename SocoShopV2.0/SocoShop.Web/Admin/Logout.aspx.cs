namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using System;
    using System.Web.UI;

    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("LogoutSystem"));
            CookiesHelper.DeleteCookie(ShopConfig.ReadConfigInfo().AdminCookies);
            ResponseHelper.Redirect("/");
        }
    }
}

