namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Web;

    public sealed class AdBLL
    {
        private static readonly IAd dal = FactoryHelper.Instance<IAd>(Global.DataProvider, "AdDAL");
        public static readonly int TableID = UploadTable.ReadTableID("Ad");

        public static int AddAd(AdInfo ad)
        {
            ad.ID = dal.AddAd(ad);
            UploadBLL.UpdateUpload(TableID, 0, ad.ID, Cookies.Admin.GetRandomNumber(false));
            CreateAdFile(ad);
            return ad.ID;
        }

        public static void ChangeAdCount(int id, ChangeAction action)
        {
            dal.ChangeAdCount(id, action);
        }

        public static void ChangeAdCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeAdCountByGeneral(strID, action);
        }

        public static void CreateAdFile(AdInfo ad)
        {
            AdClass class2 = new AdClass();
            class2.AdType = (AdType) ad.AdClass;
            class2.Display = ad.Display;
            class2.EndDate = ad.EndDate;
            class2.FileName = ServerHelper.MapPath(ShopCommon.GetAdFile(ad.ID.ToString()));
            class2.Height = ad.Height;
            class2.IsEnabled = ad.IsEnabled;
            class2.StartDate = ad.StartDate;
            class2.Title = ad.Title;
            class2.Url = "/Ad.aspx?AdID=" + ad.ID.ToString() + "&URL=" + HttpContext.Current.Server.UrlEncode(ad.Url);
            class2.Width = ad.Width;
            class2.MakeAdFile();
        }

        public static void DeleteAd(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            AdRecordBLL.DeleteAdRecordByAdID(strID);
            dal.DeleteAd(strID);
        }

        public static AdInfo ReadAd(int id)
        {
            return dal.ReadAd(id);
        }

        public static List<AdInfo> ReadAdList(int currentPage, int pageSize, ref int count)
        {
            return dal.ReadAdList(currentPage, pageSize, ref count);
        }

        public static List<AdInfo> ReadAdList(int classID, int currentPage, int pageSize, ref int count)
        {
            return dal.ReadAdList(classID, currentPage, pageSize, ref count);
        }

        public static void UpdateAd(AdInfo ad)
        {
            dal.UpdateAd(ad);
            UploadBLL.UpdateUpload(TableID, 0, ad.ID, Cookies.Admin.GetRandomNumber(false));
            CreateAdFile(ad);
        }
    }
}

