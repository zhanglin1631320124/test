namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SocoShop.Common;

    public sealed class ShippingBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("Shipping");
        private static readonly IShipping dal = FactoryHelper.Instance<IShipping>(Global.DataProvider, "ShippingDAL");

        public static int AddShipping(ShippingInfo shipping)
        {
            shipping.ID = dal.AddShipping(shipping);
            CacheHelper.Remove(cacheKey);
            return shipping.ID;
        }

        public static void ChangeShippingOrder(ChangeAction action, int id)
        {
            dal.ChangeShippingOrder(action, id);
            CacheHelper.Remove(cacheKey);
        }

        public static void DeleteShipping(string strID)
        {
            dal.DeleteShipping(strID);
            CacheHelper.Remove(cacheKey);
        }

        public static ShippingInfo ReadShippingCache(int id)
        {
            ShippingInfo info = new ShippingInfo();
            List<ShippingInfo> list = ReadShippingCacheList();
            foreach (ShippingInfo info2 in list)
            {
                if (info2.ID == id) return info2;
            }
            return info;
        }

        public static List<ShippingInfo> ReadShippingCacheList()
        {
            if (CacheHelper.Read(cacheKey) == null) CacheHelper.Write(cacheKey, dal.ReadShippingAllList());
            return (List<ShippingInfo>) CacheHelper.Read(cacheKey);
        }

        public static List<ShippingInfo> ReadShippingIsEnabledCacheList()
        {
            List<ShippingInfo> list = new List<ShippingInfo>();
            foreach (ShippingInfo info in ReadShippingCacheList())
            {
                if (info.IsEnabled == 1) list.Add(info);
            }
            return list;
        }

        public static void UpdateShipping(ShippingInfo shipping)
        {
            dal.UpdateShipping(shipping);
            CacheHelper.Remove(cacheKey);
        }
    }
}

