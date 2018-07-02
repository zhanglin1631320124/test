namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class StandardDAL : IStandard
    {
        public int AddStandard(StandardInfo standard)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@displayTye", SqlDbType.Int), new SqlParameter("@valueList", SqlDbType.NVarChar), new SqlParameter("@photoList", SqlDbType.NVarChar) };
            pt[0].Value = standard.Name;
            pt[1].Value = standard.DisplayTye;
            pt[2].Value = standard.ValueList;
            pt[3].Value = standard.PhotoList;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddStandard", pt));
        }

        public void DeleteStandard(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteStandard", pt);
        }

        public void PrepareStandardModel(SqlDataReader dr, List<StandardInfo> standardList)
        {
            while (dr.Read())
            {
                StandardInfo item = new StandardInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.DisplayTye = dr.GetInt32(2);
                item.ValueList = dr[3].ToString();
                item.PhotoList = dr[4].ToString();
                standardList.Add(item);
            }
        }

        public List<StandardInfo> ReadStandardAllList()
        {
            List<StandardInfo> standardList = new List<StandardInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadStandardAllList"))
            {
                this.PrepareStandardModel(reader, standardList);
            }
            return standardList;
        }

        public void UpdateStandard(StandardInfo standard)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@displayTye", SqlDbType.Int), new SqlParameter("@valueList", SqlDbType.NVarChar), new SqlParameter("@photoList", SqlDbType.NVarChar) };
            pt[0].Value = standard.ID;
            pt[1].Value = standard.Name;
            pt[2].Value = standard.DisplayTye;
            pt[3].Value = standard.ValueList;
            pt[4].Value = standard.PhotoList;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateStandard", pt);
        }
    }
}

