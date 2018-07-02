namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class SendEmail : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            EmailSendRecordInfo emailSendRecord = new EmailSendRecordInfo();
            emailSendRecord.Title = this.txtTitle.Text;
            emailSendRecord.Content = this.Content.Text;
            emailSendRecord.IsSystem = 0;
            emailSendRecord.EmailList = RequestHelper.GetForm<string>("ToUserEmail");
            emailSendRecord.IsStatisticsOpendEmail = 0;
            emailSendRecord.SendStatus = 1;
            emailSendRecord.AddDate = RequestHelper.DateNow;
            emailSendRecord.SendDate = RequestHelper.DateNow;
            emailSendRecord.ID = EmailSendRecordBLL.AddEmailSendRecord(emailSendRecord);
            EmailSendRecordBLL.SendEmail(emailSendRecord);
            AdminBasePage.Alert(ShopLanguage.ReadLanguage("SendEmailOK"), RequestHelper.RawUrl);
        }
    }
}

