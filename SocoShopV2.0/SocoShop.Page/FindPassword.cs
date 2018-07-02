namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;

    public class FindPassword : CommonBasePage
    {
        protected string errorMessage = string.Empty;
        protected string result = string.Empty;

        protected override void PageLoad()
        {
            base.PageLoad();
            this.result = RequestHelper.GetQueryString<string>("Result");
            this.errorMessage = RequestHelper.GetQueryString<string>("ErrorMessage");
        }

        protected override void PostBack()
        {
            string userName = StringHelper.SearchSafe(RequestHelper.GetForm<string>("UserName"));
            string email = StringHelper.SearchSafe(RequestHelper.GetForm<string>("Email"));
            string form = RequestHelper.GetForm<string>("SafeCode");
            int id = 0;
            if (userName == string.Empty) this.errorMessage = "用户名不能为空";
            if (this.errorMessage == string.Empty)
            {
                id = UserBLL.CheckUserName(userName);
                if (id == 0) this.errorMessage = "不存在该用户名";
            }
            if (this.errorMessage == string.Empty && email == string.Empty) this.errorMessage = "Email不能为空";
            if (this.errorMessage == string.Empty && !UserBLL.CheckEmail(email)) this.errorMessage = "不存在该Email";
            if (this.errorMessage == string.Empty && form.ToLower() != Cookies.Common.CheckCode.ToLower()) this.errorMessage = "验证码错误";
            if (this.errorMessage == string.Empty && UserBLL.ReadUser(id).Email != email) this.errorMessage = "用户名和Email不匹配";
            if (this.errorMessage == string.Empty)
            {
                string safeCode = Guid.NewGuid().ToString();
                UserBLL.ChangeUserSafeCode(id, safeCode, RequestHelper.DateNow);
                string newValue = "http://" + base.Request.ServerVariables["HTTP_HOST"] + "/User/ResetPassword.aspx?CheckCode=" + StringHelper.Encode(string.Concat(new object[] { id, "|", email, "|", userName, "|", safeCode }), ShopConfig.ReadConfigInfo().SecureKey);
                EmailContentInfo info2 = EmailContentHelper.ReadSystemEmailContent("FindPassword");
                EmailSendRecordInfo emailSendRecord = new EmailSendRecordInfo();
                emailSendRecord.Title = info2.EmailTitle;
                emailSendRecord.Content = info2.EmailContent.Replace("$Url$", newValue);
                emailSendRecord.IsSystem = 1;
                emailSendRecord.EmailList = email;
                emailSendRecord.IsStatisticsOpendEmail = 0;
                emailSendRecord.SendStatus = 1;
                emailSendRecord.AddDate = RequestHelper.DateNow;
                emailSendRecord.SendDate = RequestHelper.DateNow;
                emailSendRecord.ID = EmailSendRecordBLL.AddEmailSendRecord(emailSendRecord);
                EmailSendRecordBLL.SendEmail(emailSendRecord);
                this.result = "您的申请已提交，请登录邮箱重设你的密码！<a href=\"http://mail." + email.Substring(email.IndexOf("@") + 1) + "\"  target=\"_blank\">马上登录</a>";
                ResponseHelper.Redirect("/User/FindPassword.aspx?Result=" + base.Server.UrlEncode(this.result));
            }
            else
                ResponseHelper.Redirect("/User/FindPassword.aspx?ErrorMessage=" + base.Server.UrlEncode(this.errorMessage));
        }
    }
}

