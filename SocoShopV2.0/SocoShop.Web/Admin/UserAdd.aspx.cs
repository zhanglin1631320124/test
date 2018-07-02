namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Text.RegularExpressions;
    using System.Web.UI.WebControls;

    public partial class UserAdd : AdminBasePage
    {
        protected int city = 0;
        protected int country = 0;
        protected int district = 0;
        protected int province = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                this.UserRegion.DataSource = RegionBLL.ReadRegionUnlimitClass();
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadUser", PowerCheckType.Single);
                    UserInfo info = UserBLL.ReadUser(queryString);
                    this.UserName.Text = info.UserName;
                    this.UserPassword.Text = info.UserPassword;
                    this.Email.Text = info.Email;
                    this.Sex.Text = info.Sex.ToString();
                    this.Introduce.Text = info.Introduce;
                    this.Photo.Text = info.Photo;
                    this.MSN.Text = info.MSN;
                    this.QQ.Text = info.QQ;
                    this.Tel.Text = info.Tel;
                    this.Mobile.Text = info.Mobile;
                    this.UserRegion.ClassID = info.RegionID;
                    this.Address.Text = info.Address;
                    this.Birthday.Text = info.Birthday;
                    this.Status.Text = info.Status.ToString();
                    this.UserName.Enabled = false;
                    this.Add.Visible = false;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string text = this.UserName.Text;
            int queryString = RequestHelper.GetQueryString<int>("ID");
            if (queryString == 0)
            {
                Regex regex = new Regex("^([a-zA-Z0-9_一-龥])+$");
                if (!regex.IsMatch(text)) ScriptHelper.Alert("用户名只能包含字母、数字、下划线、中文", RequestHelper.RawUrl);
            }
            UserInfo user = new UserInfo();
            user.ID = queryString;
            user.UserName = text;
            user.UserPassword = this.UserPassword.Text;
            user.Email = this.Email.Text;
            user.Sex = Convert.ToInt32(this.Sex.Text);
            user.Introduce = this.Introduce.Text;
            user.Photo = this.Photo.Text;
            user.MSN = this.MSN.Text;
            user.QQ = this.QQ.Text;
            user.Tel = this.Tel.Text;
            user.Mobile = this.Mobile.Text;
            user.RegionID = this.UserRegion.ClassID;
            user.Address = this.Address.Text;
            user.Birthday = this.Birthday.Text;
            user.RegisterDate = RequestHelper.DateNow;
            user.LastLoginDate = RequestHelper.DateNow;
            user.FindDate = RequestHelper.DateNow;
            user.Status = Convert.ToInt32(this.Status.Text);
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (user.ID == -2147483648)
            {
                base.CheckAdminPower("AddUser", PowerCheckType.Single);
                int id = UserBLL.AddUser(user);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("User"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateUser", PowerCheckType.Single);
                UserBLL.UpdateUser(user);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("User"), user.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

