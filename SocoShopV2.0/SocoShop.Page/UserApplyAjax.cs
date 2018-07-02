namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class UserApplyAjax : UserAjaxBasePage
    {
        protected string action = string.Empty;
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected decimal moneyLeft = 0M;
        protected List<UserApplyInfo> userApplyList = new List<UserApplyInfo>();

        protected void AddUserApply()
        {
            string content = string.Empty;
            decimal queryString = RequestHelper.GetQueryString<decimal>("Money");
            string str2 = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("UserNote"));
            if (queryString <= 0M || str2 == string.Empty)
                content = "请填写金额和备注";
            else if (UserBLL.ReadUserMore(base.UserID).MoneyLeft < queryString)
                content = "提现金额大于剩余金额";
            else
            {
                UserApplyInfo userApply = new UserApplyInfo();
                Random random = new Random();
                userApply.Number = RequestHelper.DateNow.ToString("yyMMddhh") + random.Next(0x3e8, 0x270f);
                userApply.Money = queryString;
                userApply.UserNote = str2;
                userApply.Status = 1;
                userApply.ApplyDate = RequestHelper.DateNow;
                userApply.ApplyIP = ClientHelper.IP;
                userApply.AdminNote = string.Empty;
                userApply.UpdateDate = RequestHelper.DateNow;
                userApply.UpdateAdminID = 0;
                userApply.UpdateAdminName = string.Empty;
                userApply.UserID = base.UserID;
                userApply.UserName = base.UserName;
                UserApplyBLL.AddUserApply(userApply);
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            this.action = RequestHelper.GetQueryString<string>("Action");
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 20;
            int count = 0;
            string action = this.action;
            if (action != null)
            {
                if (!(action == "Read"))
                {
                    if (action == "Add")
                        this.moneyLeft = UserBLL.ReadUserMore(base.UserID).MoneyLeft;
                    else if (action == "AddUserApply") this.AddUserApply();
                }
                else
                {
                    UserApplySearchInfo userApply = new UserApplySearchInfo();
                    userApply.UserID = base.UserID;
                    this.userApplyList = UserApplyBLL.SearchUserApplyList(queryString, pageSize, userApply, ref count);
                    this.ajaxPagerClass.CurrentPage = queryString;
                    this.ajaxPagerClass.PageSize = pageSize;
                    this.ajaxPagerClass.Count = count;
                }
            }
        }
    }
}

