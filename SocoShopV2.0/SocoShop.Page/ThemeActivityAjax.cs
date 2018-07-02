namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using System;
    using System.Collections.Generic;
    using SocoShop.Entity;

    public class ThemeActivityAjax : AjaxBasePage
    {
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected List<ThemeActivityInfo> themeActivityList = new List<ThemeActivityInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 6;
            int count = 0;
            this.themeActivityList = ThemeActivityBLL.ReadThemeActivityList(queryString, pageSize, ref count);
            this.ajaxPagerClass.CurrentPage = queryString;
            this.ajaxPagerClass.PageSize = pageSize;
            this.ajaxPagerClass.Count = count;
            this.ajaxPagerClass.DisCount = false;
            this.ajaxPagerClass.ListType = false;
        }
    }
}

