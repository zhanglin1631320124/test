namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Login : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string loginName = StringHelper.SearchSafe(this.AdminName.Text);
            string content = StringHelper.SearchSafe(this.Password.Text);
            string text = this.SafeCode.Text;
            bool flag = this.Remember.Checked;
            if (!Cookies.Common.CheckCode.ToLower().Equals(text.ToLower())) ScriptHelper.Alert(ShopLanguage.ReadLanguage("SafeCodeError"), RequestHelper.RawUrl);
            content = StringHelper.Password(content, (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
            AdminInfo info = AdminBLL.CheckAdminLogin(loginName, content);
            if (info.ID > 0)
            {
                string str4 = Guid.NewGuid().ToString();
                string str5 = FormsAuthentication.HashPasswordForStoringInConfigFile(info.ID.ToString() + info.Name + info.GroupID.ToString() + str4 + ShopConfig.ReadConfigInfo().SecureKey + ClientHelper.Agent, "MD5");
                string str6 = str5 + "|" + info.ID.ToString() + "|" + info.Name + "|" + info.GroupID.ToString() + "|" + str4;
                if (flag)
                    CookiesHelper.AddCookie(ShopConfig.ReadConfigInfo().AdminCookies, str6, 1, TimeType.Year);
                else
                    CookiesHelper.AddCookie(ShopConfig.ReadConfigInfo().AdminCookies, str6);
                AdminBLL.UpdateAdminLogin(info.ID, RequestHelper.DateNow, ClientHelper.IP);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("LoginSystem"));
                ResponseHelper.Redirect("/Admin");
            }
            else
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("LoginFaild"), RequestHelper.RawUrl);
        }
    }
}

