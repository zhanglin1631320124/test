namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class AdRecordBLL
    {
        private static readonly IAdRecord dal = FactoryHelper.Instance<IAdRecord>(Global.DataProvider, "AdRecordDAL");

        public static int AddAdRecord(AdRecordInfo adRecord)
        {
            adRecord.ID = dal.AddAdRecord(adRecord);
            AdBLL.ChangeAdCount(adRecord.AdID, ChangeAction.Plus);
            return adRecord.ID;
        }

        public static void DeleteAdRecord(string strID, int userID)
        {
            AdBLL.ChangeAdCountByGeneral(strID, ChangeAction.Minus);
            dal.DeleteAdRecord(strID, userID);
        }

        public static void DeleteAdRecordByAdID(string strAdID)
        {
            dal.DeleteAdRecordByAdID(strAdID);
        }

        public static AdRecordInfo ReadAdRecord(int id, int userID)
        {
            return dal.ReadAdRecord(id, userID);
        }

        public static List<AdRecordInfo> ReadAdRecordList(int currentPage, int pageSize, ref int count, int userID)
        {
            return dal.ReadAdRecordList(currentPage, pageSize, ref count, userID);
        }

        public static List<AdRecordInfo> ReadAdRecordList(int adID, int currentPage, int pageSize, ref int count, int userID)
        {
            return dal.ReadAdRecordList(adID, currentPage, pageSize, ref count, userID);
        }

        public static void UpdateAdRecord(AdRecordInfo adRecord)
        {
            AdRecordInfo info = ReadAdRecord(adRecord.ID, 0);
            dal.UpdateAdRecord(adRecord);
            if (adRecord.AdID != info.AdID)
            {
                AdBLL.ChangeAdCount(info.AdID, ChangeAction.Minus);
                AdBLL.ChangeAdCount(adRecord.AdID, ChangeAction.Plus);
            }
        }
    }
}

