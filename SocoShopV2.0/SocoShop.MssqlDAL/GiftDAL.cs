namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class GiftDAL : IGift
    {
        public int AddGift(GiftInfo gift)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText) };
            pt[0].Value = gift.Name;
            pt[1].Value = gift.Photo;
            pt[2].Value = gift.Description;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddGift", pt));
        }

        public void DeleteGift(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteGift", pt);
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, GiftSearchInfo giftSearch)
        {
            mssqlCondition.Add("[Name]", giftSearch.Name, ConditionType.Like);
            mssqlCondition.Add("[ID]", giftSearch.InGiftID, ConditionType.In);
        }

        public void PrepareGiftModel(SqlDataReader dr, List<GiftInfo> giftList)
        {
            while (dr.Read())
            {
                GiftInfo item = new GiftInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Photo = dr[2].ToString();
                item.Description = dr[3].ToString();
                giftList.Add(item);
            }
        }

        public GiftInfo ReadGift(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            GiftInfo info = new GiftInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadGift", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.Photo = reader[2].ToString();
                    info.Description = reader[3].ToString();
                }
            }
            return info;
        }

        public List<GiftInfo> SearchGiftList(GiftSearchInfo giftSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, giftSearch);
            List<GiftInfo> giftList = new List<GiftInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchGiftList", pt))
            {
                this.PrepareGiftModel(reader, giftList);
            }
            return giftList;
        }

        public List<GiftInfo> SearchGiftList(int currentPage, int pageSize, GiftSearchInfo giftSearch, ref int count)
        {
            List<GiftInfo> giftList = new List<GiftInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Gift";
            class2.Fields = "[ID],[Name],[Photo],[Description]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, giftSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareGiftModel(reader, giftList);
            }
            return giftList;
        }

        public void UpdateGift(GiftInfo gift)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText) };
            pt[0].Value = gift.ID;
            pt[1].Value = gift.Name;
            pt[2].Value = gift.Photo;
            pt[3].Value = gift.Description;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateGift", pt);
        }
    }
}

