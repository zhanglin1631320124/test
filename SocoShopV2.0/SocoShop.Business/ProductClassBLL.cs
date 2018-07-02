namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using SocoShop.Common;

    public sealed class ProductClassBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("ProductClass");
        private static readonly IProductClass dal = FactoryHelper.Instance<IProductClass>(Global.DataProvider, "ProductClassDAL");

        public static int AddProductClass(ProductClassInfo productClass)
        {
            productClass.ID = dal.AddProductClass(productClass);
            CacheHelper.Remove(cacheKey);
            return productClass.ID;
        }

        public static void DeleteProductClass(int id)
        {
            dal.DeleteProductClass(id);
            CacheHelper.Remove(cacheKey);
        }

        public static void DeleteTaobaoProductClass()
        {
            dal.DeleteTaobaoProductClass();
            CacheHelper.Remove(cacheKey);
        }

        public static void MoveDownProductClass(int id)
        {
            dal.MoveDownProductClass(id);
            CacheHelper.Remove(cacheKey);
        }

        public static void MoveUpProductClass(int id)
        {
            dal.MoveUpProductClass(id);
            CacheHelper.Remove(cacheKey);
        }

        public static string ProductClassNameList(string idList)
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
                            className = ReadProductClassCache(Convert.ToInt32(str4)).ClassName;
                        else
                            className = className + " > " + ReadProductClassCache(Convert.ToInt32(str4)).ClassName;
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

        public static ProductClassInfo ReadProductClassCache(int id)
        {
            ProductClassInfo info = new ProductClassInfo();
            List<ProductClassInfo> list = ReadProductClassCacheList();
            foreach (ProductClassInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static ProductClassInfo ReadProductClassCacheByTaobaoID(long taobaoID)
        {
            ProductClassInfo info = new ProductClassInfo();
            List<ProductClassInfo> list = ReadProductClassCacheList();
            foreach (ProductClassInfo info2 in list)
            {
                if (info2.TaobaoID == taobaoID) return info2;
            }
            return info;
        }

        public static List<ProductClassInfo> ReadProductClassCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadProductClassAllList());
            return (List<ProductClassInfo>) CacheHelper.Read(cacheKey);
        }

        public static List<ProductClassInfo> ReadProductClassChildList(int fatherID)
        {
            List<ProductClassInfo> list = new List<ProductClassInfo>();
            List<ProductClassInfo> list2 = ReadProductClassCacheList();
            foreach (ProductClassInfo info in list2)
            {
                if (info.FatherID == fatherID) list.Add(info);
            }
            return list;
        }

        private static List<ProductClassInfo> ReadProductClassChildList(int fatherID, int depth)
        {
            List<ProductClassInfo> list = new List<ProductClassInfo>();
            List<ProductClassInfo> list2 = ReadProductClassCacheList();
            foreach (ProductClassInfo info in list2)
            {
                if (info.FatherID == fatherID)
                {
                    ProductClassInfo item = (ProductClassInfo) ServerHelper.CopyClass(info);
                    string str = string.Empty;
                    for (int i = 1; i < depth; i++)
                    {
                        str = str + HttpContext.Current.Server.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    item.ClassName = str + item.ClassName;
                    list.Add(item);
                    list.AddRange(ReadProductClassChildList(item.ID, depth + 1));
                }
            }
            return list;
        }

        public static List<ProductClassInfo> ReadProductClassNamedList()
        {
            List<ProductClassInfo> list = new List<ProductClassInfo>();
            List<ProductClassInfo> list2 = ReadProductClassRootList();
            foreach (ProductClassInfo info in list2)
            {
                list.Add(info);
                list.AddRange(ReadProductClassChildList(info.ID, 2));
            }
            return list;
        }

        public static List<ProductClassInfo> ReadProductClassRootList()
        {
            List<ProductClassInfo> list = new List<ProductClassInfo>();
            List<ProductClassInfo> list2 = ReadProductClassCacheList();
            foreach (ProductClassInfo info in list2)
            {
                if (info.FatherID == 0) list.Add(info);
            }
            return list;
        }

        public static List<UnlimitClassInfo> ReadProductClassUnlimitClass()
        {
            List<ProductClassInfo> list = ReadProductClassCacheList();
            List<UnlimitClassInfo> list2 = new List<UnlimitClassInfo>();
            foreach (ProductClassInfo info in list)
            {
                UnlimitClassInfo item = new UnlimitClassInfo();
                item.ClassID = info.ID;
                item.ClassName = info.ClassName;
                item.FatherID = info.FatherID;
                list2.Add(item);
            }
            return list2;
        }

        public static string SearchProductClassList(int fatherID)
        {
            string str = string.Empty;
            List<ProductClassInfo> list = ReadProductClassCacheList();
            foreach (ProductClassInfo info in list)
            {
                if (info.FatherID == fatherID)
                {
                    if (str == string.Empty)
                        str = info.ID.ToString() + "," + info.ClassName;
                    else
                    {
                        string str3 = str;
                        str = str3 + "|" + info.ID.ToString() + "," + info.ClassName;
                    }
                }
            }
            return str;
        }

        public static void UpdateProductClass(ProductClassInfo productClass)
        {
            dal.UpdateProductClass(productClass);
            CacheHelper.Remove(cacheKey);
        }

        public static void UpdateProductFatherID(Dictionary<long, int> fatherIDDic)
        {
            dal.UpdateProductFatherID(fatherIDDic);
            CacheHelper.Remove(cacheKey);
        }
    }
}

