namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using SocoShop.Common;

    public sealed class ArticleClassBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("ArticleClass");
        private static readonly IArticleClass dal = FactoryHelper.Instance<IArticleClass>(Global.DataProvider, "ArticleClassDAL");

        public static int AddArticleClass(ArticleClassInfo articleClass)
        {
            articleClass.ID = dal.AddArticleClass(articleClass);
            CacheHelper.Remove(cacheKey);
            return articleClass.ID;
        }

        public static string ArticleClassNameList(string idList)
        {
            string str = string.Empty;
            if (idList != string.Empty) idList = idList.Substring(1, idList.Length - 2);
            idList = idList.Replace("||", "#");
            if (idList.Length > 0)
            {
                foreach (string str2 in idList.Split(new char[] { '#' }))
                {
                    string className = string.Empty;
                    foreach (string str4 in str2.Split(new char[] { '|' }))
                    {
                        if (className == string.Empty)
                            className = ReadArticleClassCache(Convert.ToInt32(str4)).ClassName;
                        else
                            className = className + " > " + ReadArticleClassCache(Convert.ToInt32(str4)).ClassName;
                    }
                    if (className != string.Empty)
                    {
                        if (str == string.Empty)
                            str = className;
                        else
                            str = str + "，" + className;
                    }
                }
            }
            return str;
        }

        public static void DeleteArticleClass(int id)
        {
            dal.DeleteArticleClass(id);
            CacheHelper.Remove(cacheKey);
        }

        public static void MoveDownArticleClass(int id)
        {
            dal.MoveDownArticleClass(id);
            CacheHelper.Remove(cacheKey);
        }

        public static void MoveUpArticleClass(int id)
        {
            dal.MoveUpArticleClass(id);
            CacheHelper.Remove(cacheKey);
        }

        public static ArticleClassInfo ReadArticleClassCache(int id)
        {
            ArticleClassInfo info = new ArticleClassInfo();
            List<ArticleClassInfo> list = ReadArticleClassCacheList();
            foreach (ArticleClassInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<ArticleClassInfo> ReadArticleClassCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadArticleClassAllList());
            return (List<ArticleClassInfo>) CacheHelper.Read(cacheKey);
        }

        public static List<ArticleClassInfo> ReadArticleClassChildList(int fatherID)
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            List<ArticleClassInfo> list2 = ReadArticleClassCacheList();
            foreach (ArticleClassInfo info in list2)
            {
                if (info.FatherID == fatherID) list.Add(info);
            }
            return list;
        }

        private static List<ArticleClassInfo> ReadArticleClassChildList(int fatherID, int depth)
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            List<ArticleClassInfo> list2 = ReadArticleClassCacheList();
            foreach (ArticleClassInfo info in list2)
            {
                if (info.FatherID == fatherID)
                {
                    ArticleClassInfo item = (ArticleClassInfo) ServerHelper.CopyClass(info);
                    string str = string.Empty;
                    for (int i = 1; i < depth; i++)
                    {
                        str = str + HttpContext.Current.Server.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    item.ClassName = str + item.ClassName;
                    list.Add(item);
                    list.AddRange(ReadArticleClassChildList(item.ID, depth + 1));
                }
            }
            return list;
        }

        private static string ReadArticleClassFatherID(int id)
        {
            string str = string.Empty;
            int fatherID = ReadArticleClassCache(id).FatherID;
            if (fatherID > 0) str = ReadArticleClassFatherID(fatherID) + "|" + fatherID;
            return str;
        }

        public static string ReadArticleClassFullFatherID(int id)
        {
            return (ReadArticleClassFatherID(id) + "|" + id.ToString() + "|");
        }

        public static List<ArticleClassInfo> ReadArticleClassNamedList()
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            List<ArticleClassInfo> list2 = ReadArticleClassRootList();
            foreach (ArticleClassInfo info in list2)
            {
                list.Add(info);
                list.AddRange(ReadArticleClassChildList(info.ID, 2));
            }
            return list;
        }

        public static List<ArticleClassInfo> ReadArticleClassRootList()
        {
            List<ArticleClassInfo> list = new List<ArticleClassInfo>();
            List<ArticleClassInfo> list2 = ReadArticleClassCacheList();
            foreach (ArticleClassInfo info in list2)
            {
                if (info.FatherID == 0) list.Add(info);
            }
            return list;
        }

        public static void UpdateArticleClass(ArticleClassInfo articleClass)
        {
            dal.UpdateArticleClass(articleClass);
            CacheHelper.Remove(cacheKey);
        }
    }
}

