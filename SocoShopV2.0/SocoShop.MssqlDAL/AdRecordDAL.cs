namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class AdRecordDAL : IAdRecord
    {
        public int AddAdRecord(AdRecordInfo adRecord)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@adID", SqlDbType.Int), new SqlParameter("@iP", SqlDbType.NVarChar), new SqlParameter("@date", SqlDbType.DateTime), new SqlParameter("@page", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int), new SqlParameter("@userName", SqlDbType.NVarChar) };
            pt[0].Value = adRecord.AdID;
            pt[1].Value = adRecord.IP;
            pt[2].Value = adRecord.Date;
            pt[3].Value = adRecord.Page;
            pt[4].Value = adRecord.UserID;
            pt[5].Value = adRecord.UserName;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddAdRecord", pt));
        }

        public void DeleteAdRecord(string strID, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = strID;
            pt[1].Value = userID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAdRecord", pt);
        }

        public void DeleteAdRecordByAdID(string strAdID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strAdID", SqlDbType.NVarChar) };
            pt[0].Value = strAdID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAdRecordByAdID", pt);
        }

        public void PrepareAdRecordModel(SqlDataReader dr, List<AdRecordInfo> adRecordList)
        {
            while (dr.Read())
            {
                AdRecordInfo item = new AdRecordInfo();
                item.ID = dr.GetInt32(0);
                item.AdID = dr.GetInt32(1);
                item.IP = dr[2].ToString();
                item.Date = dr.GetDateTime(3);
                item.Page = dr[4].ToString();
                item.UserID = dr.GetInt32(5);
                item.UserName = dr[6].ToString();
                adRecordList.Add(item);
            }
        }

        public AdRecordInfo ReadAdRecord(int id, int userID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar), new SqlParameter("@userID", SqlDbType.Int) };
            pt[0].Value = id;
            pt[1].Value = userID;
            AdRecordInfo info = new AdRecordInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadAdRecord", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.AdID = reader.GetInt32(1);
                    info.IP = reader[2].ToString();
                    info.Date = reader.GetDateTime(3);
                    info.Page = reader[4].ToString();
                    info.UserID = reader.GetInt32(5);
                    info.UserName = reader[6].ToString();
                }
            }
            return info;
        }

        public List<AdRecordInfo> ReadAdRecordList(int currentPage, int pageSize, ref int count, int userID)
        {
            List<AdRecordInfo> adRecordList = new List<AdRecordInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "AdRecord";
            class2.Fields = "[ID],[AdID],[IP],[Date],[Page],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add("[UserID]", userID, ConditionType.Equal);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareAdRecordModel(reader, adRecordList);
            }
            return adRecordList;
        }

        public List<AdRecordInfo> ReadAdRecordList(int adID, int currentPage, int pageSize, ref int count, int userID)
        {
            List<AdRecordInfo> adRecordList = new List<AdRecordInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "AdRecord";
            class2.Fields = "[ID],[AdID],[IP],[Date],[Page],[UserID],[UserName]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add("[UserID]", userID, ConditionType.Equal);
            class2.MssqlCondition.Add("[AdID]", adID, ConditionType.Equal);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareAdRecordModel(reader, adRecordList);
            }
            return adRecordList;
        }

        public void UpdateAdRecord(AdRecordInfo adRecord)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@page", SqlDbType.NVarChar) };
            pt[0].Value = adRecord.ID;
            pt[1].Value = adRecord.Page;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateAdRecord", pt);
        }
    }
}

