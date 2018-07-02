namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class UserPasswordAdd : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.CheckAdminPower("UpdateUser", PowerCheckType.Single);
            int queryString = RequestHelper.GetQueryString<int>("ID");
            if (queryString != -2147483648) this.Name.Text = UserBLL.ReadUser(queryString).UserName;
        }

        protected void SubmitButton_Click(object sender, EventArgs E)
        {
            int queryString = RequestHelper.GetQueryString<int>("ID");
            if (queryString != -2147483648)
            {
                string newPassword = StringHelper.Password(this.NewPassword.Text, (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
                UserBLL.ChangePassword(queryString, newPassword);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("ChangeUserPassword"), queryString);
                AdminBasePage.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
            }
        }
    }
}

