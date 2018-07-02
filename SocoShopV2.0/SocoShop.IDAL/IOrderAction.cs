namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IOrderAction
    {
        int AddOrderAction(OrderActionInfo orderDetail);
        void DeleteOrderAction(string strID);
        void DeleteOrderActionByOrderID(string strOrderID);
        OrderActionInfo ReadLatestOrderAction(int orderID, int endOrderStatus);
        OrderActionInfo ReadOrderAction(int id);
        List<OrderActionInfo> ReadOrderActionByOrder(int orderID);
    }
}

