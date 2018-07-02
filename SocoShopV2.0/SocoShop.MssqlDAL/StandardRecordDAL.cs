namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class StandardRecordDAL : IStandardRecord
    {
        public void AddStandardRecord(StandardRecordInfo standardRecord)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@standardIDList", SqlDbType.NVarChar), new SqlParameter("@valueList", SqlDbType.NVarChar), new SqlParameter("@groupTag", SqlDbType.NVarChar) };
            pt[0].Value = standardRecord.ProductID;
            pt[1].Value = standardRecord.StandardIDList;
            pt[2].Value = standardRecord.ValueList;
            pt[3].Value = standardRecord.GroupTag;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "AddStandardRecord", pt);
        }

        public void DeleteStandardRecord(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteStandardRecord", pt);
        }

        public void DeleteStandardRecordByProductID(string strProductID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strProductID", SqlDbType.NVarChar) };
            pt[0].Value = strProductID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteStandardRecordByProductID", pt);
        }

        public void PrepareStandardRecordModel(SqlDataReader dr, List<StandardRecordInfo> standardRecordList)
        {
            while (dr.Read())
            {
                StandardRecordInfo item = new StandardRecordInfo();
                item.ProductID = dr.GetInt32(0);
                item.StandardIDList = dr[1].ToString();
                item.ValueList = dr[2].ToString();
                item.GroupTag = dr[3].ToString();
                standardRecordList.Add(item);
            }
        }

        public List<StandardRecordInfo> ReadStandardRecordByProduct(int productID, int standardType)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@productID", SqlDbType.Int), new SqlParameter("@standardType", SqlDbType.Int) };
            pt[0].Value = productID;
            pt[1].Value = standardType;
            List<StandardRecordInfo> standardRecordList = new List<StandardRecordInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadStandardRecordByProduct", pt))
            {
                this.PrepareStandardRecordModel(reader, standardRecordList);
            }
            return standardRecordList;
        }
    }
}

