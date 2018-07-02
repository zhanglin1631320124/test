namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class ProductCommentAjax : AjaxBasePage
    {
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected List<ProductCommentInfo> productCommentList = new List<ProductCommentInfo>();
        protected List<UserInfo> userList = new List<UserInfo>();

        public void AddProductComment(ref string result)
        {
            int queryString = RequestHelper.GetQueryString<int>("ProductID");
            string str = CookiesHelper.ReadCookieValue("CommentCookies" + queryString.ToString());
            if (ShopConfig.ReadConfigInfo().CommentRestrictTime > 0 && str != string.Empty)
                result = "请不要频繁提交";
            else
            {
                ProductCommentInfo productComment = new ProductCommentInfo();
                productComment.ProductID = queryString;
                productComment.Title = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("Title"));
                productComment.Content = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("Content"));
                productComment.UserIP = ClientHelper.IP;
                productComment.PostDate = RequestHelper.DateNow;
                productComment.Support = 0;
                productComment.Against = 0;
                productComment.Status = ShopConfig.ReadConfigInfo().CommentDefaultStatus;
                productComment.Rank = RequestHelper.GetQueryString<int>("Rank");
                productComment.ReplyCount = 0;
                productComment.AdminReplyContent = string.Empty;
                productComment.AdminReplyDate = RequestHelper.DateNow;
                productComment.UserID = base.UserID;
                productComment.UserName = base.UserName;
                ProductCommentBLL.AddProductComment(productComment);
                if (ShopConfig.ReadConfigInfo().CommentRestrictTime > 0) CookiesHelper.AddCookie("CommentCookies" + queryString.ToString(), "CommentCookies" + queryString.ToString(), ShopConfig.ReadConfigInfo().CommentRestrictTime, TimeType.Second);
            }
        }

        public void AddTags(ref string result)
        {
            int queryString = RequestHelper.GetQueryString<int>("ProductID");
            string str = CookiesHelper.ReadCookieValue("TagsCookies" + queryString.ToString());
            if (ShopConfig.ReadConfigInfo().AddTagsRestrictTime > 0 && str != string.Empty)
                result = "请不要频繁提交";
            else
            {
                TagsInfo tags = new TagsInfo();
                tags.ProductID = queryString;
                tags.Word = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("Word"));
                tags.Color = "#4C5A62";
                tags.Size = 12;
                tags.IsTop = 0;
                tags.UserID = base.UserID;
                tags.UserName = base.UserName;
                TagsBLL.AddTags(tags);
                if (ShopConfig.ReadConfigInfo().AddTagsRestrictTime > 0) CookiesHelper.AddCookie("TagsCookies" + queryString.ToString(), "TagsCookies" + queryString.ToString(), ShopConfig.ReadConfigInfo().AddTagsRestrictTime, TimeType.Second);
            }
        }

        public void AgainstComment()
        {
            string content = string.Empty;
            if (ShopConfig.ReadConfigInfo().AllowAnonymousCommentOperate == 0 && base.UserID == 0)
                content = "还未登录";
            else
            {
                int queryString = RequestHelper.GetQueryString<int>("CommentID");
                string str2 = CookiesHelper.ReadCookieValue("CommentOperateCookies" + queryString.ToString());
                if (ShopConfig.ReadConfigInfo().CommentOperateRestrictTime > 0 && str2 != string.Empty)
                    content = "请不要频繁提交";
                else
                {
                    ProductCommentBLL.ChangeProductCommentAgainstCount(queryString.ToString(), ChangeAction.Plus);
                    if (ShopConfig.ReadConfigInfo().CommentOperateRestrictTime > 0) CookiesHelper.AddCookie("CommentOperateCookies" + queryString.ToString(), "CommentOperateCookies" + queryString.ToString(), ShopConfig.ReadConfigInfo().CommentOperateRestrictTime, TimeType.Second);
                    content = "ok";
                }
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            string queryString = RequestHelper.GetQueryString<string>("Action");
            if (queryString != null)
            {
                if (!(queryString == "Add"))
                {
                    if (queryString == "Against")
                        this.AgainstComment();
                    else if (queryString == "Support")
                        this.SupportComment();
                    else if (queryString == "AddTags") this.PostTags();
                }
                else
                    this.PostProductComment();
            }
            int currentPage = RequestHelper.GetQueryString<int>("Page");
            if (currentPage < 1) currentPage = 1;
            int pageSize = 8;
            int count = 0;
            ProductCommentSearchInfo productComment = new ProductCommentSearchInfo();
            productComment.ProductID = RequestHelper.GetQueryString<int>("ProductID");
            productComment.Status = 2;
            this.productCommentList = ProductCommentBLL.SearchProductCommentList(currentPage, pageSize, productComment, ref count);
            this.ajaxPagerClass.CurrentPage = currentPage;
            this.ajaxPagerClass.PageSize = pageSize;
            this.ajaxPagerClass.Count = count;
            this.ajaxPagerClass.DisCount = false;
            this.ajaxPagerClass.ListType = false;
            string str2 = string.Empty;
            foreach (ProductCommentInfo info2 in this.productCommentList)
            {
                if (str2 == string.Empty)
                    str2 = info2.UserID.ToString();
                else
                    str2 = str2 + "," + info2.UserID.ToString();
            }
            if (str2 != string.Empty)
            {
                UserSearchInfo user = new UserSearchInfo();
                user.InUserID = str2;
                this.userList = UserBLL.SearchUserList(user);
            }
        }

        public void PostProductComment()
        {
            string result = "ok";
            if (ShopConfig.ReadConfigInfo().AllowAnonymousComment == 0 && base.UserID == 0)
                result = "还未登录";
            else
                this.AddProductComment(ref result);
            ResponseHelper.Write(result);
            ResponseHelper.End();
        }

        public void PostTags()
        {
            string result = "ok";
            if (ShopConfig.ReadConfigInfo().AllowAnonymousAddTags == 0 && base.UserID == 0)
                result = "还未登录";
            else
                this.AddTags(ref result);
            ResponseHelper.Write(result);
            ResponseHelper.End();
        }

        public void SupportComment()
        {
            string content = string.Empty;
            if (ShopConfig.ReadConfigInfo().AllowAnonymousCommentOperate == 0 && base.UserID == 0)
                content = "还未登录";
            else
            {
                int queryString = RequestHelper.GetQueryString<int>("CommentID");
                string str2 = CookiesHelper.ReadCookieValue("CommentOperateCookies" + queryString.ToString());
                if (ShopConfig.ReadConfigInfo().CommentOperateRestrictTime > 0 && str2 != string.Empty)
                    content = "请不要频繁提交";
                else
                {
                    ProductCommentBLL.ChangeProductCommentSupportCount(queryString.ToString(), ChangeAction.Plus);
                    if (ShopConfig.ReadConfigInfo().CommentOperateRestrictTime > 0) CookiesHelper.AddCookie("CommentOperateCookies" + queryString.ToString(), "CommentOperateCookies" + queryString.ToString(), ShopConfig.ReadConfigInfo().CommentOperateRestrictTime, TimeType.Second);
                    content = "ok";
                }
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }
    }
}

