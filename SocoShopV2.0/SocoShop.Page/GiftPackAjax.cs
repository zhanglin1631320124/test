namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using System;
    using System.Collections.Generic;
    using SocoShop.Entity;

    public class GiftPackAjax : AjaxBasePage
    {
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected List<GiftPackInfo> giftPackList = new List<GiftPackInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 6;
            int count = 0;
            this.giftPackList = GiftPackBLL.ReadGiftPackList(queryString, pageSize, ref count);
            this.ajaxPagerClass.CurrentPage = queryString;
            this.ajaxPagerClass.PageSize = pageSize;
            this.ajaxPagerClass.Count = count;
            this.ajaxPagerClass.DisCount = false;
            this.ajaxPagerClass.ListType = false;
        }
    }
}

