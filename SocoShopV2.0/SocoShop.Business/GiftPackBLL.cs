namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;

    public sealed class GiftPackBLL
    {
        private static readonly IGiftPack dal = FactoryHelper.Instance<IGiftPack>(Global.DataProvider, "GiftPackDAL");
        public static readonly int TableID = UploadTable.ReadTableID("GiftPack");

        public static int AddGiftPack(GiftPackInfo giftPack)
        {
            giftPack.ID = dal.AddGiftPack(giftPack);
            UploadBLL.UpdateUpload(TableID, 0, giftPack.ID, Cookies.Admin.GetRandomNumber(false));
            return giftPack.ID;
        }

        public static void DeleteGiftPack(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            dal.DeleteGiftPack(strID);
        }

        public static GiftPackInfo ReadGiftPack(int id)
        {
            return dal.ReadGiftPack(id);
        }

        public static List<GiftPackInfo> ReadGiftPackList(int currentPage, int pageSize, ref int count)
        {
            return dal.ReadGiftPackList(currentPage, pageSize, ref count);
        }

        public static void UpdateGiftPack(GiftPackInfo giftPack)
        {
            dal.UpdateGiftPack(giftPack);
            UploadBLL.UpdateUpload(TableID, 0, giftPack.ID, Cookies.Admin.GetRandomNumber(false));
        }
    }
}

