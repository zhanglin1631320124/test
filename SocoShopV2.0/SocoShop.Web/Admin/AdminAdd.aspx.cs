namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class AdminAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.GroupID.DataSource = AdminGroupBLL.ReadAdminGroupCacheList();
                this.GroupID.DataTextField = "Name";
                this.GroupID.DataValueField = "ID";
                this.GroupID.DataBind();
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadAdmin", PowerCheckType.Single);
                    AdminInfo info = AdminBLL.ReadAdmin(queryString);
                    this.GroupID.Text = info.GroupID.ToString();
                    this.Name.Text = info.Name;
                    this.Name.Enabled = false;
                    this.Email.Text = info.Email;
                    this.Add.Visible = false;
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            AdminInfo admin = new AdminInfo();
            admin.ID = RequestHelper.GetQueryString<int>("ID");
            if (admin.ID > 0) admin = AdminBLL.ReadAdmin(admin.ID);
            admin.Name = this.Name.Text;
            admin.Email = this.Email.Text;
            admin.GroupID = Convert.ToInt32(this.GroupID.Text);
            admin.Password = StringHelper.Password(this.Password.Text, (PasswordType) ShopConfig.ReadConfigInfo().PasswordType);
            admin.LastLoginDate = RequestHelper.DateNow;
            admin.LastLoginIP = ClientHelper.IP;
            admin.IsCreate = 0;
            string alertMessage = ShopLanguage.ReadLanguage("AddOK");
            if (admin.ID == -2147483648)
            {
                base.CheckAdminPower("AddAdmin", PowerCheckType.Single);
                int id = AdminBLL.AddAdmin(admin);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("Admin"), id);
            }
            else
            {
                base.CheckAdminPower("UpdateAdmin", PowerCheckType.Single);
                AdminBLL.UpdateAdmin(admin);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("Admin"), admin.ID);
                alertMessage = ShopLanguage.ReadLanguage("UpdateOK");
            }
            AdminBasePage.Alert(alertMessage, RequestHelper.RawUrl);
        }
    }
}

