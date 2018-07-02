namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class User : AdminBasePage
    {
        protected int status = 0;

        protected void ActiveButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("ActiveUser", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                UserBLL.ChangeUserStatus(intsForm, 2);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("ActiveRecord"), ShopLanguage.ReadLanguage("User"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("ActiveOK"), RequestHelper.RawUrl);
            }
        }

        protected void FreezeButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("FreezeUser", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                UserBLL.ChangeUserStatus(intsForm, 3);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("FreezeRecord"), ShopLanguage.ReadLanguage("User"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("FreezeOK"), RequestHelper.RawUrl);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadUser", PowerCheckType.Single);
                this.Sex.DataSource = EnumHelper.ReadEnumList<SexType>();
                this.Sex.DataValueField = "Value";
                this.Sex.DataTextField = "ChineseName";
                this.Sex.DataBind();
                this.Sex.Items.Insert(0, new ListItem("所有", string.Empty));
                this.status = RequestHelper.GetQueryString<int>("Status");
                UserSearchInfo user = new UserSearchInfo();
                user.UserName = RequestHelper.GetQueryString<string>("UserName");
                user.Email = RequestHelper.GetQueryString<string>("Email");
                user.Sex = RequestHelper.GetQueryString<int>("Sex");
                user.StartRegisterDate = RequestHelper.GetQueryString<DateTime>("StartRegisterDate");
                user.EndRegisterDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndRegisterDate"));
                user.Status = RequestHelper.GetQueryString<int>("Status");
                this.UserName.Text = user.UserName;
                this.Email.Text = user.Email;
                this.StartRegisterDate.Text = RequestHelper.GetQueryString<string>("StartRegisterDate");
                this.EndRegisterDate.Text = RequestHelper.GetQueryString<string>("EndRegisterDate");
                base.BindControl(UserBLL.SearchUserList(base.CurrentPage, base.PageSize, user, ref this.Count), this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect(((((("User.aspx?Action=search&" + "UserName=" + this.UserName.Text + "&") + "Email=" + this.Email.Text + "&") + "Sex=" + this.Sex.Text + "&") + "StartRegisterDate=" + this.StartRegisterDate.Text + "&") + "EndRegisterDate=" + this.EndRegisterDate.Text + "&") + "Status=" + RequestHelper.GetQueryString<string>("Status"));
        }

        protected void UnActiveButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("UnActiveUser", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                UserBLL.ChangeUserStatus(intsForm, 1);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UnActiveRecord"), ShopLanguage.ReadLanguage("User"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("UnActiveOK"), RequestHelper.RawUrl);
            }
        }

        protected void UnFreezeButton_Click(object sender, EventArgs e)
        {
            base.CheckAdminPower("UnFreezeUser", PowerCheckType.Single);
            string intsForm = RequestHelper.GetIntsForm("SelectID");
            if (intsForm != string.Empty)
            {
                UserBLL.ChangeUserStatus(intsForm, 2);
                AdminLogBLL.AddAdminLog(ShopLanguage.ReadLanguage("UnFreezeRecord"), ShopLanguage.ReadLanguage("User"), intsForm);
                ScriptHelper.Alert(ShopLanguage.ReadLanguage("UnFreezeOK"), RequestHelper.RawUrl);
            }
        }
    }
}

