namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class Activity : CommonBasePage
    {
        protected List<ActivityPluginsInfo> activityPluginsList = new List<ActivityPluginsInfo>();
        protected FavorableActivityInfo favorableActivity = new FavorableActivityInfo();
        protected List<FavorableActivityInfo> favorableActivityList = new List<FavorableActivityInfo>();
        protected long leftTime = 0;

        protected override void PageLoad()
        {
            base.PageLoad();
            this.favorableActivity = FavorableActivityBLL.ReadFavorableActivity(RequestHelper.DateNow, RequestHelper.DateNow, 0);
            if (this.favorableActivity.ID > 0)
            {
                TimeSpan span = (TimeSpan) (this.favorableActivity.EndDate - RequestHelper.DateNow);
                this.leftTime = span.Days * 0x18 * 0xe10 + span.Hours * 0xe10 + span.Minutes * 60 + span.Seconds;
            }
            else
            {
                int count = -2147483648;
                this.favorableActivityList = FavorableActivityBLL.ReadFavorableActivityList(1, 5, ref count);
            }
            this.activityPluginsList = ActivityPlugins.ReadIsEnabledActivityPluginsList();
            base.Title = "商家活动";
        }
    }
}

