namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class UserRechargeAjax : UserAjaxBasePage
    {
        protected string action = string.Empty;
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected decimal moneyLeft = 0M;
        protected List<PayPluginsInfo> payPluginsList = new List<PayPluginsInfo>();
        protected List<UserRechargeInfo> userRechargeList = new List<UserRechargeInfo>();

        protected void AddUserRecharge()
        {
            string content = string.Empty;
            decimal queryString = RequestHelper.GetQueryString<decimal>("Money");
            string key = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("PayKey"));
            if (queryString <= 0M || key == string.Empty)
                content = "请填写金额和选择支付方式";
            else
            {
                UserRechargeInfo userRecharge = new UserRechargeInfo();
                Random random = new Random();
                userRecharge.Number = RequestHelper.DateNow.ToString("yyMMddhh") + random.Next(0x3e8, 0x270f);
                userRecharge.Money = queryString;
                userRecharge.PayKey = key;
                userRecharge.PayName = PayPlugins.ReadPayPlugins(key).Name;
                userRecharge.RechargeDate = RequestHelper.DateNow;
                userRecharge.RechargeIP = ClientHelper.IP;
                userRecharge.IsFinish = 0;
                userRecharge.UserID = base.UserID;
                userRecharge.UserName = base.UserName;
                content = UserRechargeBLL.AddUserRecharge(userRecharge).ToString();
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            this.action = RequestHelper.GetQueryString<string>("Action");
            string action = this.action;
            if (action != null)
            {
                if (!(action == "Read"))
                {
                    if (action == "Add")
                    {
                        this.payPluginsList = PayPlugins.ReadRechargePayPluginsList();
                        this.moneyLeft = UserBLL.ReadUserMore(base.UserID).MoneyLeft;
                    }
                    else if (action == "AddUserRecharge") this.AddUserRecharge();
                }
                else
                {
                    int queryString = RequestHelper.GetQueryString<int>("Page");
                    if (queryString < 1) queryString = 1;
                    int pageSize = 20;
                    int count = 0;
                    UserRechargeSearchInfo userRecharge = new UserRechargeSearchInfo();
                    userRecharge.UserID = base.UserID;
                    this.userRechargeList = UserRechargeBLL.SearchUserRechargeList(queryString, pageSize, userRecharge, ref count);
                    this.ajaxPagerClass.CurrentPage = queryString;
                    this.ajaxPagerClass.PageSize = pageSize;
                    this.ajaxPagerClass.Count = count;
                }
            }
        }
    }
}

