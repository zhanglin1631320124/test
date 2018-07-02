namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using SkyCES.EntLib;

    public sealed class OrderBLL
    {
        private static readonly IOrder dal = FactoryHelper.Instance<IOrder>(Global.DataProvider, "OrderDAL");

        public static int AddOrder(OrderInfo order)
        {
            order.ID = dal.AddOrder(order);
            return order.ID;
        }

        public static void AdminUpdateOrderAddAction(OrderInfo order, string note, int orderOperate, int startOrderStatus)
        {
            dal.UpdateOrder(order);
            OrderActionBLL.AdminAddOrderAction(order.ID, startOrderStatus, order.OrderStatus, note, orderOperate);
        }

        public static void DeleteOrder(string strID, int userID)
        {
            if (userID != 0) strID = dal.ReadOrderIDList(strID, userID);
            OrderDetailBLL.DeleteOrderDetailByOrderID(strID);
            OrderActionBLL.DeleteOrderActionByOrderID(strID);
            dal.DeleteOrder(strID, userID);
        }

        public static decimal ReadHasPaidMoney(OrderInfo order)
        {
            return (order.Balance + order.CouponMoney);
        }

        public static decimal ReadNoPayMoney(OrderInfo order)
        {
            return (ReadOrderMoney(order) - ReadHasPaidMoney(order));
        }

        public static OrderInfo ReadOrder(int id, int userID)
        {
            return dal.ReadOrder(id, userID);
        }

        public static OrderInfo ReadOrderByNumber(string orderNumber, int userID)
        {
            return dal.ReadOrderByNumber(orderNumber, userID);
        }

        public static decimal ReadOrderMoney(OrderInfo order)
        {
            return (order.ProductMoney - order.FavorableMoney + order.ShippingMoney + order.OtherMoney);
        }

        public static decimal ReadOrderProductPrice(int id)
        {
            decimal num = 0M;
            foreach (OrderDetailInfo info in OrderDetailBLL.ReadOrderDetailByOrder(id))
            {
                num += info.ProductPrice * info.BuyCount;
            }
            return num;
        }

        public static int ReadOrderSendPoint(int id)
        {
            int num = 0;
            foreach (OrderDetailInfo info in OrderDetailBLL.ReadOrderDetailByOrder(id))
            {
                num += info.SendPoint * info.BuyCount;
            }
            return num;
        }

        public static decimal ReadOrderShippingMoney(OrderInfo order)
        {
            decimal shippingMoney = order.ShippingMoney;
            ShippingInfo info = ShippingBLL.ReadShippingCache(order.ShippingID);
            ShippingRegionInfo info2 = ShippingRegionBLL.SearchShippingRegion(order.ShippingID, order.RegionID);
            switch (info.ShippingType)
            {
                case 1:
                    return info2.FixedMoeny;

                case 2:
                {
                    decimal num2 = 0M;
                    foreach (OrderDetailInfo info3 in OrderDetailBLL.ReadOrderDetailByOrder(order.ID))
                    {
                        num2 += info3.ProductWeight * info3.BuyCount;
                    }
                    if (num2 <= info.FirstWeight) return info2.FirstMoney;
                    return (info2.FirstMoney + Math.Ceiling((decimal) ((num2 - info.FirstWeight) / info.AgainWeight)) * info2.AgainMoney);
                }
                case 3:
                {
                    int num3 = 0;
                    foreach (OrderDetailInfo info3 in OrderDetailBLL.ReadOrderDetailByOrder(order.ID))
                    {
                        if (info3.FatherID == 0) num3 += info3.BuyCount;
                    }
                    return (info2.OneMoeny + (num3 - 1) * info2.AnotherMoeny);
                }
            }
            return shippingMoney;
        }

        public static string ReadOrderStatus(int orderStatus)
        {
            string str = string.Empty;
            switch (orderStatus)
            {
                case 1:
                    return "待付款";

                case 2:
                    return "待审核";

                case 3:
                    return "无效";

                case 4:
                    return "配货中";

                case 5:
                    return "已发货";

                case 6:
                    return "已收货";

                case 7:
                    return "已退货";
            }
            return str;
        }

        public static string ReadOrderUserOperate(int orderID, int orderStatus, string payKey)
        {
            string str = string.Empty;
            switch (orderStatus)
            {
                case 1:
                    str = string.Concat(new object[] { "<a href=\"javascript:orderOperate(", orderID, ",", 1, ")\">取消</a>" });
                    if (PayPlugins.ReadPayPlugins(payKey).IsOnline == 1)
                    {
                        string str3 = str;
                        str = str3 + " <a href=\"/Plugins/Pay/" + payKey + "/Pay.aspx?Action=PayOrder&OrderID=" + orderID.ToString() + "\" target=\"_blank\">付款</a>";
                    }
                    return str;

                case 2:
                    return string.Concat(new object[] { "<a href=\"javascript:orderOperate(", orderID, ",", 2, ")\">取消</a>" });

                case 3:
                case 4:
                    return str;

                case 5:
                    return string.Concat(new object[] { "<a href=\"javascript:orderOperate(", orderID, ",", 5, ")\">确定收货</a>" });

                case 6:
                case 7:
                    return str;
            }
            return str;
        }

        public static List<OrderInfo> ReadPreNextOrder(int id)
        {
            return dal.ReadPreNextOrder(id);
        }

        public static List<OrderInfo> SearchOrderList(OrderSearchInfo order)
        {
            return dal.SearchOrderList(order);
        }

        public static List<OrderInfo> SearchOrderList(int currentPage, int pageSize, OrderSearchInfo order, ref int count)
        {
            return dal.SearchOrderList(currentPage, pageSize, order, ref count);
        }

        public static DataTable StatisticsOrderArea(OrderSearchInfo orderSearch)
        {
            return dal.StatisticsOrderArea(orderSearch);
        }

        public static DataTable StatisticsOrderCount(OrderSearchInfo orderSearch, DateType dateType)
        {
            return dal.StatisticsOrderCount(orderSearch, dateType);
        }

        public static DataTable StatisticsOrderStatus(OrderSearchInfo orderSearch)
        {
            return dal.StatisticsOrderStatus(orderSearch);
        }

        public static DataTable StatisticsSaleStop(string productIDList)
        {
            return dal.StatisticsSaleStop(productIDList);
        }

        public static DataTable StatisticsSaleTotal(OrderSearchInfo orderSearch, DateType dateType)
        {
            return dal.StatisticsSaleTotal(orderSearch, dateType);
        }

        public static void UpdateOrder(OrderInfo order)
        {
            dal.UpdateOrder(order);
        }

        public static void UserUpdateOrderAddAction(OrderInfo order, string note, int orderOperate, int startOrderStatus)
        {
            dal.UpdateOrder(order);
            OrderActionBLL.UserAddOrderAction(order.ID, startOrderStatus, order.OrderStatus, note, orderOperate);
        }
    }
}

