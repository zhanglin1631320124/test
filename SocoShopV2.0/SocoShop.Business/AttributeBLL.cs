namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;

    public sealed class AttributeBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("Attribute");
        private static readonly IAttribute dal = FactoryHelper.Instance<IAttribute>(Global.DataProvider, "AttributeDAL");

        public static int AddAttribute(AttributeInfo attribute)
        {
            attribute.ID = dal.AddAttribute(attribute);
            AttributeClassBLL.ChangeAttributeClassCount(attribute.AttributeClassID, ChangeAction.Plus);
            CacheHelper.Remove(cacheKey);
            return attribute.ID;
        }

        public static void ChangeAttributeOrder(ChangeAction action, int id)
        {
            dal.ChangeAttributeOrder(action, id);
            CacheHelper.Remove(cacheKey);
        }

        public static void DeleteAttribute(string strID)
        {
            AttributeClassBLL.ChangeAttributeClassCountByGeneral(strID, ChangeAction.Minus);
            dal.DeleteAttribute(strID);
            CacheHelper.Remove(cacheKey);
        }

        public static void DeleteAttributeByAttributeClassID(string strAttributeClassID)
        {
            dal.DeleteAttributeByAttributeClassID(strAttributeClassID);
            CacheHelper.Remove(cacheKey);
        }

        public static List<AttributeInfo> JoinAttribute(int attributeClassID, int productID)
        {
            List<AttributeRecordInfo> list = AttributeRecordBLL.ReadAttributeRecordByProduct(productID);
            List<AttributeInfo> list2 = ReadAttributeListByClassID(attributeClassID);
            List<AttributeInfo> list3 = new List<AttributeInfo>();
            foreach (AttributeInfo info in list2)
            {
                bool flag = false;
                foreach (AttributeRecordInfo info2 in list)
                {
                    if (info.ID == info2.AttributeID)
                    {
                        AttributeInfo item = new AttributeInfo();
                        item = (AttributeInfo) ServerHelper.CopyClass(info);
                        item.AttributeRecord = info2;
                        flag = true;
                        list3.Add(item);
                        break;
                    }
                }
                if (!flag) list3.Add(info);
            }
            return list3;
        }

        public static AttributeInfo ReadAttributeCache(int id)
        {
            AttributeInfo info = new AttributeInfo();
            List<AttributeInfo> list = ReadAttributeCacheList();
            foreach (AttributeInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<AttributeInfo> ReadAttributeCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadAttributeAllList());
            return (List<AttributeInfo>) CacheHelper.Read(cacheKey);
        }

        public static List<AttributeInfo> ReadAttributeListByClassID(int attributeClassID)
        {
            List<AttributeInfo> list = new List<AttributeInfo>();
            foreach (AttributeInfo info in ReadAttributeCacheList())
            {
                if (info.AttributeClassID == attributeClassID) list.Add(info);
            }
            return list;
        }

        public static void UpdateAttribute(AttributeInfo attribute)
        {
            dal.UpdateAttribute(attribute);
            CacheHelper.Remove(cacheKey);
        }
    }
}

