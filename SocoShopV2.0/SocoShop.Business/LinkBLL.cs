namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;

    public sealed class LinkBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("Link");
        private static readonly ILink dal = FactoryHelper.Instance<ILink>(Global.DataProvider, "LinkDAL");
        public static readonly int TableID = UploadTable.ReadTableID("Link");

        public static int AddLink(LinkInfo link)
        {
            link.ID = dal.AddLink(link);
            UploadBLL.UpdateUpload(TableID, 0, link.ID, Cookies.Admin.GetRandomNumber(false));
            CacheHelper.Remove(cacheKey);
            return link.ID;
        }

        public static void ChangeLinkOrder(ChangeAction action, int id)
        {
            dal.ChangeLinkOrder(action, id);
            CacheHelper.Remove(cacheKey);
        }

        public static void DeleteLink(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            dal.DeleteLink(strID);
            CacheHelper.Remove(cacheKey);
        }

        public static LinkInfo ReadLinkCache(int id)
        {
            LinkInfo info = new LinkInfo();
            List<LinkInfo> list = ReadLinkCacheList();
            foreach (LinkInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<LinkInfo> ReadLinkCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadLinkAllList());
            return (List<LinkInfo>) CacheHelper.Read(cacheKey);
        }

        public static List<LinkInfo> ReadLinkCacheListByClass(int classID)
        {
            List<LinkInfo> list = new List<LinkInfo>();
            List<LinkInfo> list2 = ReadLinkCacheList();
            foreach (LinkInfo info in list2)
            {
                if (info.LinkClass == classID) list.Add(info);
            }
            return list;
        }

        public static string ReadLinkDisplay(object display, object linkClass)
        {
            string str = display.ToString();
            if (Convert.ToInt32(linkClass) == 2) str = "<img src=\"" + str + "\" width=\"88\" height=\"31\"/>";
            return str;
        }

        public static void UpdateLink(LinkInfo link)
        {
            dal.UpdateLink(link);
            UploadBLL.UpdateUpload(TableID, 0, link.ID, Cookies.Admin.GetRandomNumber(false));
            CacheHelper.Remove(cacheKey);
        }
    }
}

