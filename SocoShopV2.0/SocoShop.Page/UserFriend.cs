namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class UserFriend : UserBasePage
    {
        protected CommonPagerClass commonPagerClass = new CommonPagerClass();
        protected List<UserFriendInfo> userFriendList = new List<UserFriendInfo>();
        protected List<UserInfo> userList = new List<UserInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            string queryString = RequestHelper.GetQueryString<string>("Action");
            if (queryString != null && queryString == "Delete")
            {
                UserFriendBLL.DeleteUserFriend(RequestHelper.GetQueryString<string>("ID"), base.UserID);
                ResponseHelper.Write("ok");
                ResponseHelper.End();
            }
            int currentPage = RequestHelper.GetQueryString<int>("Page");
            if (currentPage < 1) currentPage = 1;
            int pageSize = 0x19;
            int count = 0;
            UserFriendSearchInfo userFriend = new UserFriendSearchInfo();
            userFriend.UserID = base.UserID;
            this.userFriendList = UserFriendBLL.SearchUserFriendList(currentPage, pageSize, userFriend, ref count);
            this.commonPagerClass.CurrentPage = currentPage;
            this.commonPagerClass.PageSize = pageSize;
            this.commonPagerClass.Count = count;
            string str3 = string.Empty;
            foreach (UserFriendInfo info2 in this.userFriendList)
            {
                if (str3 == string.Empty)
                    str3 = info2.FriendID.ToString();
                else
                    str3 = str3 + "," + info2.FriendID.ToString();
            }
            if (str3 != string.Empty)
            {
                UserSearchInfo user = new UserSearchInfo();
                user.InUserID = str3;
                this.userList = UserBLL.SearchUserList(user);
            }
        }
    }
}

