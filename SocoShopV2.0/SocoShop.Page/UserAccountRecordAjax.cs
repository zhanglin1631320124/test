namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using System;
    using System.Collections.Generic;
    using SocoShop.Entity;

    public class UserAccountRecordAjax : UserAjaxBasePage
    {
        protected int accountType = 0;
        protected string action = string.Empty;
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected decimal moneyLeft = 0M;
        protected int pointLeft = 0;
        protected List<UserAccountRecordInfo> userAccountRecordList = new List<UserAccountRecordInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            this.action = RequestHelper.GetQueryString<string>("Action");
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 15;
            int count = 0;
            this.accountType = 2;
            if (this.action == "Money") this.accountType = 1;
            this.userAccountRecordList = UserAccountRecordBLL.ReadUserAccountRecordList(queryString, pageSize, ref count, base.UserID, this.accountType);
            if (this.userAccountRecordList.Count > 0)
            {
                if (this.accountType == 1)
                    this.moneyLeft = UserAccountRecordBLL.ReadMoneyLeftBeforID(this.userAccountRecordList[0].ID, base.UserID);
                else
                    this.pointLeft = UserAccountRecordBLL.ReadPointLeftBeforID(this.userAccountRecordList[0].ID, base.UserID);
            }
            this.ajaxPagerClass.CurrentPage = queryString;
            this.ajaxPagerClass.PageSize = pageSize;
            this.ajaxPagerClass.Count = count;
        }
    }
}

