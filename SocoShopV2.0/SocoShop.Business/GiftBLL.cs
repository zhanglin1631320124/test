namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;

    public sealed class GiftBLL
    {
        private static readonly IGift dal = FactoryHelper.Instance<IGift>(Global.DataProvider, "GiftDAL");
        public static readonly int TableID = UploadTable.ReadTableID("Gift");

        public static int AddGift(GiftInfo gift)
        {
            gift.ID = dal.AddGift(gift);
            UploadBLL.UpdateUpload(TableID, 0, gift.ID, Cookies.Admin.GetRandomNumber(false));
            return gift.ID;
        }

        public static void DeleteGift(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            dal.DeleteGift(strID);
        }

        public static GiftInfo ReadGift(int id)
        {
            return dal.ReadGift(id);
        }

        public static List<GiftInfo> SearchGiftList(GiftSearchInfo gift)
        {
            return dal.SearchGiftList(gift);
        }

        public static List<GiftInfo> SearchGiftList(int currentPage, int pageSize, GiftSearchInfo gift, ref int count)
        {
            return dal.SearchGiftList(currentPage, pageSize, gift, ref count);
        }

        public static void UpdateGift(GiftInfo gift)
        {
            dal.UpdateGift(gift);
            UploadBLL.UpdateUpload(TableID, 0, gift.ID, Cookies.Admin.GetRandomNumber(false));
        }
    }
}

