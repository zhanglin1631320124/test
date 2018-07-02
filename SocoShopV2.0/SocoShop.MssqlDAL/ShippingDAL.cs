namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ShippingDAL : IShipping
    {
        public int AddShipping(ShippingInfo shipping)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText), new SqlParameter("@isEnabled", SqlDbType.Int), new SqlParameter("@shippingType", SqlDbType.Int), new SqlParameter("@firstWeight", SqlDbType.Int), new SqlParameter("@againWeight", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int) };
            pt[0].Value = shipping.Name;
            pt[1].Value = shipping.Description;
            pt[2].Value = shipping.IsEnabled;
            pt[3].Value = shipping.ShippingType;
            pt[4].Value = shipping.FirstWeight;
            pt[5].Value = shipping.AgainWeight;
            pt[6].Value = shipping.OrderID;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddShipping", pt));
        }

        public void ChangeShippingOrder(ChangeAction action, int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@action", SqlDbType.NVarChar), new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = action.ToString();
            pt[1].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeShippingOrder", pt);
        }

        public void DeleteShipping(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteShipping", pt);
        }

        public void PrepareShippingModel(SqlDataReader dr, List<ShippingInfo> shippingList)
        {
            while (dr.Read())
            {
                ShippingInfo item = new ShippingInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Description = dr[2].ToString();
                item.IsEnabled = dr.GetInt32(3);
                item.ShippingType = dr.GetInt32(4);
                item.FirstWeight = dr.GetInt32(5);
                item.AgainWeight = dr.GetInt32(6);
                item.OrderID = dr.GetInt32(7);
                shippingList.Add(item);
            }
        }

        public List<ShippingInfo> ReadShippingAllList()
        {
            List<ShippingInfo> shippingList = new List<ShippingInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadShippingAllList"))
            {
                this.PrepareShippingModel(reader, shippingList);
            }
            return shippingList;
        }

        public void UpdateShipping(ShippingInfo shipping)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText), new SqlParameter("@isEnabled", SqlDbType.Int), new SqlParameter("@shippingType", SqlDbType.Int), new SqlParameter("@firstWeight", SqlDbType.Int), new SqlParameter("@againWeight", SqlDbType.Int) };
            pt[0].Value = shipping.ID;
            pt[1].Value = shipping.Name;
            pt[2].Value = shipping.Description;
            pt[3].Value = shipping.IsEnabled;
            pt[4].Value = shipping.ShippingType;
            pt[5].Value = shipping.FirstWeight;
            pt[6].Value = shipping.AgainWeight;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateShipping", pt);
        }
    }
}

