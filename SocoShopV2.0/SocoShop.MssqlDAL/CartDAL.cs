namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class CartDAL : ICart
    {
        public int AddCart(CartInfo cart)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@productName", SqlDbType.NVarChar), new SqlParameter("@buyCount", SqlDbType.Int), new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@randNumber", SqlDbType.NVarChar), new SqlParameter("@giftPackID", SqlDbType.Int), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = cart.ProductID;
            pt[1].Value = cart.ProductName;
            pt[2].Value = cart.BuyCount;
            pt[3].Value = cart.FatherID;
            pt[4].Value = cart.RandNumber;
            pt[5].Value = cart.GiftPackID;
            pt[6].Value = cart.UserID;
            pt[7].Value = cart.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddCart", pt));
        }

        public void ClearCart(int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ClearCart", pt);
        }

        public void DeleteCart(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteCart", pt);
        }

        public bool IsProductInCart(int productID, string productName, int userID)
        {
            bool flag = false;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@productName", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = productID;
            pt[1].Value = productName;
            pt[2].Value = userID;
            object obj2 = ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "IsProductInCart", pt);
            if (obj2 != null && obj2 != DBNull.Value && Convert.ToUInt32(obj2) > 0) flag = true;
            return flag;
        }

        public void PrepareCartModel(SqlDataReader dr, List<CartInfo> cartList)
        {
            while (dr.Read())
            {
                CartInfo item = new CartInfo();
                item.ID = dr.GetInt32(0);
                item.ProductID = dr.GetInt32(1);
                item.ProductName = dr[2].ToString();
                item.BuyCount = dr.GetInt32(3);
                item.FatherID = dr.GetInt32(4);
                item.RandNumber = dr[5].ToString();
                item.GiftPackID = dr.GetInt32(6);
                item.UserID = dr.GetInt32(7);
                item.UserName = dr[8].ToString();
                cartList.Add(item);
            }
        }

        public string ReadCartIDList(string strID, int userID)
        {
            string str = string.Empty;
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadCartIDList", pt))
            {
                while (reader.Read())
                {
                    if (str == string.Empty)
                        str = reader.GetInt32(0).ToString();
                    else
                        str = str + "," + reader.GetInt32(0).ToString();
                }
            }
            return str;
        }

        public List<CartInfo> ReadCartListByUser(int userID)
        {
            List<CartInfo> cartList = new List<CartInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = userID;
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadCartListByUser", pt))
            {
                this.PrepareCartModel(reader, cartList);
            }
            return cartList;
        }

        public void UpdateCart(string strID, int count)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@buyCount", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = count;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateCart", pt);
        }
    }
}

