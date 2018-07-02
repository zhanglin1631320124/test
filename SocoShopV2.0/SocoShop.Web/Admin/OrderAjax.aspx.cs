namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;

    public partial class OrderAjax : AdminBasePage
    {
        protected bool canEdit = false;
        protected FavorableActivityInfo favorableActivity = new FavorableActivityInfo();
        protected OrderInfo order = new OrderInfo();
        protected List<OrderCommonProductVirtualInfo> orderCommonProductVirtualList = new List<OrderCommonProductVirtualInfo>();
        protected List<OrderDetailInfo> orderDetailList = new List<OrderDetailInfo>();
        protected List<OrderGiftPackVirtualInfo> orderGiftPackVirtualList = new List<OrderGiftPackVirtualInfo>();
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected int totalPoint = 0;
        protected int totalProductCount = 0;
        protected decimal totalWeight = 0M;
        protected UserCouponInfo userCoupon = new UserCouponInfo();

        protected void ChangeOrderProductBuyCount()
        {
            string queryString = RequestHelper.GetQueryString<string>("StrOrderDetailID");
            int buyCount = RequestHelper.GetQueryString<int>("BuyCount");
            string strProductID = RequestHelper.GetQueryString<string>("StrProductID");
            int num2 = RequestHelper.GetQueryString<int>("OldCount");
            int orderID = RequestHelper.GetQueryString<int>("OrderID");
            ProductBLL.ChangeProductOrderCount(strProductID, num2 - buyCount);
            OrderDetailBLL.ChangeOrderProductBuyCount(queryString, buyCount);
            this.OrderUpdateHanlder(orderID);
            ResponseHelper.End();
        }

        protected void DeleteOrderProduct()
        {
            string queryString = RequestHelper.GetQueryString<string>("StrOrderDetailID");
            string strProductID = RequestHelper.GetQueryString<string>("StrProductID");
            int changeCount = RequestHelper.GetQueryString<int>("OldCount");
            int orderID = RequestHelper.GetQueryString<int>("OrderID");
            ProductBLL.ChangeProductOrderCount(strProductID, changeCount);
            OrderDetailBLL.DeleteOrderDetail(queryString);
            this.OrderUpdateHanlder(orderID);
            ResponseHelper.End();
        }

        protected void OrderUpdateHanlder(int orderID)
        {
            OrderInfo order = OrderBLL.ReadOrder(orderID, 0);
            order.ProductMoney = OrderBLL.ReadOrderProductPrice(orderID);
            order.ShippingMoney = OrderBLL.ReadOrderShippingMoney(order);
            this.ReadFavorableInfo(order);
            OrderBLL.UpdateOrder(order);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int isCod;
            base.ClearCache();
            string queryString = RequestHelper.GetQueryString<string>("Action");
            if (queryString != string.Empty)
            {
                OrderInfo info = OrderBLL.ReadOrder(RequestHelper.GetQueryString<int>("OrderID"), 0);
                isCod = PayPlugins.ReadPayPlugins(info.PayKey).IsCod;
                if (info.OrderStatus != 1 && info.OrderStatus != 2 || isCod != 1 || info.IsActivity != 0)
                {
                    ResponseHelper.Write("订单已经审核，无法修改");
                    ResponseHelper.End();
                }
                else
                {
                    string str3 = queryString;
                    if (str3 != null)
                    {
                        if (!(str3 == "DeleteOrderProduct"))
                        {
                            if (str3 == "ChangeOrderProductBuyCount") this.ChangeOrderProductBuyCount();
                        }
                        else
                            this.DeleteOrderProduct();
                    }
                }
            }
            int id = RequestHelper.GetQueryString<int>("ID");
            if (id != -2147483648)
            {
                base.CheckAdminPower("ReadOrder", PowerCheckType.Single);
                this.order = OrderBLL.ReadOrder(id, 0);
                isCod = PayPlugins.ReadPayPlugins(this.order.PayKey).IsCod;
                if ((this.order.OrderStatus == 1 || this.order.OrderStatus == 2 && isCod == 1) && this.order.IsActivity == 0) this.canEdit = true;
                this.orderDetailList = OrderDetailBLL.ReadOrderDetailByOrder(id);
                if (this.order.FavorableActivityID > 0) this.favorableActivity = FavorableActivityBLL.ReadFavorableActivity(this.order.FavorableActivityID);
                this.userCoupon = UserCouponBLL.ReadUserCouponByOrder(this.order.ID);
                string str2 = string.Empty;
                foreach (OrderDetailInfo info2 in this.orderDetailList)
                {
                    if (info2.FatherID == 0) this.totalProductCount += info2.BuyCount;
                    this.totalWeight += info2.BuyCount * info2.ProductWeight;
                    this.totalPoint += info2.BuyCount * info2.SendPoint;
                    if (str2 == string.Empty)
                        str2 = info2.ProductID.ToString();
                    else
                        str2 = str2 + "," + info2.ProductID.ToString();
                }
                if (str2 != string.Empty)
                {
                    ProductSearchInfo productSearch = new ProductSearchInfo();
                    productSearch.InProductID = str2;
                    this.productList = ProductBLL.SearchProductList(productSearch);
                }
                OrderDetailBLL.HandlerOrderDetailList(this.orderDetailList, ref this.orderGiftPackVirtualList, ref this.orderCommonProductVirtualList);
            }
        }

        protected void ReadFavorableInfo(OrderInfo order)
        {
            FavorableActivityInfo info = FavorableActivityBLL.ReadFavorableActivity(order.AddDate, order.AddDate, 0);
            if (info.ID > 0)
            {
                UserGradeInfo info3 = UserGradeBLL.ReadUserGradeByMoney(UserBLL.ReadUserMore(order.UserID).MoneyUsed);
                if (("," + info.UserGrade + ",").IndexOf("," + info3.ID + ",") <= -1 || order.ProductMoney < info.OrderProductMoney)
                {
                    order.FavorableActivityID = 0;
                    order.GiftID = 0;
                    order.FavorableMoney = 0M;
                }
                else
                {
                    order.FavorableMoney = 0M;
                    if (info.ID != order.FavorableActivityID)
                    {
                        order.FavorableActivityID = info.ID;
                        order.GiftID = 0;
                    }
                    switch (info.ReduceWay)
                    {
                        case 1:
                            order.FavorableMoney += info.ReduceMoney;
                            break;

                        case 2:
                            order.FavorableMoney += order.ProductMoney * (10M - info.ReduceDiscount) / 10M;
                            break;
                    }
                    if (info.ShippingWay == 1 && ShippingRegionBLL.IsRegionIn(order.RegionID, info.RegionID)) order.FavorableMoney += order.ShippingMoney;
                }
            }
        }
    }
}

