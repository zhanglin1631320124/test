namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class MessageAjax : UserAjaxBasePage
    {
        protected string action = string.Empty;
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected List<ReceiveMessageInfo> receiveMessageList = new List<ReceiveMessageInfo>();
        protected List<SendMessageInfo> sendMessageList = new List<SendMessageInfo>();
        protected List<UserFriendInfo> userFriendList = new List<UserFriendInfo>();

        protected void DeleteReceiveMessage()
        {
            ReceiveMessageBLL.DeleteReceiveMessage(StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("SelectID")), base.UserID);
            ResponseHelper.Write("ok");
            ResponseHelper.End();
        }

        protected void DeleteSendMessage()
        {
            SendMessageBLL.DeleteSendMessage(StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("SelectID")), base.UserID);
            ResponseHelper.Write("ok");
            ResponseHelper.End();
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            this.action = RequestHelper.GetQueryString<string>("Action");
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 8;
            int count = 0;
            switch (this.action)
            {
                case "ReceiveMessage":
                {
                    ReceiveMessageSearchInfo receiveMessage = new ReceiveMessageSearchInfo();
                    receiveMessage.UserID = base.UserID;
                    this.receiveMessageList = ReceiveMessageBLL.SearchReceiveMessageList(queryString, pageSize, receiveMessage, ref count);
                    this.ajaxPagerClass.CurrentPage = queryString;
                    this.ajaxPagerClass.PageSize = pageSize;
                    this.ajaxPagerClass.Count = count;
                    break;
                }
                case "SendMessage":
                {
                    SendMessageSearchInfo sendMessage = new SendMessageSearchInfo();
                    sendMessage.UserID = base.UserID;
                    this.sendMessageList = SendMessageBLL.SearchSendMessageList(queryString, pageSize, sendMessage, ref count);
                    this.ajaxPagerClass.CurrentPage = queryString;
                    this.ajaxPagerClass.PageSize = pageSize;
                    this.ajaxPagerClass.Count = count;
                    break;
                }
                case "WriteMessage":
                {
                    UserFriendSearchInfo userFriend = new UserFriendSearchInfo();
                    userFriend.UserID = base.UserID;
                    this.userFriendList = UserFriendBLL.SearchUserFriendList(userFriend);
                    break;
                }
                case "SearchFriend":
                    this.SearchFriend();
                    break;

                case "SendUserMessage":
                    this.SendUserMessage();
                    break;

                case "DeleteReceiveMessage":
                    this.DeleteReceiveMessage();
                    break;

                case "DeleteSendMessage":
                    this.DeleteSendMessage();
                    break;
            }
        }

        protected void SearchFriend()
        {
            UserFriendSearchInfo userFriend = new UserFriendSearchInfo();
            userFriend.FriendName = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("FriendName"));
            userFriend.UserID = base.UserID;
            List<UserFriendInfo> list = UserFriendBLL.SearchUserFriendList(userFriend);
            string content = string.Empty;
            foreach (UserFriendInfo info2 in list)
            {
                object obj2 = content;
                content = string.Concat(new object[] { obj2, info2.FriendID, "|", info2.FriendName, "||" });
            }
            if (content.Length > 0) content = content.Substring(0, content.Length - 2);
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected void SendUserMessage()
        {
            string content = string.Empty;
            string str2 = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("UserIDList"));
            string str3 = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("UserNameList"));
            string str4 = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("Title"));
            string str5 = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("Content"));
            if (str3 == string.Empty || str4 == string.Empty || str5 == string.Empty)
                content = "请填写完整的信息";
            else
            {
                SendMessageInfo sendMessage = new SendMessageInfo();
                sendMessage.Title = str4;
                sendMessage.Content = str5;
                sendMessage.Date = RequestHelper.DateNow;
                sendMessage.ToUserID = str2;
                sendMessage.ToUserName = str3;
                sendMessage.UserID = base.UserID;
                sendMessage.UserName = base.UserName;
                sendMessage.IsAdmin = 0;
                SendMessageBLL.AddSendMessage(sendMessage);
                string[] strArray = str2.Split(new char[] { ',' });
                string[] strArray2 = str3.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    ReceiveMessageInfo receiveMessage = new ReceiveMessageInfo();
                    receiveMessage.Title = str4;
                    receiveMessage.Content = str5;
                    receiveMessage.Date = RequestHelper.DateNow;
                    receiveMessage.IsRead = 0;
                    receiveMessage.IsAdmin = 0;
                    receiveMessage.FromUserID = base.UserID;
                    receiveMessage.FromUserName = base.UserName;
                    receiveMessage.UserID = Convert.ToInt32(strArray[i]);
                    receiveMessage.UserName = strArray2[i];
                    ReceiveMessageBLL.AddReceiveMessage(receiveMessage);
                }
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }
    }
}

