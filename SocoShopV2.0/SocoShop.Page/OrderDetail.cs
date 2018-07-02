namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class OrderDetail : UserBasePage
    {
        protected GiftInfo gift = new GiftInfo();
        protected OrderInfo order = new OrderInfo();
        protected List<OrderCommonProductVirtualInfo> orderCommonProductVirtualList = new List<OrderCommonProductVirtualInfo>();
        protected List<OrderDetailInfo> orderDetailList = new List<OrderDetailInfo>();
        protected List<OrderGiftPackVirtualInfo> orderGiftPackVirtualList = new List<OrderGiftPackVirtualInfo>();
        protected List<ProductInfo> productList = new List<ProductInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.order = OrderBLL.ReadOrder(queryString, base.UserID);
            if (this.order.GiftID > 0) this.gift = GiftBLL.ReadGift(this.order.GiftID);
            this.orderDetailList = OrderDetailBLL.ReadOrderDetailByOrder(queryString);
            string str = string.Empty;
            foreach (OrderDetailInfo info in this.orderDetailList)
            {
                if (str == string.Empty)
                    str = info.ProductID.ToString();
                else
                    str = str + "," + info.ProductID.ToString();
            }
            if (str != string.Empty)
            {
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.InProductID = str;
                this.productList = ProductBLL.SearchProductList(productSearch);
            }
            OrderDetailBLL.HandlerOrderDetailList(this.orderDetailList, ref this.orderGiftPackVirtualList, ref this.orderCommonProductVirtualList);
        }
    }
}

