namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;

    public sealed class StandardBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("Standard");
        private static readonly IStandard dal = FactoryHelper.Instance<IStandard>(Global.DataProvider, "StandardDAL");
        public static readonly int TableID = UploadTable.ReadTableID("Standard");

        public static int AddStandard(StandardInfo standard)
        {
            standard.ID = dal.AddStandard(standard);
            UploadBLL.UpdateUpload(TableID, 0, standard.ID, Cookies.Admin.GetRandomNumber(false));
            CacheHelper.Remove(cacheKey);
            return standard.ID;
        }

        public static void DeleteStandard(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            dal.DeleteStandard(strID);
            CacheHelper.Remove(cacheKey);
        }

        public static StandardInfo ReadStandardCache(int id)
        {
            StandardInfo info = new StandardInfo();
            List<StandardInfo> list = ReadStandardCacheList();
            foreach (StandardInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<StandardInfo> ReadStandardCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadStandardAllList());
            List<StandardInfo> objClass = (List<StandardInfo>) CacheHelper.Read(cacheKey);
            return (List<StandardInfo>) ServerHelper.CopyClass(objClass);
        }

        public static void UpdateStandard(StandardInfo standard)
        {
            dal.UpdateStandard(standard);
            UploadBLL.UpdateUpload(TableID, 0, standard.ID, Cookies.Admin.GetRandomNumber(false));
            CacheHelper.Remove(cacheKey);
        }
    }
}

