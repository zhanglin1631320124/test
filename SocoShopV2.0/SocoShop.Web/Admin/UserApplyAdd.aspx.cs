namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class UserApplyAdd : AdminBasePage
    {
        protected UserApplyInfo userApply = new UserApplyInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                int queryString = RequestHelper.GetQueryString<int>("ID");
                if (queryString != -2147483648)
                {
                    base.CheckAdminPower("ReadUserApply", PowerCheckType.Single);
                    this.userApply = UserApplyBLL.ReadUserApply(queryString, 0);
                    this.Status.Text = this.userApply.Status.ToString();
                    this.AdminNote.Text = this.userApply.AdminNote;
                    if (this.userApply.Status != 1)
                    {
                        this.Status.Enabled = false;
                        this.SubmitButton.Enabled = false;
                    }
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            int queryString = RequestHelper.GetQueryString<int>("ID");
            if (queryString > 0)
            {
                UserApplyInfo userApply = UserApplyBLL.ReadUserApply(queryString, 0);
                userApply.Status = Convert.ToInt32(this.Status.Text);
                userApply.AdminNote = this.AdminNote.Text;
                userApply.UpdateDate = RequestHelper.DateNow;
                userApply.UpdateAdminID = Cookies.Admin.GetAdminID(false);
                userApply.UpdateAdminName = Cookies.Admin.GetAdminName(false);
                string str = ShopLanguage.ReadLanguage("AddOK");
                base.CheckAdminPower("UpdateUserApply", PowerCheckType.Single);
                UserApplyBLL.UpdateUserApply(userApply);
                if (userApply.Status == 2) UserAccountRecordBLL.AddUserAccountRecord(-userApply.Money, 0, "用户提现成功，流水号：" + userApply.Number, userApply.UserID);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UpdateRecord"), ShopLanguage.ReadLanguage("UserApply"), userApply.ID);
                AdminBasePage.Alert(ShopLanguage.ReadLanguage("UpdateOK"), RequestHelper.RawUrl);
            }
        }
    }
}

