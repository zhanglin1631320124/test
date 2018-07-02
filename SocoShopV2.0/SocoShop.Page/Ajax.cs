namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class Ajax : AjaxBasePage
    {
        protected void AddFriend()
        {
            string content = string.Empty;
            int queryString = RequestHelper.GetQueryString<int>("UserID");
            if (queryString > 0)
            {
                if (base.UserID == 0)
                    content = "还未登录";
                else if (base.UserID == queryString)
                    content = "不能添加自己为好友";
                else if (UserFriendBLL.ReadUserFriendByFriendID(queryString, base.UserID).ID > 0)
                    content = "该用户已经是你好友";
                else
                {
                    UserFriendInfo userFriend = new UserFriendInfo();
                    userFriend.FriendID = queryString;
                    userFriend.FriendName = UserBLL.ReadUser(queryString).UserName;
                    userFriend.UserID = base.UserID;
                    userFriend.UserName = base.UserName;
                    UserFriendBLL.AddUserFriend(userFriend);
                    content = "成功加入";
                }
            }
            else
                content = "请选择用户";
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        private void AddGiftPack()
        {
            string content = string.Empty;
            int queryString = RequestHelper.GetQueryString<int>("GiftPackID");
            int num2 = RequestHelper.GetQueryString<int>("GroupID");
            int num3 = RequestHelper.GetQueryString<int>("Count");
            int num4 = RequestHelper.GetQueryString<int>("ProductID");
            string str2 = RequestHelper.GetQueryString<string>("ProductPhoto");
            string str3 = RequestHelper.GetQueryString<string>("ProductName");
            string str4 = num2.ToString() + "," + num3.ToString() + "," + num4.ToString() + "," + str2.Replace(",", string.Empty).Replace("|", string.Empty) + "," + str3.Replace(",", string.Empty).Replace("|", string.Empty);
            string str5 = CookiesHelper.ReadCookieValue("GiftPack" + queryString.ToString());
            if (str5 == string.Empty)
            {
                str4 = StringHelper.Encode(str4, ShopConfig.ReadConfigInfo().SecureKey);
                CookiesHelper.AddCookie("GiftPack" + queryString.ToString(), str4);
            }
            else
            {
                str5 = StringHelper.Decode(str5, ShopConfig.ReadConfigInfo().SecureKey);
                int num5 = 0;
                bool flag = false;
                foreach (string str6 in str5.Split(new char[] { '|' }))
                {
                    if (str6 != string.Empty)
                    {
                        if (str6.Split(new char[] { ',' })[0] == num2.ToString()) num5++;
                        if (str6.Split(new char[] { ',' })[0] == num2.ToString() && str6.Split(new char[] { ',' })[2] == num4.ToString())
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    if (num5 < num3)
                    {
                        str4 = StringHelper.Encode(str5 + "|" + str4, ShopConfig.ReadConfigInfo().SecureKey);
                        CookiesHelper.AddCookie("GiftPack" + queryString.ToString(), str4);
                    }
                    else
                        content = "已经达到该礼品组的限购数量";
                }
                else
                    content = "请勿加入重复商品";
            }
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected void AddGiftPackToCart()
        {
            string content = "ok";
            int queryString = RequestHelper.GetQueryString<int>("GiftPackID");
            GiftPackInfo info = GiftPackBLL.ReadGiftPack(queryString);
            int num2 = 0;
            foreach (string str2 in info.GiftGroup.Split(new char[] { '#' }))
            {
                num2 += Convert.ToInt32(str2.Split(new char[] { '|' })[1]);
            }
            string str3 = StringHelper.Decode(CookiesHelper.ReadCookieValue("GiftPack" + queryString.ToString()), ShopConfig.ReadConfigInfo().SecureKey);
            int length = str3.Split(new char[] { '|' }).Length;
            if (num2 == length)
            {
                string str4 = Guid.NewGuid().ToString();
                foreach (string str2 in str3.Split(new char[] { '|' }))
                {
                    string[] strArray = str2.Split(new char[] { ',' });
                    CartInfo cart = new CartInfo();
                    cart.ProductID = Convert.ToInt32(strArray[2]);
                    cart.ProductName = strArray[4];
                    cart.BuyCount = 1;
                    cart.FatherID = 0;
                    cart.RandNumber = str4;
                    cart.GiftPackID = queryString;
                    cart.UserID = base.UserID;
                    cart.UserName = base.UserName;
                    CartBLL.AddCart(cart, base.UserID);
                    Sessions.ProductBuyCount++;
                }
                Sessions.ProductTotalPrice += info.Price;
                CookiesHelper.DeleteCookie("GiftPack" + queryString.ToString());
                string str5 = content;
                content = str5 + "|" + Sessions.ProductBuyCount.ToString() + "|" + Sessions.ProductTotalPrice.ToString();
            }
            else
                content = "没有满足礼品包的要求";
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected void AddToCart()
        {
            string content = "ok";
            int queryString = RequestHelper.GetQueryString<int>("ProductID");
            string productName = StringHelper.AddSafe(RequestHelper.GetQueryString<string>("ProductName"));
            int num2 = RequestHelper.GetQueryString<int>("BuyCount");
            decimal num3 = RequestHelper.GetQueryString<decimal>("CurrentMemberPrice");
            if (!CartBLL.IsProductInCart(queryString, productName, base.UserID))
            {
                CartInfo cart = new CartInfo();
                cart.ProductID = queryString;
                cart.ProductName = productName;
                cart.BuyCount = num2;
                cart.FatherID = 0;
                cart.RandNumber = string.Empty;
                cart.GiftPackID = 0;
                cart.UserID = base.UserID;
                cart.UserName = base.UserName;
                int num4 = CartBLL.AddCart(cart, base.UserID);
                Sessions.ProductBuyCount += num2;
                Sessions.ProductTotalPrice += num2 * num3;
                ProductInfo info2 = ProductBLL.ReadProduct(queryString);
                if (info2.Accessory != string.Empty)
                {
                    ProductSearchInfo productSearch = new ProductSearchInfo();
                    productSearch.InProductID = info2.Accessory;
                    List<ProductInfo> list = ProductBLL.SearchProductList(productSearch);
                    foreach (ProductInfo info4 in list)
                    {
                        cart = new CartInfo();
                        cart.ProductID = info4.ID;
                        cart.ProductName = info4.Name;
                        cart.BuyCount = num2;
                        cart.FatherID = num4;
                        cart.RandNumber = string.Empty;
                        cart.GiftPackID = 0;
                        cart.UserID = base.UserID;
                        cart.UserName = base.UserName;
                        CartBLL.AddCart(cart, base.UserID);
                    }
                }
            }
            else
                content = "该产品已经在购物车";
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected void CheckEmail()
        {
            int num = 1;
            string email = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("Email"));
            if (email != string.Empty && UserBLL.CheckEmail(email)) num = 2;
            ResponseHelper.Write(num.ToString());
            ResponseHelper.End();
        }

        protected void CheckUserCoupon()
        {
            string content = string.Empty;
            string number = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("Number"));
            string password = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("Password"));
            if (number != string.Empty && password != string.Empty)
            {
                UserCouponInfo userCoupon = UserCouponBLL.ReadUserCouponByNumber(number, password);
                if (userCoupon.ID > 0)
                {
                    if (userCoupon.UserID == 0)
                    {
                        if (userCoupon.IsUse == 0)
                        {
                            CouponInfo info2 = CouponBLL.ReadCoupon(userCoupon.CouponID);
                            if (RequestHelper.DateNow >= info2.UseStartDate && RequestHelper.DateNow <= info2.UseEndDate)
                            {
                                if (Sessions.ProductTotalPrice >= info2.UseMinAmount)
                                {
                                    content = userCoupon.ID.ToString() + "|" + info2.Money.ToString();
                                    if (base.UserID > 0)
                                    {
                                        userCoupon.UserID = base.UserID;
                                        userCoupon.UserName = base.UserName;
                                        UserCouponBLL.UpdateUserCoupon(userCoupon);
                                    }
                                }
                                else
                                    content = "购物车的金额小于该优惠券要求的最低消费的金额";
                            }
                            else
                                content = "该优惠券没在使用期限内";
                        }
                        else
                            content = "该优惠券已经使用了";
                    }
                    else
                        content = "该优惠券已经绑定了用户";
                }
                else
                    content = "不存在该优惠券";
            }
            else
                content = "编号或者密码不能为空";
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected void CheckUserName()
        {
            int num = 1;
            string userName = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("UserName"));
            if (userName != string.Empty)
            {
                string forbiddenName = ShopConfig.ReadConfigInfo().ForbiddenName;
                if (forbiddenName != string.Empty)
                {
                    foreach (string str3 in forbiddenName.Split(new char[] { '|' }))
                    {
                        if (userName.IndexOf(str3.Trim()) != -1)
                        {
                            num = 3;
                            break;
                        }
                    }
                }
                if (num != 3 && UserBLL.CheckUserName(userName) > 0) num = 2;
            }
            ResponseHelper.Write(num.ToString());
            ResponseHelper.End();
        }

        public void Collect()
        {
            string content = string.Empty;
            int queryString = RequestHelper.GetQueryString<int>("ProductID");
            if (queryString > 0)
            {
                if (base.UserID == 0)
                    content = "还未登录";
                else if (ProductCollectBLL.ReadProductCollectByProductID(queryString, base.UserID).ID > 0)
                    content = "您已经收藏了该产品";
                else
                {
                    ProductCollectInfo productCollect = new ProductCollectInfo();
                    productCollect.ProductID = queryString;
                    productCollect.Date = RequestHelper.DateNow;
                    productCollect.UserID = base.UserID;
                    productCollect.UserName = base.UserName;
                    ProductCollectBLL.AddProductCollect(productCollect);
                    content = "成功收藏";
                }
            }
            else
                content = "请选择产品";
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected void DeleteGiftPack()
        {
            string content = string.Empty;
            int queryString = RequestHelper.GetQueryString<int>("GiftPackID");
            int num2 = RequestHelper.GetQueryString<int>("GroupID");
            int num3 = RequestHelper.GetQueryString<int>("ProductID");
            string str2 = StringHelper.Decode(CookiesHelper.ReadCookieValue("GiftPack" + queryString.ToString()), ShopConfig.ReadConfigInfo().SecureKey);
            string str3 = string.Empty;
            foreach (string str4 in str2.Split(new char[] { '|' }))
            {
                if (str4 != string.Empty && str4.Split(new char[] { ',' })[0] != num2.ToString() || str4.Split(new char[] { ',' })[2] != num3.ToString())
                {
                    if (str3 == string.Empty)
                        str3 = str4;
                    else
                        str3 = str3 + "|" + str4;
                }
            }
            str3 = StringHelper.Encode(str3, ShopConfig.ReadConfigInfo().SecureKey);
            CookiesHelper.AddCookie("GiftPack" + queryString.ToString(), str3);
            ResponseHelper.Write(content);
            ResponseHelper.End();
        }

        protected override void PageLoad()
        {
            base.PageLoad();
            switch (RequestHelper.GetQueryString<string>("Action"))
            {
                case "CheckUserName":
                    this.CheckUserName();
                    break;

                case "CheckEmail":
                    this.CheckEmail();
                    break;

                case "Collect":
                    this.Collect();
                    break;

                case "AddFriend":
                    this.AddFriend();
                    break;

                case "AddGiftPack":
                    this.AddGiftPack();
                    break;

                case "DeleteGiftPack":
                    this.DeleteGiftPack();
                    break;

                case "AddToCart":
                    this.AddToCart();
                    break;

                case "AddGiftPackToCart":
                    this.AddGiftPackToCart();
                    break;

                case "SelectShipping":
                    this.SelectShipping();
                    break;

                case "CheckUserCoupon":
                    this.CheckUserCoupon();
                    break;
            }
        }

        protected void SelectShipping()
        {
            int queryString = RequestHelper.GetQueryString<int>("ShippingID");
            string regionID = RequestHelper.GetQueryString<string>("RegionID");
            decimal fixedMoeny = 0M;
            ShippingInfo info = ShippingBLL.ReadShippingCache(queryString);
            ShippingRegionInfo info2 = ShippingRegionBLL.SearchShippingRegion(queryString, regionID);
            switch (info.ShippingType)
            {
                case 1:
                    fixedMoeny = info2.FixedMoeny;
                    break;

                case 2:
                {
                    decimal productTotalWeight = Sessions.ProductTotalWeight;
                    if (productTotalWeight > info.FirstWeight)
                    {
                        fixedMoeny = info2.FirstMoney + Math.Ceiling((decimal) ((productTotalWeight - info.FirstWeight) / info.AgainWeight)) * info2.AgainMoney;
                        break;
                    }
                    fixedMoeny = info2.FirstMoney;
                    break;
                }
                case 3:
                {
                    int productBuyCount = Sessions.ProductBuyCount;
                    fixedMoeny = info2.OneMoeny + (productBuyCount - 1) * info2.AnotherMoeny;
                    break;
                }
            }
            decimal num5 = 0M;
            FavorableActivityInfo info3 = FavorableActivityBLL.ReadFavorableActivity(DateTime.Now, DateTime.Now, 0);
            if (info3.ID > 0 && ("," + info3.UserGrade + ",").IndexOf("," + this.GradeID.ToString() + ",") > -1 && Sessions.ProductTotalPrice >= info3.OrderProductMoney)
            {
                switch (info3.ReduceWay)
                {
                    case 1:
                        num5 += info3.ReduceMoney;
                        break;

                    case 2:
                        num5 += Sessions.ProductTotalPrice * (10M - info3.ReduceDiscount) / 10M;
                        break;
                }
                if (info3.ShippingWay == 1 && ShippingRegionBLL.IsRegionIn(regionID, info3.RegionID)) num5 += fixedMoeny;
            }
            ResponseHelper.Write(fixedMoeny.ToString() + "|" + num5.ToString());
            ResponseHelper.End();
        }
    }
}

