namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Text.RegularExpressions;
    using System.Web;

    public class Register : CommonBasePage
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
            string userName = StringHelper.SearchSafe(StringHelper.AddSafe(RequestHelper.GetForm<string>("UserName")));
            string str2 = StringHelper.SearchSafe(StringHelper.AddSafe(RequestHelper.GetForm<string>("Email")));
            string form = RequestHelper.GetForm<string>("UserPassword1");
            string str4 = RequestHelper.GetForm<string>("UserPassword2");
            string str5 = RequestHelper.GetForm<string>("SafeCode");
            if (userName == string.Empty) this.errorMessage = "用户名不能为空";
            if (this.errorMessage == string.Empty)
            {
                string forbiddenName = ShopConfig.ReadConfigInfo().ForbiddenName;
                if (forbiddenName != string.Empty)
                {
                    foreach (string str7 in forbiddenName.Split(new char[] { '|' }))
                    {
                        if (userName.IndexOf(str7.Trim()) != -1)
                        {
                            this.errorMessage = "用户名含有非法字符";
                            break;
                        }
                    }
                }
            }
            if (this.errorMessage == string.Empty && UserBLL.CheckUserName(userName) > 0) this.errorMessage = "用户名已经被占用";
            if (this.errorMessage == string.Empty)
            {
                Regex regex = new Regex("^([a-zA-Z0-9_一-龥])+$");
                if (!regex.IsMatch(userName)) this.errorMessage = "用户名只能包含字母、数字、下划线、中文";
            }
            if (this.errorMessage == string.Empty && form == string.Empty || str4 == string.Empty) this.errorMessage = "密码不能为空";
            if (this.errorMessage == string.Empty && form != str4) this.errorMessage = "两次密码不一致";
            if (this.errorMessage == string.Empty && str5.ToLower() != Cookies.Common.CheckCode.ToLower()) this.errorMessage = "验证码错误";
            if (this.errorMessage == string.Empty)
            {
                UserInfo user = new UserInfo();
                user.UserName = userName;
                user.UserPassword = StringHelper.Password(form, (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
                user.Email = str2;
                user.RegisterIP = ClientHelper.IP;
                user.RegisterDate = RequestHelper.DateNow;
                user.LastLoginIP = ClientHelper.IP;
                user.LastLoginDate = RequestHelper.DateNow;
                user.FindDate = RequestHelper.DateNow;
                if (ShopConfig.ReadConfigInfo().RegisterCheck == 1)
                    user.Status = 2;
                else
                    user.Status = 1;
                int id = UserBLL.AddUser(user);
                if (ShopConfig.ReadConfigInfo().RegisterCheck == 1)
                {
                    HttpCookie cookie = new HttpCookie(ShopConfig.ReadConfigInfo().UserCookies);
                    cookie["User"] = StringHelper.Encode(userName, ShopConfig.ReadConfigInfo().SecureKey);
                    cookie["Password"] = StringHelper.Encode(form, ShopConfig.ReadConfigInfo().SecureKey);
                    cookie["Key"] = StringHelper.Encode(ClientHelper.Agent, ShopConfig.ReadConfigInfo().SecureKey);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    user = UserBLL.ReadUserMore(id);
                    UserBLL.UserLoginInit(user);
                    UserBLL.UpdateUserLogin(user.ID, RequestHelper.DateNow, ClientHelper.IP);
                    ResponseHelper.Redirect("/User/Index.aspx");
                }
                else if (ShopConfig.ReadConfigInfo().RegisterCheck == 2)
                {
                    string newValue = "http://" + base.Request.ServerVariables["HTTP_HOST"] + "/User/ActiveUser.aspx?CheckCode=" + StringHelper.Encode(string.Concat(new object[] { id, "|", str2, "|", userName }), ShopConfig.ReadConfigInfo().SecureKey);
                    EmailContentInfo info2 = EmailContentHelper.ReadSystemEmailContent("Register");
                    EmailSendRecordInfo emailSendRecord = new EmailSendRecordInfo();
                    emailSendRecord.Title = info2.EmailTitle;
                    emailSendRecord.Content = info2.EmailContent.Replace("$UserName$", user.UserName).Replace("$Url$", newValue);
                    emailSendRecord.IsSystem = 1;
                    emailSendRecord.EmailList = str2;
                    emailSendRecord.IsStatisticsOpendEmail = 0;
                    emailSendRecord.SendStatus = 1;
                    emailSendRecord.AddDate = RequestHelper.DateNow;
                    emailSendRecord.SendDate = RequestHelper.DateNow;
                    emailSendRecord.ID = EmailSendRecordBLL.AddEmailSendRecord(emailSendRecord);
                    EmailSendRecordBLL.SendEmail(emailSendRecord);
                    this.result = "恭喜您，注册成功，请登录邮箱激活！<a href=\"http://mail." + str2.Substring(str2.IndexOf("@") + 1) + "\"  target=\"_blank\">马上激活</a>";
                }
                else
                    this.result = "恭喜您，注册成功，请等待我们的审核！";
                ResponseHelper.Redirect("/User/Register.aspx?Result=" + base.Server.UrlEncode(this.result));
            }
            else
                ResponseHelper.Redirect("/User/Register.aspx?ErrorMessage=" + base.Server.UrlEncode(this.errorMessage));
        }
    }
}

