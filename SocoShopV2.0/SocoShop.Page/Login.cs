namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;

    public class Login : CommonBasePage
    {
        protected string redirectUrl = string.Empty;
        protected string result = string.Empty;

        protected override void PageLoad()
        {
            base.PageLoad();
            this.result = RequestHelper.GetQueryString<string>("Message");
            this.redirectUrl = RequestHelper.GetQueryString<string>("RedirectUrl");
        }

        protected override void PostBack()
        {
            string str4;
            this.redirectUrl = RequestHelper.GetForm<string>("RedirectUrl");
            string loginName = StringHelper.SearchSafe(RequestHelper.GetForm<string>("UserName"));
            string loginPass = StringHelper.Password(RequestHelper.GetForm<string>("UserPassword"), (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
            if (!(RequestHelper.GetForm<string>("SafeCode").ToLower() == Cookies.Common.CheckCode.ToLower()))
                this.result = "验证码错误";
            else
            {
                UserInfo user = UserBLL.CheckUserLogin(loginName, loginPass);
                if (user.ID <= 0)
                    this.result = "用户名或者密码错误";
                else
                {
                    switch (user.Status)
                    {
                        case 1:
                            this.result = "该用户未激活";
                            goto Label_0142;

                        case 2:
                            user = UserBLL.ReadUserMore(user.ID);
                            UserBLL.UserLoginInit(user);
                            UserBLL.UpdateUserLogin(user.ID, RequestHelper.DateNow, ClientHelper.IP);
                            if (!(this.redirectUrl != string.Empty))
                                ResponseHelper.Redirect("/User/Index.aspx");
                            else
                                ResponseHelper.Redirect(this.redirectUrl);
                            goto Label_0142;

                        case 3:
                            this.result = "该用户已冻结";
                            goto Label_0142;
                    }
                }
            }
        Label_0142:
            str4 = "/User/Login.aspx?Message=" + this.result;
            if (this.redirectUrl != string.Empty) str4 = str4 + "&RedirectUrl=" + this.redirectUrl;
            ResponseHelper.Redirect(str4);
        }
    }
}

