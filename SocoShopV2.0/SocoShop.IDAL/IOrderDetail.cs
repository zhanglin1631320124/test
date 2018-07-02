namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IOrderDetail
    {
        int AddOrderDetail(OrderDetailInfo orderDetail);
        void ChangeOrderProductBuyCount(string strID, int buyCount);
        void DeleteOrderDetail(string strID);
        void DeleteOrderDetailByOrderID(string strOrderID);
        OrderDetailInfo ReadOrderDetail(int id);
        List<OrderDetailInfo> ReadOrderDetailByOrder(int orderID);
        List<OrderDetailInfo> ReadOrderDetailByProductID(int productID);
        DataTable StatisticsSaleDetail(int currentPage, int pageSize, OrderSearchInfo orderSearch, ProductSearchInfo productSearch, ref int count);
        void UpdateOrderDetail(OrderDetailInfo orderDetail);
    }
}

