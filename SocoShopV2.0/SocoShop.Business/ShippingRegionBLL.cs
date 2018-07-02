namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class ShippingRegionBLL
    {
        private static readonly IShippingRegion dal = FactoryHelper.Instance<IShippingRegion>(Global.DataProvider, "ShippingRegionDAL");

        public static int AddShippingRegion(ShippingRegionInfo shippingRegion)
        {
            shippingRegion.ID = dal.AddShippingRegion(shippingRegion);
            return shippingRegion.ID;
        }

        public static void DeleteShippingRegion(string strID)
        {
            dal.DeleteShippingRegion(strID);
        }

        public static void DeleteShippingRegionByShippingID(string strShippingID)
        {
            dal.DeleteShippingRegionByShippingID(strShippingID);
        }

        public static bool IsRegionIn(string regionID1, string regionID2)
        {
            regionID2 = "|" + regionID2 + "|";
            if (regionID1 != string.Empty)
            {
                while (regionID1.Length >= 1)
                {
                    if (regionID2.IndexOf("|" + regionID1 + "|") > -1) return true;
                    regionID1 = regionID1.Substring(0, regionID1.Length - 1);
                    regionID1 = regionID1.Substring(0, regionID1.LastIndexOf('|') + 1);
                }
            }
            return false;
        }

        public static ShippingRegionInfo ReadShippingRegion(int id)
        {
            return dal.ReadShippingRegion(id);
        }

        public static List<ShippingRegionInfo> ReadShippingRegionByShipping(string strShippingID)
        {
            return dal.ReadShippingRegionByShipping(strShippingID);
        }

        public static ShippingRegionInfo SearchShippingRegion(int shippingID, string regionID)
        {
            List<ShippingRegionInfo> list = ReadShippingRegionByShipping(shippingID.ToString());
            ShippingRegionInfo info = new ShippingRegionInfo();
            if (regionID != string.Empty)
            {
                while (regionID.Length >= 1)
                {
                    foreach (ShippingRegionInfo info2 in list)
                    {
                        if (("|" + info2.RegionID + "|").IndexOf("|" + regionID + "|") > -1)
                        {
                            info = info2;
                            break;
                        }
                    }
                    if (info.ID > 0) return info;
                    regionID = regionID.Substring(0, regionID.Length - 1);
                    regionID = regionID.Substring(0, regionID.LastIndexOf('|') + 1);
                }
            }
            return info;
        }

        public static void UpdateShippingRegion(ShippingRegionInfo shippingRegion)
        {
            dal.UpdateShippingRegion(shippingRegion);
        }
    }
}

