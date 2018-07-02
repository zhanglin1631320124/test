namespace SocoShop.Business
{
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using SkyCES.EntLib;

    public sealed class CartBLL
    {
        private static readonly ICart dal = FactoryHelper.Instance<ICart>(Global.DataProvider, "CartDAL");

        public static int AddCart(CartInfo cart, int userID)
        {
            if (userID > 0)
                cart.ID = dal.AddCart(cart);
            else
                cart.ID = CartHelper.AddToCart(cart);
            return cart.ID;
        }

        public static void ClearCart(int userID)
        {
            if (userID > 0)
                dal.ClearCart(userID);
            else
                CartHelper.ClearCart();
        }

        public static void CookiesImportDataBase(int userID, string userName)
        {
            List<CartInfo> list = CartHelper.ReadCart();
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
            foreach (CartInfo info in list)
            {
                bool flag = false;
                if (info.GiftPackID > 0)
                    flag = true;
                else if (info.FatherID == 0 && !IsProductInCart(info.ProductID, info.ProductName, userID) || info.FatherID > 0 && dictionary.ContainsKey(info.FatherID))
                {
                    flag = true;
                    dictionary.Add(info.ID, 1);
                }
                if (flag)
                {
                    if (info.FatherID > 0) info.FatherID = dictionary2[info.FatherID];
                    info.UserID = userID;
                    info.UserName = userName;
                    int num = dal.AddCart(info);
                    dictionary2.Add(info.ID, num);
                }
            }
            CartHelper.ClearCart();
        }

        public static void DeleteCart(string strID, int userID)
        {
            if (userID > 0)
            {
                strID = dal.ReadCartIDList(strID, userID);
                dal.DeleteCart(strID, userID);
            }
            else
                CartHelper.DeleteCart(strID);
        }

        public static void HandlerCartList(List<CartInfo> cartList, ref List<CartGiftPackVirtualInfo> cartGiftPackVirtualList, ref List<CartCommonProductVirtualInfo> cartCommonProductVirtualList)
        {
            string str;
            string str2;
            decimal num2;
            int num3;
            foreach (CartInfo info in cartList)
            {
                CartGiftPackVirtualInfo current;
                if (!(info.RandNumber != string.Empty) || info.GiftPackID <= 0) goto Label_00F8;
                bool flag = false;
                using (List<CartGiftPackVirtualInfo>.Enumerator enumerator2 = cartGiftPackVirtualList.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        current = enumerator2.Current;
                        if (current.RandNumber == info.RandNumber)
                        {
                            flag = true;
                            current.CartList.Add(info);
                            goto Label_00A5;
                        }
                    }
                }
            Label_00A5:
                if (!flag)
                {
                    current = new CartGiftPackVirtualInfo();
                    current.RandNumber = info.RandNumber;
                    current.GiftPackBuyCount = info.BuyCount;
                    current.LeftStorageCount = info.LeftStorageCount;
                    current.CartList.Add(info);
                    cartGiftPackVirtualList.Add(current);
                }
                goto Label_018A;
            Label_00F8:
                if (info.FatherID == 0)
                {
                    CartCommonProductVirtualInfo item = new CartCommonProductVirtualInfo();
                    item.FatherCart = info;
                    cartCommonProductVirtualList.Add(item);
                }
                else
                {
                    foreach (CartCommonProductVirtualInfo info3 in cartCommonProductVirtualList)
                    {
                        if (info3.FatherCart.ID == info.FatherID)
                        {
                            info3.ChildCartList.Add(info);
                            break;
                        }
                    }
                }
            Label_018A:;
            }
            if (cartGiftPackVirtualList.Count > 0)
            {
                foreach (CartGiftPackVirtualInfo info2 in cartGiftPackVirtualList)
                {
                    int id = 0;
                    str = string.Empty;
                    str2 = string.Empty;
                    num2 = 0M;
                    num3 = 0;
                    foreach (CartInfo info in info2.CartList)
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
                        id = info.GiftPackID;
                    }
                    GiftPackInfo info4 = GiftPackBLL.ReadGiftPack(id);
                    info2.GiftPackID = id;
                    info2.GiftPackName = info4.Name;
                    info2.GiftPackPhoto = info4.Photo;
                    info2.StrProductID = str;
                    info2.StrCartID = str2;
                    info2.TotalProductWeight = num2;
                    info2.TotalSendPoint = num3;
                    info2.TotalPrice = info4.Price;
                }
            }
            if (cartCommonProductVirtualList.Count > 0)
            {
                foreach (CartCommonProductVirtualInfo info3 in cartCommonProductVirtualList)
                {
                    str = info3.FatherCart.ProductID.ToString();
                    str2 = info3.FatherCart.ID.ToString();
                    num2 = 0M;
                    num3 = 0;
                    foreach (CartInfo info in info3.ChildCartList)
                    {
                        str = str + "," + info.ProductID.ToString();
                        str2 = str2 + "," + info.ID.ToString();
                        num2 += info.ProductWeight;
                        num3 += info.SendPoint;
                    }
                    info3.StrProductID = str;
                    info3.StrCartID = str2;
                    CartInfo fatherCart = info3.FatherCart;
                    fatherCart.ProductWeight += num2;
                    CartInfo info5 = info3.FatherCart;
                    info5.SendPoint += num3;
                }
            }
        }

        public static bool IsProductInCart(int productID, string productName, int userID)
        {
            if (userID > 0) return dal.IsProductInCart(productID, productName, userID);
            return CartHelper.IsProductInCart(productID, productName);
        }

        public static List<CartInfo> ReadCartList(int userID)
        {
            if (userID > 0) return dal.ReadCartListByUser(userID);
            return CartHelper.ReadCart();
        }

        public static void StaticsCart(int userID, int gradeID)
        {
            List<CartGiftPackVirtualInfo> cartGiftPackVirtualList = new List<CartGiftPackVirtualInfo>();
            List<CartCommonProductVirtualInfo> cartCommonProductVirtualList = new List<CartCommonProductVirtualInfo>();
            List<ProductInfo> productList = new List<ProductInfo>();
            List<CartInfo> cartList = ReadCartList(userID);
            string strProductID = string.Empty;
            foreach (CartInfo info in cartList)
            {
                if (strProductID == string.Empty)
                    strProductID = info.ProductID.ToString();
                else
                    strProductID = strProductID + "," + info.ProductID.ToString();
            }
            if (strProductID != string.Empty)
            {
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.InProductID = strProductID;
                productList = ProductBLL.SearchProductList(productSearch);
            }
            List<MemberPriceInfo> memberPriceList = MemberPriceBLL.ReadMemberPriceByProductGrade(strProductID, gradeID);
            foreach (CartInfo info in cartList)
            {
                ProductInfo product = ProductBLL.ReadProductByProductList(productList, info.ProductID);
                info.ProductWeight = product.Weight;
                info.SendPoint = product.SendPoint;
                if (ShopConfig.ReadConfigInfo().ProductStorageType == 1)
                    info.LeftStorageCount = product.TotalStorageCount - product.OrderCount;
                else
                    info.LeftStorageCount = product.ImportVirtualStorageCount;
                info.ProductPrice = MemberPriceBLL.ReadCurrentMemberPrice(memberPriceList, gradeID, product);
            }
            HandlerCartList(cartList, ref cartGiftPackVirtualList, ref cartCommonProductVirtualList);
            int num = 0;
            decimal num2 = 0M;
            decimal num3 = 0M;
            foreach (CartGiftPackVirtualInfo info4 in cartGiftPackVirtualList)
            {
                num3 += info4.TotalProductWeight * info4.GiftPackBuyCount;
                num2 += info4.TotalPrice * info4.GiftPackBuyCount;
            }
            foreach (CartCommonProductVirtualInfo info5 in cartCommonProductVirtualList)
            {
                num3 += info5.FatherCart.ProductWeight * info5.FatherCart.BuyCount;
                num2 += info5.FatherCart.ProductPrice * info5.FatherCart.BuyCount;
            }
            foreach (CartInfo info in cartList)
            {
                if (info.FatherID == 0) num += info.BuyCount;
            }
            Sessions.ProductBuyCount = num;
            Sessions.ProductTotalPrice = num2;
            Sessions.ProductTotalWeight = num3;
        }

        public static void UpdateCart(string strID, int count, int userID)
        {
            if (userID > 0)
                dal.UpdateCart(strID, count);
            else
                CartHelper.UpdateCart(strID, count);
        }
    }
}

