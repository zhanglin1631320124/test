namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class ProductReplyAjax : AjaxBasePage
    {
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected List<ProductReplyInfo> productReplyList = new List<ProductReplyInfo>();
        protected List<UserInfo> userList = new List<UserInfo>();

        public void AddProductReply(ref string result)
        {
            int queryString = RequestHelper.GetQueryString<int>("ProductID");
            int num2 = RequestHelper.GetQueryString<int>("CommentID");
            string str = CookiesHelper.ReadCookieValue("ReplytCookies" + num2.ToString());
            if (ShopConfig.ReadConfigInfo().ReplyRestrictTime > 0 && str != string.Empty)
                result = "请不要频繁提交";
            else
            {
                ProductReplyInfo productReply = new ProductReplyInfo();
                productReply.ProductID = queryString;
                productReply.CommentID = num2;
                productReply.Content = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("Content"));
                productReply.UserIP = ClientHelper.IP;
                productReply.PostDate = RequestHelper.DateNow;
                productReply.UserID = base.UserID;
                productReply.UserName = base.UserName;
                ProductReplyBLL.AddProductReply(productReply);
                if (ShopConfig.ReadConfigInfo().ReplyRestrictTime > 0) CookiesHelper.AddCookie("ReplytCookies" + num2.ToString(), "ReplytCookies" + num2.ToString(), ShopConfig.ReadConfigInfo().ReplyRestrictTime, TimeType.Second);
            }
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            string queryString = RequestHelper.GetQueryString<string>("Action");
            if (queryString != null && queryString == "Add") this.PostProductReply();
            int currentPage = RequestHelper.GetQueryString<int>("Page");
            if (currentPage < 1) currentPage = 1;
            int pageSize = 8;
            int count = 0;
            int commentID = RequestHelper.GetQueryString<int>("CommentID");
            this.productReplyList = ProductReplyBLL.ReadProductReplyList(commentID, currentPage, pageSize, ref count, -2147483648);
            this.ajaxPagerClass.CurrentPage = currentPage;
            this.ajaxPagerClass.PageSize = pageSize;
            this.ajaxPagerClass.Count = count;
            this.ajaxPagerClass.DisCount = false;
            this.ajaxPagerClass.ListType = false;
            string str2 = string.Empty;
            foreach (ProductReplyInfo info in this.productReplyList)
            {
                if (str2 == string.Empty)
                    str2 = info.UserID.ToString();
                else
                    str2 = str2 + "," + info.UserID.ToString();
            }
            if (str2 != string.Empty)
            {
                UserSearchInfo user = new UserSearchInfo();
                user.InUserID = str2;
                this.userList = UserBLL.SearchUserList(user);
            }
        }

        public void PostProductReply()
        {
            string result = "ok";
            if (ShopConfig.ReadConfigInfo().AllowAnonymousReply == 0 && base.UserID == 0)
                result = "还未登录";
            else
                this.AddProductReply(ref result);
            ResponseHelper.Write(result);
            ResponseHelper.End();
        }
    }
}

