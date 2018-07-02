namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class AdDAL : IAd
    {
        public int AddAd(AdInfo ad)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@introduction", SqlDbType.NText), new SqlParameter("@adClass", SqlDbType.Int), new SqlParameter("@display", SqlDbType.NText), new SqlParameter("@width", SqlDbType.Int), new SqlParameter("@height", SqlDbType.Int), new SqlParameter("@url", SqlDbType.NVarChar), new SqlParameter("@startDate", SqlDbType.DateTime), new SqlParameter("@endDate", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.NVarChar), new SqlParameter("@clickCount", SqlDbType.Int), new SqlParameter("@isEnabled", SqlDbType.Int) };
            pt[0].Value = ad.Title;
            pt[1].Value = ad.Introduction;
            pt[2].Value = ad.AdClass;
            pt[3].Value = ad.Display;
            pt[4].Value = ad.Width;
            pt[5].Value = ad.Height;
            pt[6].Value = ad.Url;
            pt[7].Value = ad.StartDate;
            pt[8].Value = ad.EndDate;
            pt[9].Value = ad.Remark;
            pt[10].Value = ad.ClickCount;
            pt[11].Value = ad.IsEnabled;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddAd", pt));
        }

        public void ChangeAdCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAdCount", pt);
        }

        public void ChangeAdCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeAdCountByGeneral", pt);
        }

        public void DeleteAd(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteAd", pt);
        }

        public void PrepareAdModel(SqlDataReader dr, List<AdInfo> adList)
        {
            while (dr.Read())
            {
                AdInfo item = new AdInfo();
                item.ID = dr.GetInt32(0);
                item.Title = dr[1].ToString();
                item.Introduction = dr[2].ToString();
                item.AdClass = dr.GetInt32(3);
                item.Display = dr[4].ToString();
                item.Width = dr.GetInt32(5);
                item.Height = dr.GetInt32(6);
                item.Url = dr[7].ToString();
                item.StartDate = dr.GetDateTime(8);
                item.EndDate = dr.GetDateTime(9);
                item.Remark = dr[10].ToString();
                item.ClickCount = dr.GetInt32(11);
                item.IsEnabled = dr.GetInt32(12);
                adList.Add(item);
            }
        }

        public AdInfo ReadAd(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar) };
            pt[0].Value = id;
            AdInfo info = new AdInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadAd", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Title = reader[1].ToString();
                    info.Introduction = reader[2].ToString();
                    info.AdClass = reader.GetInt32(3);
                    info.Display = reader[4].ToString();
                    info.Width = reader.GetInt32(5);
                    info.Height = reader.GetInt32(6);
                    info.Url = reader[7].ToString();
                    info.StartDate = reader.GetDateTime(8);
                    info.EndDate = reader.GetDateTime(9);
                    info.Remark = reader[10].ToString();
                    info.ClickCount = reader.GetInt32(11);
                    info.IsEnabled = reader.GetInt32(12);
                }
            }
            return info;
        }

        public List<AdInfo> ReadAdList(int currentPage, int pageSize, ref int count)
        {
            List<AdInfo> adList = new List<AdInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Ad";
            class2.Fields = "[ID],[Title],[Introduction],[AdClass],[Display],[Width],[Height],[Url],[StartDate],[EndDate],[Remark],[ClickCount],[IsEnabled]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareAdModel(reader, adList);
            }
            return adList;
        }

        public List<AdInfo> ReadAdList(int classID, int currentPage, int pageSize, ref int count)
        {
            List<AdInfo> adList = new List<AdInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Ad";
            class2.Fields = "[ID],[Title],[Introduction],[AdClass],[Display],[Width],[Height],[Url],[StartDate],[EndDate],[Remark],[ClickCount],[IsEnabled]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.MssqlCondition.Add("[AdClass]", classID, ConditionType.Equal);
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareAdModel(reader, adList);
            }
            return adList;
        }

        public void UpdateAd(AdInfo ad)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@introduction", SqlDbType.NText), new SqlParameter("@adClass", SqlDbType.Int), new SqlParameter("@display", SqlDbType.NText), new SqlParameter("@width", SqlDbType.Int), new SqlParameter("@height", SqlDbType.Int), new SqlParameter("@url", SqlDbType.NVarChar), new SqlParameter("@startDate", SqlDbType.DateTime), new SqlParameter("@endDate", SqlDbType.DateTime), new SqlParameter("@remark", SqlDbType.NVarChar), new SqlParameter("@isEnabled", SqlDbType.Int) };
            pt[0].Value = ad.ID;
            pt[1].Value = ad.Title;
            pt[2].Value = ad.Introduction;
            pt[3].Value = ad.AdClass;
            pt[4].Value = ad.Display;
            pt[5].Value = ad.Width;
            pt[6].Value = ad.Height;
            pt[7].Value = ad.Url;
            pt[8].Value = ad.StartDate;
            pt[9].Value = ad.EndDate;
            pt[10].Value = ad.Remark;
            pt[11].Value = ad.IsEnabled;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateAd", pt);
        }
    }
}

