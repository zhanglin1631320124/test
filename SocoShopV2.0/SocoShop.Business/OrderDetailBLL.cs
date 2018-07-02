namespace SocoShop.Business
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using SkyCES.EntLib;
    using SocoShop.Common;

    public sealed class OrderDetailBLL
    {
        private static readonly IOrderDetail dal = FactoryHelper.Instance<IOrderDetail>(Global.DataProvider, "OrderDetailDAL");

        public static int AddOrderDetail(OrderDetailInfo orderDetail)
        {
            orderDetail.ID = dal.AddOrderDetail(orderDetail);
            return orderDetail.ID;
        }

        public static void ChangeOrderProductBuyCount(string strID, int buyCount)
        {
            dal.ChangeOrderProductBuyCount(strID, buyCount);
        }

        public static void DeleteOrderDetail(string strID)
        {
            dal.DeleteOrderDetail(strID);
        }

        public static void DeleteOrderDetailByOrderID(string strOrderID)
        {
            dal.DeleteOrderDetailByOrderID(strOrderID);
        }

        public static void HandlerOrderDetailList(List<OrderDetailInfo> orderDetailList, ref List<OrderGiftPackVirtualInfo> orderGiftPackVirtualList, ref List<OrderCommonProductVirtualInfo> orderCommonProductVirtualList)
        {
            string str;
            string str2;
            decimal num2;
            int num3;
            foreach (OrderDetailInfo info in orderDetailList)
            {
                OrderGiftPackVirtualInfo current;
                if (!(info.RandNumber != string.Empty) || info.GiftPackID <= 0) goto Label_00EB;
                bool flag = false;
                using (List<OrderGiftPackVirtualInfo>.Enumerator enumerator2 = orderGiftPackVirtualList.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        current = enumerator2.Current;
                        if (current.RandNumber == info.RandNumber)
                        {
                            flag = true;
                            current.OrderDetailList.Add(info);
                            goto Label_00A5;
                        }
                    }
                }
            Label_00A5:
                if (!flag)
                {
                    current = new OrderGiftPackVirtualInfo();
                    current.RandNumber = info.RandNumber;
                    current.GiftPackBuyCount = info.BuyCount;
                    current.OrderDetailList.Add(info);
                    orderGiftPackVirtualList.Add(current);
                }
                goto Label_017D;
            Label_00EB:
                if (info.FatherID == 0)
                {
                    OrderCommonProductVirtualInfo item = new OrderCommonProductVirtualInfo();
                    item.FatherOrderDetail = info;
                    orderCommonProductVirtualList.Add(item);
                }
                else
                {
                    foreach (OrderCommonProductVirtualInfo info3 in orderCommonProductVirtualList)
                    {
                        if (info3.FatherOrderDetail.ID == info.FatherID)
                        {
                            info3.ChildOrderDetailList.Add(info);
                            break;
                        }
                    }
                }
            Label_017D:;
            }
            if (orderGiftPackVirtualList.Count > 0)
            {
                foreach (OrderGiftPackVirtualInfo info2 in orderGiftPackVirtualList)
                {
                    int id = 0;
                    str = string.Empty;
                    str2 = string.Empty;
                    num2 = 0M;
                    num3 = 0;
                    decimal num4 = 0M;
                    foreach (OrderDetailInfo info in info2.OrderDetailList)
                    {
                        if (str == string.Empty)
                        {
                            str = info.ProductID.ToString();
                            str2 = info.ID.ToString();
                        }
                        else
                        {
                            str = str + "," + info.ProductID.ToString();
                            str2 = str2 + "," + info.ID.ToString();
                        }
                        num2 += info.ProductWeight;
                        num3 += info.SendPoint;
                        num4 += info.ProductPrice;
                        id = info.GiftPackID;
                    }
                    GiftPackInfo info4 = GiftPackBLL.ReadGiftPack(id);
                    info2.GiftPackID = id;
                    info2.GiftPackName = info4.Name;
                    info2.GiftPackPhoto = info4.Photo;
                    info2.StrProductID = str;
                    info2.StrOrderDetailID = str2;
                    info2.TotalProductWeight = num2;
                    info2.TotalSendPoint = num3;
                    info2.TotalPrice = num4;
                }
            }
            if (orderCommonProductVirtualList.Count > 0)
            {
                foreach (OrderCommonProductVirtualInfo info3 in orderCommonProductVirtualList)
                {
                    str = info3.FatherOrderDetail.ProductID.ToString();
                    str2 = info3.FatherOrderDetail.ID.ToString();
                    num2 = 0M;
                    num3 = 0;
                    foreach (OrderDetailInfo info in info3.ChildOrderDetailList)
                    {
                        str = str + "," + info.ProductID.ToString();
                        str2 = str2 + "," + info.ID.ToString();
                        num2 += info.ProductWeight;
                        num3 += info.SendPoint;
                    }
                    info3.StrProductID = str;
                    info3.StrOrderDetailID = str2;
                    OrderDetailInfo fatherOrderDetail = info3.FatherOrderDetail;
                    fatherOrderDetail.ProductWeight += num2;
                    OrderDetailInfo info5 = info3.FatherOrderDetail;
                    info5.SendPoint += num3;
                }
            }
        }

        public static OrderDetailInfo ReadOrderDetail(int id)
        {
            return dal.ReadOrderDetail(id);
        }

        public static List<OrderDetailInfo> ReadOrderDetailByOrder(int orderID)
        {
            return dal.ReadOrderDetailByOrder(orderID);
        }

        public static List<OrderDetailInfo> ReadOrderDetailByProductID(int productID)
        {
            return dal.ReadOrderDetailByProductID(productID);
        }

        public static DataTable StatisticsSaleDetail(int currentPage, int pageSize, OrderSearchInfo orderSearch, ProductSearchInfo productSearch, ref int count)
        {
            return dal.StatisticsSaleDetail(currentPage, pageSize, orderSearch, productSearch, ref count);
        }

        public static void UpdateOrderDetail(OrderDetailInfo orderDetail)
        {
            dal.UpdateOrderDetail(orderDetail);
        }
    }
}

