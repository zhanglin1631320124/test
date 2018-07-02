namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;

    public sealed class AdminGroupBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("AdminGroup");
        private static readonly IAdminGroup dal = FactoryHelper.Instance<IAdminGroup>(Global.DataProvider, "AdminGroupDAL");

        public static int AddAdminGroup(AdminGroupInfo adminGroup)
        {
            adminGroup.ID = dal.AddAdminGroup(adminGroup);
            CacheHelper.Remove(cacheKey);
            return adminGroup.ID;
        }

        public static void ChangeAdminGroupCount(int id, ChangeAction action)
        {
            dal.ChangeAdminGroupCount(id, action);
            CacheHelper.Remove(cacheKey);
        }

        public static void ChangeAdminGroupCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeAdminGroupCountByGeneral(strID, action);
            CacheHelper.Remove(cacheKey);
        }

        public static void DeleteAdminGroup(string strID)
        {
            AdminBLL.DeleteAdminByGroupID(strID);
            dal.DeleteAdminGroup(strID);
            CacheHelper.Remove(cacheKey);
        }

        public static AdminGroupInfo ReadAdminGroupCache(int id)
        {
            AdminGroupInfo info = new AdminGroupInfo();
            List<AdminGroupInfo> list = ReadAdminGroupCacheList();
            foreach (AdminGroupInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<AdminGroupInfo> ReadAdminGroupCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadAdminGroupAllList());
            return (List<AdminGroupInfo>) CacheHelper.Read(cacheKey);
        }

        public static void UpdateAdminGroup(AdminGroupInfo adminGroup)
        {
            dal.UpdateAdminGroup(adminGroup);
            CacheHelper.Remove(cacheKey);
        }
    }
}

