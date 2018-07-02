namespace SocoShop.Page
{
    using System;

    public abstract class AjaxBasePage : BasePage
    {
        protected AjaxBasePage()
        {
        }

        protected void ClearCache()
        {
            base.Response.Cache.SetNoServerCaching();
            base.Response.Cache.SetNoStore();
            base.Response.Expires = 0;
        }

        protected override void PageLoad()
        {
            this.ClearCache();
        }
    }
}

