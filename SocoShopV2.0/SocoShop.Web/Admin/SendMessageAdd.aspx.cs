namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class SendMessageAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            SendMessageInfo sendMessage = new SendMessageInfo();
            sendMessage.Title = this.txtTitle.Text;
            sendMessage.Content = this.Content.Text;
            sendMessage.Date = RequestHelper.DateNow;
            sendMessage.ToUserID = RequestHelper.GetIntsForm("UserIDList");
            sendMessage.ToUserName = RequestHelper.GetForm<string>("UserNameList");
            sendMessage.UserID = 0;
            sendMessage.UserName = string.Empty;
            sendMessage.IsAdmin = 1;
            base.CheckAdminPower("AddSendMessage", PowerCheckType.Single);
            int id = SendMessageBLL.AddSendMessage(sendMessage);
            string[] strArray = sendMessage.ToUserID.Split(new char[] { ',' });
            string[] strArray2 = sendMessage.ToUserName.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                ReceiveMessageInfo receiveMessage = new ReceiveMessageInfo();
                receiveMessage.Title = sendMessage.Title;
                receiveMessage.Content = sendMessage.Content;
                receiveMessage.Date = sendMessage.Date;
                receiveMessage.IsRead = 0;
                receiveMessage.IsAdmin = 1;
                receiveMessage.FromUserID = 0;
                receiveMessage.FromUserName = string.Empty;
                receiveMessage.UserID = Convert.ToInt32(strArray[i]);
                receiveMessage.UserName = strArray2[i];
                ReceiveMessageBLL.AddReceiveMessage(receiveMessage);
            }
            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("SendMessage"), id);
            AdminBasePage.Alert(ShopLanguage.ReadLanguage("AddOK"), RequestHelper.RawUrl);
        }
    }
}

