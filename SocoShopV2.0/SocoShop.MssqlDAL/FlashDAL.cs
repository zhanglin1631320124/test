namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class FlashDAL : IFlash
    {
        public int AddFlash(FlashInfo flash)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@introduce", SqlDbType.NText), new SqlParameter("@width", SqlDbType.Int), new SqlParameter("@height", SqlDbType.Int), new SqlParameter("@photoCount", SqlDbType.Int) };
            pt[0].Value = flash.Title;
            pt[1].Value = flash.Introduce;
            pt[2].Value = flash.Width;
            pt[3].Value = flash.Height;
            pt[4].Value = flash.PhotoCount;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddFlash", pt));
        }

        public void ChangeFlashCount(int id, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = id;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeFlashCount", pt);
        }

        public void ChangeFlashCountByGeneral(string strID, ChangeAction action)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar), new SqlParameter("@action", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            pt[1].Value = action.ToString();
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "ChangeFlashCountByGeneral", pt);
        }

        public void DeleteFlash(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteFlash", pt);
        }

        public void PrepareFlashModel(SqlDataReader dr, List<FlashInfo> flashList)
        {
            while (dr.Read())
            {
                FlashInfo item = new FlashInfo();
                item.ID = dr.GetInt32(0);
                item.Title = dr[1].ToString();
                item.Introduce = dr[2].ToString();
                item.Width = dr.GetInt32(3);
                item.Height = dr.GetInt32(4);
                item.PhotoCount = dr.GetInt32(5);
                flashList.Add(item);
            }
        }

        public FlashInfo ReadFlash(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar) };
            pt[0].Value = id;
            FlashInfo info = new FlashInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadFlash", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Title = reader[1].ToString();
                    info.Introduce = reader[2].ToString();
                    info.Width = reader.GetInt32(3);
                    info.Height = reader.GetInt32(4);
                    info.PhotoCount = reader.GetInt32(5);
                }
            }
            return info;
        }

        public List<FlashInfo> ReadFlashList(int currentPage, int pageSize, ref int count)
        {
            List<FlashInfo> flashList = new List<FlashInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Flash";
            class2.Fields = "[ID],[Title],[Introduce],[Width],[Height],[PhotoCount]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareFlashModel(reader, flashList);
            }
            return flashList;
        }

        public void UpdateFlash(FlashInfo flash)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@introduce", SqlDbType.NText), new SqlParameter("@width", SqlDbType.Int), new SqlParameter("@height", SqlDbType.Int) };
            pt[0].Value = flash.ID;
            pt[1].Value = flash.Title;
            pt[2].Value = flash.Introduce;
            pt[3].Value = flash.Width;
            pt[4].Value = flash.Height;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateFlash", pt);
        }
    }
}

