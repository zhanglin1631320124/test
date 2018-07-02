namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class GiftPackDAL : IGiftPack
    {
        public int AddGiftPack(GiftPackInfo giftPack)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@startDate", SqlDbType.DateTime), new SqlParameter("@endDate", SqlDbType.DateTime), new SqlParameter("@price", SqlDbType.Decimal), new SqlParameter("@giftGroup", SqlDbType.NText) };
            pt[0].Value = giftPack.Name;
            pt[1].Value = giftPack.Photo;
            pt[2].Value = giftPack.StartDate;
            pt[3].Value = giftPack.EndDate;
            pt[4].Value = giftPack.Price;
            pt[5].Value = giftPack.GiftGroup;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddGiftPack", pt));
        }

        public void DeleteGiftPack(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteGiftPack", pt);
        }

        public void PrepareGiftPackModel(SqlDataReader dr, List<GiftPackInfo> giftPackList)
        {
            while (dr.Read())
            {
                GiftPackInfo item = new GiftPackInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Photo = dr[2].ToString();
                item.StartDate = dr.GetDateTime(3);
                item.EndDate = dr.GetDateTime(4);
                item.Price = dr.GetDecimal(5);
                item.GiftGroup = dr[6].ToString();
                giftPackList.Add(item);
            }
        }

        public GiftPackInfo ReadGiftPack(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            GiftPackInfo info = new GiftPackInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadGiftPack", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.Photo = reader[2].ToString();
                    info.StartDate = reader.GetDateTime(3);
                    info.EndDate = reader.GetDateTime(4);
                    info.Price = reader.GetDecimal(5);
                    info.GiftGroup = reader[6].ToString();
                }
            }
            return info;
        }

        public List<GiftPackInfo> ReadGiftPackList(int currentPage, int pageSize, ref int count)
        {
            List<GiftPackInfo> giftPackList = new List<GiftPackInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "GiftPack";
            class2.Fields = "[ID],[Name],[Photo],[StartDate],[EndDate],[Price],[GiftGroup]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareGiftPackModel(reader, giftPackList);
            }
            return giftPackList;
        }

        public void UpdateGiftPack(GiftPackInfo giftPack)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@startDate", SqlDbType.DateTime), new SqlParameter("@endDate", SqlDbType.DateTime), new SqlParameter("@price", SqlDbType.Decimal), new SqlParameter("@giftGroup", SqlDbType.NText) };
            pt[0].Value = giftPack.ID;
            pt[1].Value = giftPack.Name;
            pt[2].Value = giftPack.Photo;
            pt[3].Value = giftPack.StartDate;
            pt[4].Value = giftPack.EndDate;
            pt[5].Value = giftPack.Price;
            pt[6].Value = giftPack.GiftGroup;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateGiftPack", pt);
        }
    }
}

