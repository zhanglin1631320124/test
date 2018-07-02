namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Web.UI.WebControls;

    public partial class UserActive : AdminBasePage
    {
        protected string result = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("StatisticsUser", PowerCheckType.Single);
                this.Sex.DataSource = EnumHelper.ReadEnumList<SexType>();
                this.Sex.DataValueField = "Value";
                this.Sex.DataTextField = "ChineseName";
                this.Sex.DataBind();
                this.Sex.Items.Insert(0, new ListItem("所有", string.Empty));
                UserSearchInfo userSearch = new UserSearchInfo();
                userSearch.UserName = RequestHelper.GetQueryString<string>("UserName");
                userSearch.Sex = RequestHelper.GetQueryString<int>("Sex");
                userSearch.StartRegisterDate = RequestHelper.GetQueryString<DateTime>("StartRegisterDate");
                userSearch.EndRegisterDate = ShopCommon.SearchEndDate(RequestHelper.GetQueryString<DateTime>("EndRegisterDate"));
                this.UserName.Text = userSearch.UserName;
                this.StartRegisterDate.Text = RequestHelper.GetQueryString<string>("StartRegisterDate");
                this.EndRegisterDate.Text = RequestHelper.GetQueryString<string>("EndRegisterDate");
                this.Sex.Text = RequestHelper.GetQueryString<int>("Sex").ToString();
                string queryString = RequestHelper.GetQueryString<string>("UserOrderType");
                queryString = (queryString == string.Empty) ? "LoginTimes" : queryString;
                this.UserOrderType.Text = queryString;
                base.BindControl(UserBLL.StatisticsUserActive(base.CurrentPage, base.PageSize, userSearch, ref this.Count, queryString), this.RecordList, this.MyPager);
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            ResponseHelper.Redirect((((("UserActive.aspx?Action=search&" + "UserName=" + this.UserName.Text + "&") + "Sex=" + this.Sex.Text + "&") + "UserOrderType=" + this.UserOrderType.Text + "&") + "StartRegisterDate=" + this.StartRegisterDate.Text + "&") + "EndRegisterDate=" + this.EndRegisterDate.Text);
        }
    }
}

