namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class ChangePassword : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAdminPower("UpdatePassword", PowerCheckType.Single);
            this.Name.Text = Cookies.Admin.GetAdminName(false);
        }

        protected void SubmitButton_Click(object sender, EventArgs E)
        {
            string oldPassword = StringHelper.Password(this.Password.Text, (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
            string newPassword = StringHelper.Password(this.NewPassword.Text, (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
            if (AdminBLL.ReadAdmin(Cookies.Admin.GetAdminID(false)).Password == oldPassword)
            {
                AdminBLL.ChangePassword(Cookies.Admin.GetAdminID(false), oldPassword, newPassword);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("ChangePassword"));
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
            }
            else
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("OldPasswordError"), RequestHelper.RawUrl);
        }
    }
}

