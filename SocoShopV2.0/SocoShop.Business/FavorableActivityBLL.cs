namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;

    public sealed class FavorableActivityBLL
    {
        private static readonly IFavorableActivity dal = FactoryHelper.Instance<IFavorableActivity>(Global.DataProvider, "FavorableActivityDAL");
        public static readonly int TableID = UploadTable.ReadTableID("FavorableActivity");

        public static int AddFavorableActivity(FavorableActivityInfo favorableActivity)
        {
            favorableActivity.ID = dal.AddFavorableActivity(favorableActivity);
            UploadBLL.UpdateUpload(TableID, 0, favorableActivity.ID, Cookies.Admin.GetRandomNumber(false));
            return favorableActivity.ID;
        }

        public static void DeleteFavorableActivity(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            dal.DeleteFavorableActivity(strID);
        }

        public static FavorableActivityInfo ReadFavorableActivity(int id)
        {
            return dal.ReadFavorableActivity(id);
        }

        public static FavorableActivityInfo ReadFavorableActivity(DateTime startDate, DateTime endDate, int id)
        {
            return dal.ReadFavorableActivity(startDate, endDate, id);
        }

        public static List<FavorableActivityInfo> ReadFavorableActivityList(int currentPage, int pageSize, ref int count)
        {
            return dal.ReadFavorableActivityList(currentPage, pageSize, ref count);
        }

        public static void UpdateFavorableActivity(FavorableActivityInfo favorableActivity)
        {
            dal.UpdateFavorableActivity(favorableActivity);
            UploadBLL.UpdateUpload(TableID, 0, favorableActivity.ID, Cookies.Admin.GetRandomNumber(false));
        }
    }
}

