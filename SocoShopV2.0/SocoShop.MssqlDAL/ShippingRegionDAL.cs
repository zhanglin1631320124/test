namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ShippingRegionDAL : IShippingRegion
    {
        public int AddShippingRegion(ShippingRegionInfo shippingRegion)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@shippingID", SqlDbType.Int), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@fixedMoeny", SqlDbType.Decimal), new SqlParameter("@firstMoney", SqlDbType.Decimal), new SqlParameter("@againMoney", SqlDbType.Decimal), new SqlParameter("@oneMoeny", SqlDbType.Decimal), new SqlParameter("@anotherMoeny", SqlDbType.Decimal) };
            pt[0].Value = shippingRegion.Name;
            pt[1].Value = shippingRegion.ShippingID;
            pt[2].Value = shippingRegion.RegionID;
            pt[3].Value = shippingRegion.FixedMoeny;
            pt[4].Value = shippingRegion.FirstMoney;
            pt[5].Value = shippingRegion.AgainMoney;
            pt[6].Value = shippingRegion.OneMoeny;
            pt[7].Value = shippingRegion.AnotherMoeny;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddShippingRegion", pt));
        }

        public void DeleteShippingRegion(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteShippingRegion", pt);
        }

        public void DeleteShippingRegionByShippingID(string strShippingID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strShippingID", SqlDbType.NVarChar) };
            pt[0].Value = strShippingID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteShippingRegionByShippingID", pt);
        }

        public void PrepareShippingRegionModel(SqlDataReader dr, List<ShippingRegionInfo> shippingRegionList)
        {
            while (dr.Read())
            {
                ShippingRegionInfo item = new ShippingRegionInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.ShippingID = dr.GetInt32(2);
                item.RegionID = dr[3].ToString();
                item.FixedMoeny = dr.GetDecimal(4);
                item.FirstMoney = dr.GetDecimal(5);
                item.AgainMoney = dr.GetDecimal(6);
                item.OneMoeny = dr.GetDecimal(7);
                item.AnotherMoeny = dr.GetDecimal(8);
                shippingRegionList.Add(item);
            }
        }

        public ShippingRegionInfo ReadShippingRegion(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShippingRegionInfo info = new ShippingRegionInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadShippingRegion", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.ShippingID = reader.GetInt32(2);
                    info.RegionID = reader[3].ToString();
                    info.FixedMoeny = reader.GetDecimal(4);
                    info.FirstMoney = reader.GetDecimal(5);
                    info.AgainMoney = reader.GetDecimal(6);
                    info.OneMoeny = reader.GetDecimal(7);
                    info.AnotherMoeny = reader.GetDecimal(8);
                }
            }
            return info;
        }

        public List<ShippingRegionInfo> ReadShippingRegionByShipping(string strShippingID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strShippingID", SqlDbType.NVarChar) };
            pt[0].Value = strShippingID;
            List<ShippingRegionInfo> shippingRegionList = new List<ShippingRegionInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadShippingRegionByShipping", pt))
            {
                this.PrepareShippingRegionModel(reader, shippingRegionList);
            }
            return shippingRegionList;
        }

        public void UpdateShippingRegion(ShippingRegionInfo shippingRegion)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@fixedMoeny", SqlDbType.Decimal), new SqlParameter("@firstMoney", SqlDbType.Decimal), new SqlParameter("@againMoney", SqlDbType.Decimal), new SqlParameter("@oneMoeny", SqlDbType.Decimal), new SqlParameter("@anotherMoeny", SqlDbType.Decimal) };
            pt[0].Value = shippingRegion.ID;
            pt[1].Value = shippingRegion.Name;
            pt[2].Value = shippingRegion.RegionID;
            pt[3].Value = shippingRegion.FixedMoeny;
            pt[4].Value = shippingRegion.FirstMoney;
            pt[5].Value = shippingRegion.AgainMoney;
            pt[6].Value = shippingRegion.OneMoeny;
            pt[7].Value = shippingRegion.AnotherMoeny;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateShippingRegion", pt);
        }
    }
}

