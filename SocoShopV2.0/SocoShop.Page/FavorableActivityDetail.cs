namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class FavorableActivityDetail : CommonBasePage
    {
        protected string buyClasss = string.Empty;
        protected FavorableActivityInfo favorableActivity = new FavorableActivityInfo();
        protected List<GiftInfo> giftList = new List<GiftInfo>();
        protected string userGrade = string.Empty;

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.favorableActivity = FavorableActivityBLL.ReadFavorableActivity(queryString);
            if (this.favorableActivity.UserGrade != string.Empty)
            {
                foreach (string str in this.favorableActivity.UserGrade.Split(new char[] { ',' }))
                {
                    if (this.userGrade == string.Empty)
                        this.userGrade = UserGradeBLL.ReadUserGradeCache(Convert.ToInt32(str)).Name;
                    else
                        this.userGrade = this.userGrade + "," + UserGradeBLL.ReadUserGradeCache(Convert.ToInt32(str)).Name;
                }
            }
            if (this.favorableActivity.GiftID != string.Empty)
            {
                GiftSearchInfo gift = new GiftSearchInfo();
                gift.InGiftID = this.favorableActivity.GiftID;
                this.giftList = GiftBLL.SearchGiftList(gift);
            }
            base.Title = this.favorableActivity.Name;
        }
    }
}

