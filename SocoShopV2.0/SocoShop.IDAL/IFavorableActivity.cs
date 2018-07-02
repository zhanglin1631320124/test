namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IFavorableActivity
    {
        int AddFavorableActivity(FavorableActivityInfo favorableActivity);
        void DeleteFavorableActivity(string strID);
        FavorableActivityInfo ReadFavorableActivity(int id);
        FavorableActivityInfo ReadFavorableActivity(DateTime startDate, DateTime endDate, int id);
        List<FavorableActivityInfo> ReadFavorableActivityList(int currentPage, int pageSize, ref int count);
        void UpdateFavorableActivity(FavorableActivityInfo favorableActivity);
    }
}

