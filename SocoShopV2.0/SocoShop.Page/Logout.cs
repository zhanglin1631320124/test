namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using System;

    public class Logout : CommonBasePage
    {
        protected override void PageLoad()
        {
            base.PageLoad();
            CookiesHelper.DeleteCookie(ShopConfig.ReadConfigInfo().UserCookies);
            CookiesHelper.DeleteCookie("UserPhoto");
            ResponseHelper.Redirect("/");
        }
    }
}

