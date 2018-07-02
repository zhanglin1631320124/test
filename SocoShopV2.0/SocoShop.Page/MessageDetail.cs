namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;

    public class MessageDetail : UserBasePage
    {
        protected SendMessageInfo sendMessage = new SendMessageInfo();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.sendMessage = SendMessageBLL.ReadSendMessage(queryString, base.UserID);
        }
    }
}

