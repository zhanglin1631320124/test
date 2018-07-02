namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class RegionDAL : IRegion
    {
        public int AddRegion(RegionInfo region)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@regionName", SqlDbType.NVarChar) };
            pt[0].Value = region.FatherID;
            pt[1].Value = region.OrderID;
            pt[2].Value = region.RegionName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddRegion", pt));
        }

        public void DeleteRegion(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteRegion", pt);
        }

        public void MoveDownRegion(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "MoveDownRegion", pt);
        }

        public void MoveUpRegion(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "MoveUpRegion", pt);
        }

        public void PrepareRegionModel(SqlDataReader dr, List<RegionInfo> regionList)
        {
            while (dr.Read())
            {
                RegionInfo item = new RegionInfo();
                item.ID = dr.GetInt32(0);
                item.FatherID = dr.GetInt32(1);
                item.OrderID = dr.GetInt32(2);
                item.RegionName = dr[3].ToString();
                regionList.Add(item);
            }
        }

        public List<RegionInfo> ReadRegionAllList()
        {
            List<RegionInfo> regionList = new List<RegionInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadRegionAllList"))
            {
                this.PrepareRegionModel(reader, regionList);
            }
            return regionList;
        }

        public void UpdateRegion(RegionInfo region)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@regionName", SqlDbType.NVarChar) };
            pt[0].Value = region.ID;
            pt[1].Value = region.FatherID;
            pt[2].Value = region.OrderID;
            pt[3].Value = region.RegionName;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateRegion", pt);
        }
    }
}

