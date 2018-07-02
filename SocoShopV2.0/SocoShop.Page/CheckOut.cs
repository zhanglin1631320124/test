namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class CheckOut : CommonBasePage
    {
        protected FavorableActivityInfo favorableActivity = new FavorableActivityInfo();
        protected List<GiftInfo> giftList = new List<GiftInfo>();
        protected decimal moneyLeft = 0M;
        protected List<PayPluginsInfo> payPluginsList = new List<PayPluginsInfo>();
        protected List<UserAddressInfo> userAddressList = new List<UserAddressInfo>();
        protected List<UserCouponInfo> userCouponList = new List<UserCouponInfo>();

        protected void AddOrderProduct(int orderID)
        {
            List<CartInfo> list = CartBLL.ReadCartList(base.UserID);
            string strProductID = string.Empty;
            foreach (CartInfo info in list)
            {
                if (strProductID == string.Empty)
                    strProductID = info.ProductID.ToString();
                else
                    strProductID = strProductID + "," + info.ProductID.ToString();
            }
            List<ProductInfo> productList = new List<ProductInfo>();
            if (strProductID != string.Empty)
            {
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.InProductID = strProductID;
                productList = ProductBLL.SearchProductList(productSearch);
            }
            List<MemberPriceInfo> memberPriceList = MemberPriceBLL.ReadMemberPriceByProductGrade(strProductID, base.GradeID);
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
            foreach (CartInfo info in list)
            {
                ProductInfo product = ProductBLL.ReadProductByProductList(productList, info.ProductID);
                OrderDetailInfo orderDetail = new OrderDetailInfo();
                orderDetail.OrderID = orderID;
                orderDetail.ProductID = info.ProductID;
                orderDetail.ProductName = info.ProductName;
                orderDetail.ProductWeight = product.Weight;
                orderDetail.SendPoint = product.SendPoint;
                if (info.GiftPackID == 0)
                    orderDetail.ProductPrice = MemberPriceBLL.ReadCurrentMemberPrice(memberPriceList, base.GradeID, product);
                else if (dictionary.ContainsKey(info.RandNumber + "|" + info.GiftPackID.ToString()))
                    orderDetail.ProductPrice = 0M;
                else
                {
                    orderDetail.ProductPrice = GiftPackBLL.ReadGiftPack(info.GiftPackID).Price;
                    dictionary.Add(info.RandNumber + "|" + info.GiftPackID.ToString(), true);
                }
                orderDetail.BuyCount = info.BuyCount;
                if (info.FatherID > 0) orderDetail.FatherID = dictionary2[info.FatherID];
                orderDetail.RandNumber = info.RandNumber;
                orderDetail.GiftPackID = info.GiftPackID;
                int num = OrderDetailBLL.AddOrderDetail(orderDetail);
                dictionary2.Add(info.ID, num);
            }
            CartBLL.ClearCart(base.UserID);
            Sessions.ProductTotalPrice = 0M;
            Sessions.ProductBuyCount = 0;
            Sessions.ProductTotalWeight = 0M;
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            if (ShopConfig.ReadConfigInfo().AllowAnonymousAddCart == 0 && base.UserID == 0)
            {
                ResponseHelper.Redirect("/User/Login.aspx?RedirectUrl=/CheckOut.aspx");
                ResponseHelper.End();
            }
            if (Sessions.ProductBuyCount == 0 || Sessions.ProductTotalPrice == 0M)
            {
                ResponseHelper.Redirect("/Cart.aspx");
                ResponseHelper.End();
            }
            if (base.UserID > 0)
            {
                this.userAddressList = UserAddressBLL.ReadUserAddressByUser(base.UserID);
                List<UserCouponInfo> list = UserCouponBLL.ReadUserCouponCanUse(base.UserID);
                foreach (UserCouponInfo info in list)
                {
                    if (info.Coupon.UseMinAmount <= Sessions.ProductTotalPrice) this.userCouponList.Add(info);
                }
                this.moneyLeft = UserBLL.ReadUserMore(base.UserID).MoneyLeft;
            }
            this.payPluginsList = PayPlugins.ReadProductBuyPayPluginsList();
            this.favorableActivity = FavorableActivityBLL.ReadFavorableActivity(DateTime.Now, DateTime.Now, 0);
            if (this.favorableActivity.ID > 0)
            {
                if (("," + this.favorableActivity.UserGrade + ",").IndexOf("," + this.GradeID.ToString() + ",") > -1 && Sessions.ProductTotalPrice >= this.favorableActivity.OrderProductMoney)
                {
                    if (this.favorableActivity.GiftID != string.Empty)
                    {
                        GiftSearchInfo gift = new GiftSearchInfo();
                        gift.InGiftID = this.favorableActivity.GiftID;
                        this.giftList = GiftBLL.SearchGiftList(gift);
                    }
                }
                else
                    this.favorableActivity = new FavorableActivityInfo();
            }
            base.Title = "结算中心";
        }

        protected override void PostBack()
        {
            string url = "/CheckOut.aspx";
            string str2 = StringHelper.AddSafe(RequestHelper.GetForm<string>("Consignee"));
            if (str2 == string.Empty) ScriptHelper.Alert("收货人姓名不能为空", url);
            string str3 = StringHelper.AddSafe(RequestHelper.GetForm<string>("Tel"));
            string str4 = StringHelper.AddSafe(RequestHelper.GetForm<string>("Mobile"));
            if (str3 == string.Empty && str4 == string.Empty) ScriptHelper.Alert("固定电话，手机必须得填写一个", url);
            string str5 = StringHelper.AddSafe(RequestHelper.GetForm<string>("ZipCode"));
            if (str5 == string.Empty) ScriptHelper.Alert("邮编不能为空", url);
            string str6 = StringHelper.AddSafe(RequestHelper.GetForm<string>("Address"));
            if (str6 == string.Empty) ScriptHelper.Alert("地址不能为空", url);
            int form = RequestHelper.GetForm<int>("ShippingID");
            if (form == -2147483648) ScriptHelper.Alert("请选择配送方式", url);
            decimal productTotalPrice = Sessions.ProductTotalPrice;
            decimal num3 = RequestHelper.GetForm<decimal>("FavorableMoney");
            decimal num4 = RequestHelper.GetForm<decimal>("ShippingMoney");
            decimal num5 = RequestHelper.GetForm<decimal>("Balance");
            decimal num6 = RequestHelper.GetForm<decimal>("CouponMoney");
            if (productTotalPrice - num3 + num4 - num5 - num6 < 0M) ScriptHelper.Alert("金额有错误，请重新检查", url);
            string key = RequestHelper.GetForm<string>("Pay");
            PayPluginsInfo info = PayPlugins.ReadPayPlugins(key);
            OrderInfo order = new OrderInfo();
            order.OrderNumber = ShopCommon.CreateOrderNumber();
            order.IsActivity = 0;
            if (productTotalPrice - num3 + num4 - num5 - num6 == 0M || info.IsCod == 1)
                order.OrderStatus = 2;
            else
                order.OrderStatus = 1;
            order.OrderNote = string.Empty;
            order.ProductMoney = productTotalPrice;
            order.Balance = num5;
            order.FavorableMoney = num3;
            order.OtherMoney = 0M;
            order.CouponMoney = num6;
            order.Consignee = str2;
            SingleUnlimitClass class2 = new SingleUnlimitClass();
            order.RegionID = class2.ClassID;
            order.Address = str6;
            order.ZipCode = str5;
            order.Tel = str3;
            if (base.UserID == 0)
                order.Email = StringHelper.AddSafe(RequestHelper.GetForm<string>("Email"));
            else
                order.Email = CookiesHelper.ReadCookieValue("UserEmail");
            order.Mobile = str4;
            order.ShippingID = form;
            order.ShippingDate = RequestHelper.DateNow;
            order.ShippingNumber = string.Empty;
            order.ShippingMoney = num4;
            order.PayKey = key;
            order.PayName = info.Name;
            order.PayDate = RequestHelper.DateNow;
            order.IsRefund = 0;
            order.FavorableActivityID = RequestHelper.GetForm<int>("FavorableActivityID");
            order.GiftID = RequestHelper.GetForm<int>("GiftID");
            order.InvoiceTitle = StringHelper.AddSafe(RequestHelper.GetForm<string>("InvoiceTitle"));
            order.InvoiceContent = StringHelper.AddSafe(RequestHelper.GetForm<string>("InvoiceContent"));
            order.UserMessage = StringHelper.AddSafe(RequestHelper.GetForm<string>("UserMessage"));
            order.AddDate = RequestHelper.DateNow;
            order.IP = ClientHelper.IP;
            order.UserID = base.UserID;
            order.UserName = base.UserName;
            int orderID = OrderBLL.AddOrder(order);
            if (num5 > 0M) UserAccountRecordBLL.AddUserAccountRecord(-num5, 0, "支付订单：" + order.OrderNumber, base.UserID, base.UserName);
            string str8 = RequestHelper.GetForm<string>("UserCoupon");
            if (num6 > 0M && str8 != "0|0")
            {
                UserCouponInfo userCoupon = UserCouponBLL.ReadUserCoupon(Convert.ToInt32(str8.Split(new char[] { '|' })[0]), base.UserID);
                userCoupon.IsUse = 1;
                userCoupon.OrderID = orderID;
                UserCouponBLL.UpdateUserCoupon(userCoupon);
            }
            this.AddOrderProduct(orderID);
            ProductBLL.ChangeProductOrderCountByOrder(orderID, ChangeAction.Plus);
            ResponseHelper.Redirect("/Finish-I" + orderID.ToString() + ".aspx");
        }
    }
}

