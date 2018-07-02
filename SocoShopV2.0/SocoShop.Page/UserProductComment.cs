namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class UserProductComment : UserBasePage
    {
        protected CommonPagerClass commonPagerClass = new CommonPagerClass();
        protected List<ProductCommentInfo> productCommentList = new List<ProductCommentInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            ProductCommentSearchInfo productComment = new ProductCommentSearchInfo();
            productComment.UserID = base.UserID;
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 20;
            int count = 0;
            this.productCommentList = ProductCommentBLL.SearchProductCommentList(queryString, pageSize, productComment, ref count);
            this.commonPagerClass.CurrentPage = queryString;
            this.commonPagerClass.PageSize = pageSize;
            this.commonPagerClass.Count = count;
        }
    }
}

