namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;

    public sealed class FlashPhotoBLL
    {
        private static readonly IFlashPhoto dal = FactoryHelper.Instance<IFlashPhoto>(Global.DataProvider, "FlashPhotoDAL");
        public static readonly int TableID = UploadTable.ReadTableID("FlashPhoto");

        public static int AddFlashPhoto(FlashPhotoInfo flashPhoto)
        {
            flashPhoto.ID = dal.AddFlashPhoto(flashPhoto);
            UploadBLL.UpdateUpload(TableID, flashPhoto.FlashID, flashPhoto.ID, Cookies.Admin.GetRandomNumber(false));
            FlashBLL.ChangeFlashCount(flashPhoto.FlashID, ChangeAction.Plus);
            FlashBLL.RebuildFile(flashPhoto.FlashID);
            return flashPhoto.ID;
        }

        public static void ChangeFlashPhotoOrder(ChangeAction action, int id, int flashID)
        {
            dal.ChangeFlashPhotoOrder(action, id);
            FlashBLL.RebuildFile(flashID);
        }

        public static void DeleteFlashPhoto(string strID, int flashID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            FlashBLL.ChangeFlashCountByGeneral(strID, ChangeAction.Minus);
            dal.DeleteFlashPhoto(strID);
            FlashBLL.RebuildFile(flashID);
        }

        public static void DeleteFlashPhotoByFlashID(string strFlashID)
        {
            dal.DeleteFlashPhotoByFlashID(strFlashID);
        }

        public static FlashPhotoInfo ReadFlashPhoto(int id)
        {
            return dal.ReadFlashPhoto(id);
        }

        public static List<FlashPhotoInfo> ReadFlashPhotoByFlash(int flashID)
        {
            return dal.ReadFlashPhotoByFlash(flashID);
        }

        public static void UpdateFlashPhoto(FlashPhotoInfo flashPhoto)
        {
            dal.UpdateFlashPhoto(flashPhoto);
            UploadBLL.UpdateUpload(TableID, flashPhoto.FlashID, flashPhoto.ID, Cookies.Admin.GetRandomNumber(false));
            FlashBLL.RebuildFile(flashPhoto.FlashID);
        }
    }
}

