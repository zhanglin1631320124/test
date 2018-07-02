namespace SocoShop.Common
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;

    public sealed class CartHelper
    {
        private static HttpCookie cartCookies = new HttpCookie(cookiesName);
        private static string cookiesName = "CookiesCart";
        private static Hashtable ht = new Hashtable();
        private static string secureKey = ShopConfig.ReadConfigInfo().SecureKey;

        public static int AddToCart(CartInfo cart)
        {
            Hashtable hashtable;
            object obj2;
            DecodeCart();
            int index = ht["productID"].ToString().Split(new char[] { '#' }).Length - 2;
            int num2 = 1;
            if (index > 0) num2 = Convert.ToInt32(ht["cartID"].ToString().Split(new char[] { '#' })[index]) + 1;
            (hashtable = ht)[obj2 = "cartID"] = hashtable[obj2] + num2.ToString() + "#";
            (hashtable = ht)[obj2 = "productID"] = hashtable[obj2] + cart.ProductID.ToString() + "#";
            (hashtable = ht)[obj2 = "productName"] = hashtable[obj2] + cart.ProductName.ToString() + "#";
            (hashtable = ht)[obj2 = "buyCount"] = hashtable[obj2] + cart.BuyCount.ToString() + "#";
            (hashtable = ht)[obj2 = "fatherID"] = hashtable[obj2] + cart.FatherID.ToString() + "#";
            (hashtable = ht)[obj2 = "randNumber"] = hashtable[obj2] + cart.RandNumber + "#";
            (hashtable = ht)[obj2 = "giftPackID"] = hashtable[obj2] + cart.GiftPackID.ToString() + "#";
            EncodeCart();
            HttpContext.Current.Response.Cookies.Add(cartCookies);
            return num2;
        }

        public static void ClearCart()
        {
            ht = new Hashtable();
            ht["cartID"] = "#";
            ht["productID"] = "#";
            ht["productName"] = "#";
            ht["buyCount"] = "#";
            ht["fatherID"] = "#";
            ht["randNumber"] = "#";
            ht["giftPackID"] = "#";
            EncodeCart();
            HttpContext.Current.Response.Cookies.Add(cartCookies);
        }

        private static void DecodeCart()
        {
            cartCookies = HttpContext.Current.Request.Cookies[cookiesName];
            if (cartCookies != null && cartCookies["Key"] != null)
            {
                if (StringHelper.Decode(cartCookies["Key"], secureKey) == ClientHelper.Agent)
                {
                    ht["cartID"] = StringHelper.Decode(cartCookies["cartID"], secureKey);
                    ht["productID"] = StringHelper.Decode(cartCookies["productID"], secureKey);
                    ht["productName"] = StringHelper.Decode(cartCookies["productName"], secureKey);
                    ht["buyCount"] = StringHelper.Decode(cartCookies["buyCount"], secureKey);
                    ht["fatherID"] = StringHelper.Decode(cartCookies["fatherID"], secureKey);
                    ht["randNumber"] = StringHelper.Decode(cartCookies["randNumber"], secureKey);
                    ht["giftPackID"] = StringHelper.Decode(cartCookies["giftPackID"], secureKey);
                }
                else
                    Init();
            }
            else
                Init();
        }

        public static void DeleteCart(string strID)
        {
            if (strID != string.Empty)
            {
                DecodeCart();
                strID = "#" + strID.Replace(",", "#") + "#";
                string[] strArray = ht["cartID"].ToString().Split(new char[] { '#' });
                string[] strArray2 = ht["productID"].ToString().Split(new char[] { '#' });
                string[] strArray3 = ht["productName"].ToString().Split(new char[] { '#' });
                string[] strArray4 = ht["buyCount"].ToString().Split(new char[] { '#' });
                string[] strArray5 = ht["fatherID"].ToString().Split(new char[] { '#' });
                string[] strArray6 = ht["randNumber"].ToString().Split(new char[] { '#' });
                string[] strArray7 = ht["giftPackID"].ToString().Split(new char[] { '#' });
                string str = "#";
                string str2 = "#";
                string str3 = "#";
                string str4 = "#";
                string str5 = "#";
                string str6 = "#";
                string str7 = "#";
                for (int i = 1; i < strArray.Length - 1; i++)
                {
                    if (strID.IndexOf("#" + strArray[i] + "#") == -1)
                    {
                        str = str + strArray[i] + "#";
                        str2 = str2 + strArray2[i] + "#";
                        str3 = str3 + strArray3[i] + "#";
                        str4 = str4 + strArray4[i] + "#";
                        str5 = str5 + strArray5[i] + "#";
                        str6 = str6 + strArray6[i] + "#";
                        str7 = str7 + strArray7[i] + "#";
                    }
                }
                ht["cartID"] = str;
                ht["productID"] = str2;
                ht["productName"] = str3;
                ht["buyCount"] = str4;
                ht["fatherID"] = str5;
                ht["randNumber"] = str6;
                ht["giftPackID"] = str7;
                EncodeCart();
                HttpContext.Current.Response.Cookies.Add(cartCookies);
            }
        }

        private static void EncodeCart()
        {
            cartCookies["cartID"] = StringHelper.Encode(ht["cartID"].ToString(), secureKey);
            cartCookies["productID"] = StringHelper.Encode(ht["productID"].ToString(), secureKey);
            cartCookies["productName"] = StringHelper.Encode(ht["productName"].ToString(), secureKey);
            cartCookies["buyCount"] = StringHelper.Encode(ht["buyCount"].ToString(), secureKey);
            cartCookies["fatherID"] = StringHelper.Encode(ht["fatherID"].ToString(), secureKey);
            cartCookies["randNumber"] = StringHelper.Encode(ht["randNumber"].ToString(), secureKey);
            cartCookies["giftPackID"] = StringHelper.Encode(ht["giftPackID"].ToString(), secureKey);
            cartCookies["Key"] = StringHelper.Encode(ClientHelper.Agent, secureKey);
        }

        public static void Init()
        {
            ht = new Hashtable();
            cartCookies = new HttpCookie(cookiesName);
            ht["cartID"] = "#";
            ht["productID"] = "#";
            ht["productName"] = "#";
            ht["buyCount"] = "#";
            ht["fatherID"] = "#";
            ht["randNumber"] = "#";
            ht["giftPackID"] = "#";
            EncodeCart();
            cartCookies.Expires = DateTime.Now.AddDays(7.0);
            HttpContext.Current.Response.Cookies.Add(cartCookies);
        }

        public static bool IsProductInCart(int productID, string productName)
        {
            DecodeCart();
            foreach (CartInfo info in ReadCart())
            {
                if (info.RandNumber == string.Empty && info.FatherID == 0 && info.ProductID == productID && info.ProductName == productName) return true;
            }
            return false;
        }

        public static List<CartInfo> ReadCart()
        {
            DecodeCart();
            List<CartInfo> list = new List<CartInfo>();
            string[] strArray = ht["cartID"].ToString().Split(new char[] { '#' });
            string[] strArray2 = ht["productID"].ToString().Split(new char[] { '#' });
            string[] strArray3 = ht["productName"].ToString().Split(new char[] { '#' });
            string[] strArray4 = ht["buyCount"].ToString().Split(new char[] { '#' });
            string[] strArray5 = ht["fatherID"].ToString().Split(new char[] { '#' });
            string[] strArray6 = ht["randNumber"].ToString().Split(new char[] { '#' });
            string[] strArray7 = ht["giftPackID"].ToString().Split(new char[] { '#' });
            for (int i = 1; i < strArray2.Length - 1; i++)
            {
                CartInfo item = new CartInfo();
                item.ID = Convert.ToInt32(strArray[i]);
                item.ProductID = Convert.ToInt32(strArray2[i]);
                item.ProductName = strArray3[i];
                item.BuyCount = Convert.ToInt32(strArray4[i]);
                item.FatherID = Convert.ToInt32(strArray5[i]);
                item.RandNumber = strArray6[i];
                item.GiftPackID = Convert.ToInt32(strArray7[i]);
                list.Add(item);
            }
            return list;
        }

        public static void UpdateCart(string strID, int count)
        {
            DecodeCart();
            strID = "#" + strID.Replace(",", "#") + "#";
            string[] strArray = ht["buyCount"].ToString().Split(new char[] { '#' });
            string[] strArray2 = ht["cartID"].ToString().Split(new char[] { '#' });
            string str = "#";
            for (int i = 1; i < strArray.Length - 1; i++)
            {
                if (strID.IndexOf("#" + strArray2[i] + "#") > -1)
                    str = str + count + "#";
                else
                    str = str + strArray[i] + "#";
            }
            ht["buyCount"] = str;
            EncodeCart();
            HttpContext.Current.Response.Cookies.Add(cartCookies);
        }
    }
}

