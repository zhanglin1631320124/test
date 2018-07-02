namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;

    public partial class UserAccountRecord : AdminBasePage
    {
        protected int accountType = 0;
        protected decimal moneyLeft = 0M;
        protected int pointLeft = 0;
        protected UserInfo user = new UserInfo();
        protected List<UserAccountRecordInfo> userAccountRecordList = new List<UserAccountRecordInfo>();
        protected int userID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                base.CheckAdminPower("ReadUserAccountRecord", PowerCheckType.Single);
                this.userID = RequestHelper.GetQueryString<int>("UserID");
                this.user = UserBLL.ReadUserMore(this.userID);
                this.accountType = RequestHelper.GetQueryString<int>("AccountType");
                if (this.accountType == -2147483648) this.accountType = 1;
                base.PageSize = 12;
                this.userAccountRecordList = UserAccountRecordBLL.ReadUserAccountRecordList(base.CurrentPage, base.PageSize, ref this.Count, this.userID, this.accountType);
                if (this.userAccountRecordList.Count > 0)
                {
                    if (this.accountType == 1)
                        this.moneyLeft = UserAccountRecordBLL.ReadMoneyLeftBeforID(this.userAccountRecordList[0].ID, this.userID);
                    else
                        this.pointLeft = UserAccountRecordBLL.ReadPointLeftBeforID(this.userAccountRecordList[0].ID, this.userID);
                }
                base.BindControl(this.MyPager);
            }
        }
    }
}

