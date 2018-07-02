namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class UserApply : AdminBasePage
    {

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("DeleteUserApply", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                UserApplyBLL.DeleteUserApply(intsForm, 0);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("DeleteRecord"), ShopLanguage.ReadLanguage("UserApply"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("DeleteOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadUserApply", PowerCheckType.Single);
                UserApplySearchInfo userApply = new UserApplySearchInfo();
                userApply.Status = RequestHelper.GetQueryString<int>("Status");
                userApply.Number = RequestHelper.GetQueryString<string>("Number");
                userApply.StartApplyDate = RequestHelper.GetQueryString<DateTime>("StartApplyDate");
                userApply.EndApplyDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndApplyDate"));
                userApply.UserName = RequestHelper.GetQueryString<string>("UserName");
                this.Status.Text = userApply.Status.ToString();
                this.Number.Text = userApply.Number;
                this.StartApplyDate.Text = RequestHelper.GetQueryString<string>("StartApplyDate");
                this.EndApplyDate.Text = RequestHelper.GetQueryString<string>("EndApplyDate");
                this.UserName.Text = RequestHelper.GetQueryString<string>("UserName");
                base.BindControl(UserApplyBLL.SearchUserApplyList(base.CurrentPage, base.PageSize, userApply, ref this.Count), this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((((("UserApply.aspx?Action=search&" + "Status=" + this.Status.Text + "&") + "Number=" + this.Number.Text + "&") + "StartApplyDate=" + this.StartApplyDate.Text + "&") + "EndApplyDate=" + this.EndApplyDate.Text + "&") + "UserName=" + this.UserName.Text);
        }
    }
}

