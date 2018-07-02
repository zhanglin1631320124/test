namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IShipping
    {
        int AddShipping(ShippingInfo shipping);
        void ChangeShippingOrder(ChangeAction action, int id);
        void DeleteShipping(string strID);
        List<ShippingInfo> ReadShippingAllList();
        void UpdateShipping(ShippingInfo shipping);
    }
}

