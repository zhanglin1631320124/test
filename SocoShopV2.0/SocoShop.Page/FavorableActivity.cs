namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using System;
    using System.Collections.Generic;
    using SocoShop.Entity;

    public class FavorableActivity : CommonBasePage
    {
        protected CommonPagerClass commonPagerClass = new CommonPagerClass();
        protected List<FavorableActivityInfo> favorableActivityList = new List<FavorableActivityInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int num2 = 10;
            int count = 0;
            this.favorableActivityList = FavorableActivityBLL.ReadFavorableActivityList(1, 5, ref count);
            this.commonPagerClass.CurrentPage = queryString;
            this.commonPagerClass.PageSize = num2;
            this.commonPagerClass.Count = count;
            base.Title = "优惠活动";
        }
    }
}

