namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class UserMessageAjax : UserAjaxBasePage
    {
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected List<UserMessageInfo> userMessageList = new List<UserMessageInfo>();

        protected void AddUserMessage()
        {
            string content = string.Empty;
            int queryString = RequestHelper.GetQueryString<int>("MessageClass");
            string str2 = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("Title"));
            string str3 = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("Content"));
            if (str3 == string.Empty || str3 == string.Empty)
                content = "请填写标题和内容";
            else
            {
                UserMessageInfo userMessage = new UserMessageInfo();
                userMessage.MessageClass = queryString;
                userMessage.Title = str2;
                userMessage.Content = str3;
                userMessage.UserIP = ClientHelper.IP;
                userMessage.PostDate = RequestHelper.DateNow;
                userMessage.IsHandler = 0;
                userMessage.AdminReplyContent = string.Empty;
                userMessage.AdminReplyDate = RequestHelper.DateNow;
                userMessage.UserID = base.UserID;
                userMessage.UserName = base.UserName;
                UserMessageBLL.AddUserMessage(userMessage);
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            if (RequestHelper.GetQueryString<string>("Action") == "AddUserMessage") this.AddUserMessage();
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 15;
            int count = 0;
            UserMessageSeachInfo userMessage = new UserMessageSeachInfo();
            userMessage.UserID = base.UserID;
            this.userMessageList = UserMessageBLL.SearchUserMessageList(queryString, pageSize, userMessage, ref count);
            this.ajaxPagerClass.CurrentPage = queryString;
            this.ajaxPagerClass.PageSize = pageSize;
            this.ajaxPagerClass.Count = count;
        }
    }
}

