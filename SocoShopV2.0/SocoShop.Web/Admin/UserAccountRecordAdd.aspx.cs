namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class UserAccountRecordAdd : AdminBasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            int queryString = RequestHelper.GetQueryString<int>("UserID");
            decimal money = Convert.ToDecimal(this.Money.Text);
            int point = Convert.ToInt32(this.Point.Text);
            string text = this.Note.Text;
            string message = ShopLanguage.ReadLanguage("AddOK");
            base.CheckAdminPower("AddUserAccountRecord", PowerCheckType.Single);
            int id = UserAccountRecordBLL.AddUserAccountRecord(money, point, text, queryString);
            AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("AddRecord"), ShopLanguage.ReadLanguage("UserAccountRecord"), id);
            ScriptHelper.Alert(message, RequestHelper.RawUrl);
        }
    }
}

