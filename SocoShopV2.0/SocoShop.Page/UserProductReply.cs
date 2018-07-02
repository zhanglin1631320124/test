namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using System;
    using System.Collections.Generic;
    using SocoShop.Entity;

    public class UserProductReply : UserBasePage
    {
        protected CommonPagerClass commonPagerClass = new CommonPagerClass();
        protected List<ProductReplyInfo> productReplyList = new List<ProductReplyInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 20;
            int count = 0;
            this.productReplyList = ProductReplyBLL.ReadProductReplyList(queryString, pageSize, ref count, base.UserID);
            this.commonPagerClass.CurrentPage = queryString;
            this.commonPagerClass.PageSize = pageSize;
            this.commonPagerClass.Count = count;
        }
    }
}

