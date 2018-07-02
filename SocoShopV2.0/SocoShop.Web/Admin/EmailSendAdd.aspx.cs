namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class EmailSendAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.UserGrade.DataSource = UserGradeBLL.ReadUserGradeCacheList();
                this.UserGrade.DataValueField = "ID";
                this.UserGrade.DataTextField = "Name";
                this.UserGrade.DataBind();
                this.Key.DataSource = EmailContentHelper.ReadCommonEmailContentList();
                this.Key.DataTextField = "EmailTitle";
                this.Key.DataValueField = "Key";
                this.Key.DataBind();
            }
        }

        protected string ReadUserEmail(string strUserGrade)
        {
            string str = string.Empty;
            Dictionary<decimal, decimal> moneyUsed = new Dictionary<decimal, decimal>();
            foreach (string str2 in strUserGrade.Split(new char[] { ',' }))
            {
                UserGradeInfo info = UserGradeBLL.ReadUserGradeCache(Convert.ToInt32(str2));
                moneyUsed.Add(info.MinMoney, info.MaxMoney);
            }
            List<string> list = UserBLL.ReadUserEmailByMoneyUsed(moneyUsed);
            foreach (string str2 in list)
            {
                if (str == string.Empty)
                    str = str2;
                else
                    str = str + "," + str2;
            }
            return str;
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("AddEmailSendRecord", PowerCheckType.Single);
            EmailContentInfo info = EmailContentHelper.ReadCommonEmailContent(this.Key.Text);
            EmailSendRecordInfo emailSendRecord = new EmailSendRecordInfo();
            emailSendRecord.Title = info.EmailTitle;
            emailSendRecord.Content = info.EmailContent;
            emailSendRecord.IsSystem = 0;
            emailSendRecord.EmailList = this.ReadUserEmail(ControlHelper.GetCheckBoxListValue(this.UserGrade));
            emailSendRecord.OpenEmailList = string.Empty;
            emailSendRecord.IsStatisticsOpendEmail = Convert.ToInt32(this.IsStatisticsOpendEmail.Text);
            emailSendRecord.Note = this.Note.Text;
            emailSendRecord.SendStatus = 1;
            emailSendRecord.AddDate = RequestHelper.DateNow;
            emailSendRecord.SendDate = RequestHelper.DateNow;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            int id = EmailSendRecordBLL.AddEmailSendRecord(emailSendRecord);
            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("EmailSendRecord"), id);
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

