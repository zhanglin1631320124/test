namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;

    public class ChangePassword : UserBasePage
    {
        protected override void PageLoad()
        {
            base.PageLoad();
        }

        protected override void PostBack()
        {
            string str = StringHelper.Password(RequestHelper.GetForm<string>("OldPassword"), (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
            string newPassword = StringHelper.Password(RequestHelper.GetForm<string>("UserPassword1"), (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
            UserInfo info = UserBLL.ReadUser(base.UserID);
            if (str == info.UserPassword)
            {
                UserBLL.ChangePassword(base.UserID, newPassword);
                ScriptHelper.Alert("密码修改成功", RequestHelper.RawUrl);
            }
            else
                ScriptHelper.Alert("旧密码错误", RequestHelper.RawUrl);
        }
    }
}

