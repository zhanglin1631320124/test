namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;

    public sealed class AttributeClassBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("AttributeClass");
        private static readonly IAttributeClass dal = FactoryHelper.Instance<IAttributeClass>(Global.DataProvider, "AttributeClassDAL");

        public static int AddAttributeClass(AttributeClassInfo attributeClass)
        {
            attributeClass.ID = dal.AddAttributeClass(attributeClass);
            CacheHelper.Remove(cacheKey);
            return attributeClass.ID;
        }

        public static void ChangeAttributeClassCount(int id, ChangeAction action)
        {
            dal.ChangeAttributeClassCount(id, action);
            CacheHelper.Remove(cacheKey);
        }

        public static void ChangeAttributeClassCountByGeneral(string strID, ChangeAction action)
        {
            dal.ChangeAttributeClassCountByGeneral(strID, action);
            CacheHelper.Remove(cacheKey);
        }

        public static void DeleteAttributeClass(string strID)
        {
            AttributeBLL.DeleteAttributeByAttributeClassID(strID);
            dal.DeleteAttributeClass(strID);
            CacheHelper.Remove(cacheKey);
        }

        public static AttributeClassInfo ReadAttributeClassCache(int id)
        {
            AttributeClassInfo info = new AttributeClassInfo();
            List<AttributeClassInfo> list = ReadAttributeClassCacheList();
            foreach (AttributeClassInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<AttributeClassInfo> ReadAttributeClassCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadAttributeClassAllList());
            return (List<AttributeClassInfo>) CacheHelper.Read(cacheKey);
        }

        public static void UpdateAttributeClass(AttributeClassInfo attributeClass)
        {
            dal.UpdateAttributeClass(attributeClass);
            CacheHelper.Remove(cacheKey);
        }
    }
}

