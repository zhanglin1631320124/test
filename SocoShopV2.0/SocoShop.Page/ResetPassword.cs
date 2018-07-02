namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;

    public class ResetPassword : CommonBasePage
    {
        protected string errorMessage = string.Empty;
        protected string result = string.Empty;

        protected override void PageLoad()
        {
            base.PageLoad();
            string queryString = RequestHelper.GetQueryString<string>("CheckCode");
            if (queryString != string.Empty)
            {
                string str2 = StringHelper.Decode(queryString, ShopConfig.ReadConfigInfo().SecureKey);
                if (str2.IndexOf('|') > 0)
                {
                    int id = Convert.ToInt32(str2.Split(new char[] { '|' })[0]);
                    string str3 = str2.Split(new char[] { '|' })[1];
                    string str4 = str2.Split(new char[] { '|' })[2];
                    string str5 = str2.Split(new char[] { '|' })[3];
                    UserInfo info = UserBLL.ReadUser(id);
                    if (info.ID > 0 && info.UserName == str4 && info.Email == str3 && str5 == info.SafeCode)
                    {
                        if (ShopConfig.ReadConfigInfo().FindPasswordTimeRestrict > 0.0 && info.FindDate.AddHours(ShopConfig.ReadConfigInfo().FindPasswordTimeRestrict) < RequestHelper.DateNow) this.errorMessage = "信息过时，请重新申请";
                    }
                    else
                        this.errorMessage = "错误的信息";
                }
                else
                    this.errorMessage = "错误的信息";
            }
            this.result = RequestHelper.GetQueryString<string>("Result");
        }

        protected override void PostBack()
        {
            string newPassword = StringHelper.Password(RequestHelper.GetForm<string>("UserPassword1"), (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
            int id = Convert.ToInt32(StringHelper.Decode(RequestHelper.GetForm<string>("CheckCode"), ShopConfig.ReadConfigInfo().SecureKey).Split(new char[] { '|' })[0]);
            UserBLL.ChangePassword(id, newPassword);
            UserBLL.ChangeUserSafeCode(id, string.Empty, RequestHelper.DateNow);
            this.result = "恭喜您，密码修改成功！";
            ResponseHelper.Redirect("/User/ResetPassword.aspx?Result=" + base.Server.UrlEncode(this.result));
        }
    }
}

