namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;

    public partial class SendMessageDetail : AdminBasePage
    {
        protected SendMessageInfo sendMessage = new SendMessageInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            int queryString = RequestHelper.GetQueryString<int>("ID");
            if (queryString != -2147483648)
            {
                base.CheckAdminPower("ReadSendMessage", PowerCheckType.Single);
                this.sendMessage = SendMessageBLL.ReadSendMessage(queryString, 0);
            }
        }
    }
}

