namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using SocoShop.Common;

    public sealed class RegionBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("Region");
        private static readonly IRegion dal = FactoryHelper.Instance<IRegion>(Global.DataProvider, "RegionDAL");

        public static int AddRegion(RegionInfo region)
        {
            region.ID = dal.AddRegion(region);
            CacheHelper.Remove(cacheKey);
            return region.ID;
        }

        public static void DeleteRegion(int id)
        {
            dal.DeleteRegion(id);
            CacheHelper.Remove(cacheKey);
        }

        public static void MoveDownRegion(int id)
        {
            dal.MoveDownRegion(id);
            CacheHelper.Remove(cacheKey);
        }

        public static void MoveUpRegion(int id)
        {
            dal.MoveUpRegion(id);
            CacheHelper.Remove(cacheKey);
        }

        public static RegionInfo ReadRegionCache(int id)
        {
            RegionInfo info = new RegionInfo();
            List<RegionInfo> list = ReadRegionCacheList();
            foreach (RegionInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<RegionInfo> ReadRegionCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadRegionAllList());
            return (List<RegionInfo>) CacheHelper.Read(cacheKey);
        }

        public static List<RegionInfo> ReadRegionChildList(int fatherID)
        {
            List<RegionInfo> list = new List<RegionInfo>();
            List<RegionInfo> list2 = ReadRegionCacheList();
            foreach (RegionInfo info in list2)
            {
                if (info.FatherID == fatherID) list.Add(info);
            }
            return list;
        }

        private static List<RegionInfo> ReadRegionChildList(int fatherID, int depth)
        {
            List<RegionInfo> list = new List<RegionInfo>();
            List<RegionInfo> list2 = ReadRegionCacheList();
            foreach (RegionInfo info in list2)
            {
                if (info.FatherID == fatherID)
                {
                    RegionInfo item = (RegionInfo) ServerHelper.CopyClass(info);
                    string str = string.Empty;
                    for (int i = 1; i < depth; i++)
                    {
                        str = str + HttpContext.Current.Server.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    item.RegionName = str + item.RegionName;
                    list.Add(item);
                    list.AddRange(ReadRegionChildList(item.ID, depth + 1));
                }
            }
            return list;
        }

        public static List<RegionInfo> ReadRegionList(string strID)
        {
            strID = "," + strID + ",";
            List<RegionInfo> list = new List<RegionInfo>();
            List<RegionInfo> list2 = ReadRegionCacheList();
            foreach (RegionInfo info in list2)
            {
                if (strID.IndexOf("," + info.ID + ",") > -1) list.Add(info);
            }
            return list;
        }

        public static List<RegionInfo> ReadRegionNamedList()
        {
            List<RegionInfo> list = new List<RegionInfo>();
            List<RegionInfo> list2 = ReadRegionRootList();
            foreach (RegionInfo info in list2)
            {
                list.Add(info);
                list.AddRange(ReadRegionChildList(info.ID, 2));
            }
            return list;
        }

        public static List<RegionInfo> ReadRegionRootList()
        {
            List<RegionInfo> list = new List<RegionInfo>();
            List<RegionInfo> list2 = ReadRegionCacheList();
            foreach (RegionInfo info in list2)
            {
                if (info.FatherID == 0) list.Add(info);
            }
            return list;
        }

        public static List<UnlimitClassInfo> ReadRegionUnlimitClass()
        {
            List<RegionInfo> list = ReadRegionCacheList();
            List<UnlimitClassInfo> list2 = new List<UnlimitClassInfo>();
            foreach (RegionInfo info in list)
            {
                UnlimitClassInfo item = new UnlimitClassInfo();
                item.ClassID = info.ID;
                item.ClassName = info.RegionName;
                item.FatherID = info.FatherID;
                list2.Add(item);
            }
            return list2;
        }

        public static string RegionNameList(string idList)
        {
            string str = string.Empty;
            if (idList != string.Empty) idList = idList.Substring(1, idList.Length - 2);
            idList = idList.Replace("||", "#");
            if (idList.Length > 0)
            {
                foreach (string str2 in idList.Split(new char[] { '#' }))
                {
                    string regionName = string.Empty;
                    foreach (string str4 in str2.Split(new char[] { '|' }))
                    {
                        if (regionName == string.Empty)
                            regionName = ReadRegionCache(Convert.ToInt32(str4)).RegionName;
                        else
                            regionName = regionName + " > " + ReadRegionCache(Convert.ToInt32(str4)).RegionName;
                    }
                    if (regionName != string.Empty)
                    {
                        if (str == string.Empty)
                            str = regionName;
                        else
                            str = str + "，" + regionName;
                    }
                }
            }
            return str;
        }

        public static string SearchRegionList(int fatherID)
        {
            string str = string.Empty;
            List<RegionInfo> list = ReadRegionCacheList();
            foreach (RegionInfo info in list)
            {
                if (info.FatherID == fatherID)
                {
                    if (str == string.Empty)
                        str = info.ID.ToString() + "," + info.RegionName;
                    else
                    {
                        string str3 = str;
                        str = str3 + "|" + info.ID.ToString() + "," + info.RegionName;
                    }
                }
            }
            return str;
        }

        public static void UpdateRegion(RegionInfo region)
        {
            dal.UpdateRegion(region);
            CacheHelper.Remove(cacheKey);
        }
    }
}

