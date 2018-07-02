namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class FavorableActivityDAL : IFavorableActivity
    {
        public int AddFavorableActivity(FavorableActivityInfo favorableActivity)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@startDate", SqlDbType.DateTime), new SqlParameter("@endDate", SqlDbType.DateTime), new SqlParameter("@userGrade", SqlDbType.NVarChar), new SqlParameter("@orderProductMoney", SqlDbType.Decimal), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@shippingWay", SqlDbType.Int), new SqlParameter("@reduceWay", SqlDbType.Int), new SqlParameter("@reduceMoney", SqlDbType.Decimal), new SqlParameter("@reduceDiscount", SqlDbType.Decimal), new SqlParameter("@giftID", SqlDbType.NVarChar) };
            pt[0].Value = favorableActivity.Name;
            pt[1].Value = favorableActivity.Photo;
            pt[2].Value = favorableActivity.Content;
            pt[3].Value = favorableActivity.StartDate;
            pt[4].Value = favorableActivity.EndDate;
            pt[5].Value = favorableActivity.UserGrade;
            pt[6].Value = favorableActivity.OrderProductMoney;
            pt[7].Value = favorableActivity.RegionID;
            pt[8].Value = favorableActivity.ShippingWay;
            pt[9].Value = favorableActivity.ReduceWay;
            pt[10].Value = favorableActivity.ReduceMoney;
            pt[11].Value = favorableActivity.ReduceDiscount;
            pt[12].Value = favorableActivity.GiftID;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddFavorableActivity", pt));
        }

        public void DeleteFavorableActivity(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteFavorableActivity", pt);
        }

        public void PrepareFavorableActivityModel(SqlDataReader dr, List<FavorableActivityInfo> favorableActivityList)
        {
            while (dr.Read())
            {
                FavorableActivityInfo item = new FavorableActivityInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Photo = dr[2].ToString();
                item.Content = dr[3].ToString();
                item.StartDate = dr.GetDateTime(4);
                item.EndDate = dr.GetDateTime(5);
                item.UserGrade = dr[6].ToString();
                item.OrderProductMoney = dr.GetDecimal(7);
                item.RegionID = dr[8].ToString();
                item.ShippingWay = dr.GetInt32(9);
                item.ReduceWay = dr.GetInt32(10);
                item.ReduceMoney = dr.GetDecimal(11);
                item.ReduceDiscount = dr.GetDecimal(12);
                item.GiftID = dr[13].ToString();
                favorableActivityList.Add(item);
            }
        }

        public FavorableActivityInfo ReadFavorableActivity(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            FavorableActivityInfo info = new FavorableActivityInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadFavorableActivity", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.Photo = reader[2].ToString();
                    info.Content = reader[3].ToString();
                    info.StartDate = reader.GetDateTime(4);
                    info.EndDate = reader.GetDateTime(5);
                    info.UserGrade = reader[6].ToString();
                    info.OrderProductMoney = reader.GetDecimal(7);
                    info.RegionID = reader[8].ToString();
                    info.ShippingWay = reader.GetInt32(9);
                    info.ReduceWay = reader.GetInt32(10);
                    info.ReduceMoney = reader.GetDecimal(11);
                    info.ReduceDiscount = reader.GetDecimal(12);
                    info.GiftID = reader[13].ToString();
                }
            }
            return info;
        }

        public FavorableActivityInfo ReadFavorableActivity(DateTime startDate, DateTime endDate, int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@startDate", SqlDbType.DateTime), new SqlParameter("@endDate", SqlDbType.DateTime), new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = startDate;
            pt[1].Value = endDate;
            pt[2].Value = id;
            FavorableActivityInfo info = new FavorableActivityInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadFavorableActivityByDateTime", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.Photo = reader[2].ToString();
                    info.Content = reader[3].ToString();
                    info.StartDate = reader.GetDateTime(4);
                    info.EndDate = reader.GetDateTime(5);
                    info.UserGrade = reader[6].ToString();
                    info.OrderProductMoney = reader.GetDecimal(7);
                    info.RegionID = reader[8].ToString();
                    info.ShippingWay = reader.GetInt32(9);
                    info.ReduceWay = reader.GetInt32(10);
                    info.ReduceMoney = reader.GetDecimal(11);
                    info.ReduceDiscount = reader.GetDecimal(12);
                    info.GiftID = reader[13].ToString();
                }
            }
            return info;
        }

        public List<FavorableActivityInfo> ReadFavorableActivityList(int currentPage, int pageSize, ref int count)
        {
            List<FavorableActivityInfo> favorableActivityList = new List<FavorableActivityInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "FavorableActivity";
            class2.Fields = "[ID],[Name],[Photo],[Content],[StartDate],[EndDate],[UserGrade],[OrderProductMoney],[RegionID],[ShippingWay],[ReduceWay],[ReduceMoney],[ReduceDiscount],[GiftID]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareFavorableActivityModel(reader, favorableActivityList);
            }
            return favorableActivityList;
        }

        public void UpdateFavorableActivity(FavorableActivityInfo favorableActivity)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@startDate", SqlDbType.DateTime), new SqlParameter("@endDate", SqlDbType.DateTime), new SqlParameter("@userGrade", SqlDbType.NVarChar), new SqlParameter("@orderProductMoney", SqlDbType.Decimal), new SqlParameter("@regionID", SqlDbType.NVarChar), new SqlParameter("@shippingWay", SqlDbType.Int), new SqlParameter("@reduceWay", SqlDbType.Int), new SqlParameter("@reduceMoney", SqlDbType.Decimal), new SqlParameter("@reduceDiscount", SqlDbType.Decimal), new SqlParameter("@giftID", SqlDbType.NVarChar) };
            pt[0].Value = favorableActivity.ID;
            pt[1].Value = favorableActivity.Name;
            pt[2].Value = favorableActivity.Photo;
            pt[3].Value = favorableActivity.Content;
            pt[4].Value = favorableActivity.StartDate;
            pt[5].Value = favorableActivity.EndDate;
            pt[6].Value = favorableActivity.UserGrade;
            pt[7].Value = favorableActivity.OrderProductMoney;
            pt[8].Value = favorableActivity.RegionID;
            pt[9].Value = favorableActivity.ShippingWay;
            pt[10].Value = favorableActivity.ReduceWay;
            pt[11].Value = favorableActivity.ReduceMoney;
            pt[12].Value = favorableActivity.ReduceDiscount;
            pt[13].Value = favorableActivity.GiftID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateFavorableActivity", pt);
        }
    }
}

