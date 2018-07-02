namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;

    public class UserMessageDetail : UserBasePage
    {
        protected UserMessageInfo userMessage = new UserMessageInfo();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.userMessage = UserMessageBLL.ReadUserMessage(queryString, base.UserID);
        }
    }
}

