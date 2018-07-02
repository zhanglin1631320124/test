namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using SocoShop.Common;

    public sealed class MenuBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("Menu");
        private static readonly IMenu dal = FactoryHelper.Instance<IMenu>(Global.DataProvider, "MenuDAL");

        public static int AddMenu(MenuInfo menu)
        {
            menu.ID = dal.AddMenu(menu);
            CacheHelper.Remove(cacheKey);
            return menu.ID;
        }

        public static void DeleteMenu(int id)
        {
            dal.DeleteMenu(id);
            CacheHelper.Remove(cacheKey);
        }

        public static void MoveDownMenu(int id)
        {
            dal.MoveDownMenu(id);
            CacheHelper.Remove(cacheKey);
        }

        public static void MoveUpMenu(int id)
        {
            dal.MoveUpMenu(id);
            CacheHelper.Remove(cacheKey);
        }

        public static List<MenuInfo> ReadMenuAllNamedChildList(int fatherID)
        {
            List<MenuInfo> list = new List<MenuInfo>();
            List<MenuInfo> list2 = ReadMenuNamedList();
            bool flag = false;
            foreach (MenuInfo info in list2)
            {
                if (info.FatherID == fatherID) flag = true;
                if (info.FatherID == 0) flag = false;
                if (flag) list.Add(info);
            }
            return list;
        }

        public static MenuInfo ReadMenuCache(int id)
        {
            MenuInfo info = new MenuInfo();
            List<MenuInfo> list = ReadMenuCacheList();
            foreach (MenuInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<MenuInfo> ReadMenuCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadMenuAllList());
            return (List<MenuInfo>) CacheHelper.Read(cacheKey);
        }

        public static List<MenuInfo> ReadMenuChildList(int fatherID)
        {
            List<MenuInfo> list = new List<MenuInfo>();
            List<MenuInfo> list2 = ReadMenuCacheList();
            foreach (MenuInfo info in list2)
            {
                if (info.FatherID == fatherID) list.Add(info);
            }
            return list;
        }

        private static List<MenuInfo> ReadMenuChildList(int fatherID, int depth)
        {
            List<MenuInfo> list = new List<MenuInfo>();
            List<MenuInfo> list2 = ReadMenuCacheList();
            foreach (MenuInfo info in list2)
            {
                if (info.FatherID == fatherID)
                {
                    MenuInfo item = (MenuInfo) ServerHelper.CopyClass(info);
                    string str = string.Empty;
                    for (int i = 1; i < depth; i++)
                    {
                        str = str + HttpContext.Current.Server.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    item.MenuName = str + item.MenuName;
                    list.Add(item);
                    list.AddRange(ReadMenuChildList(item.ID, depth + 1));
                }
            }
            return list;
        }

        public static List<MenuInfo> ReadMenuNamedList()
        {
            List<MenuInfo> list = new List<MenuInfo>();
            List<MenuInfo> list2 = ReadMenuRootList();
            foreach (MenuInfo info in list2)
            {
                list.Add(info);
                list.AddRange(ReadMenuChildList(info.ID, 2));
            }
            return list;
        }

        public static List<MenuInfo> ReadMenuRootList()
        {
            List<MenuInfo> list = new List<MenuInfo>();
            List<MenuInfo> list2 = ReadMenuCacheList();
            foreach (MenuInfo info in list2)
            {
                if (info.FatherID == 0) list.Add(info);
            }
            return list;
        }

        public static void UpdateMenu(MenuInfo menu)
        {
            dal.UpdateMenu(menu);
            CacheHelper.Remove(cacheKey);
        }
    }
}

