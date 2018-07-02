namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IShippingRegion
    {
        int AddShippingRegion(ShippingRegionInfo shippingRegion);
        void DeleteShippingRegion(string strID);
        void DeleteShippingRegionByShippingID(string strShippingID);
        ShippingRegionInfo ReadShippingRegion(int id);
        List<ShippingRegionInfo> ReadShippingRegionByShipping(string strShippingID);
        void UpdateShippingRegion(ShippingRegionInfo shippingRegion);
    }
}

