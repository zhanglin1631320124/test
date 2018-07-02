namespace SocoShop.Page
{
    using System;

    public abstract class UserBasePage : CommonBasePage
    {
        protected UserBasePage()
        {
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            base.CheckUserLogin();
        }
    }
}

