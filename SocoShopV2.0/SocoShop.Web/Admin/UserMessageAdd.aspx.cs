namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class UserMessageAdd : AdminBasePage
    {
        protected UserMessageInfo userMessage = new UserMessageInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadUserMessage", PowerCheckType.Single);
                    this.userMessage = UserMessageBLL.ReadUserMessage(queryString, 0);
                    this.IsHandler.Text = this.userMessage.IsHandler.ToString();
                    this.AdminReplyContent.Text = this.userMessage.AdminReplyContent;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            UserMessageInfo userMessage = new UserMessageInfo();
            userMessage.ID = RequestHelper.GetQueryString<int>("ID");
            userMessage.IsHandler = Convert.ToInt32(this.IsHandler.Text);
            userMessage.AdminReplyContent = this.AdminReplyContent.Text;
            userMessage.AdminReplyDate = RequestHelper.DateNow;
            string alertMessage = ShopLanguage.ReadLanguage("ReplyOK");
            if (userMessage.ID > 0)
            {
                base.CheckAdminPower("UpdateUserMessage", PowerCheckType.Single);
                UserMessageBLL.UpdateUserMessage(userMessage);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("UserMessage"), userMessage.ID);
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

