namespace SocoShop.Page
{
    using System;

    public abstract class UserAjaxBasePage : AjaxBasePage
    {
        protected UserAjaxBasePage()
        {
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            base.CheckUserLogin();
        }
    }
}

