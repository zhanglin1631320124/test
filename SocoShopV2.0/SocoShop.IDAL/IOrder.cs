namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IOrder
    {
        int AddOrder(OrderInfo order);
        void DeleteOrder(string strID, int userID);
        OrderInfo ReadOrder(int id, int userID);
        OrderInfo ReadOrderByNumber(string orderNumber, int userID);
        string ReadOrderIDList(string strID, int userID);
        List<OrderInfo> ReadPreNextOrder(int id);
        List<OrderInfo> SearchOrderList(OrderSearchInfo order);
        List<OrderInfo> SearchOrderList(int currentPage, int pageSize, OrderSearchInfo order, ref int count);
        DataTable StatisticsOrderArea(OrderSearchInfo orderSearch);
        DataTable StatisticsOrderCount(OrderSearchInfo orderSearch, DateType dateType);
        DataTable StatisticsOrderStatus(OrderSearchInfo orderSearch);
        DataTable StatisticsSaleStop(string productIDList);
        DataTable StatisticsSaleTotal(OrderSearchInfo orderSearch, DateType dateType);
        void UpdateOrder(OrderInfo order);
    }
}

