namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ThemeActivityDAL : IThemeActivity
    {
        public int AddThemeActivity(ThemeActivityInfo themeActivity)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText), new SqlParameter("@css", SqlDbType.NText), new SqlParameter("@productGroup", SqlDbType.NVarChar), new SqlParameter("@style", SqlDbType.NVarChar) };
            pt[0].Value = themeActivity.Name;
            pt[1].Value = themeActivity.Photo;
            pt[2].Value = themeActivity.Description;
            pt[3].Value = themeActivity.Css;
            pt[4].Value = themeActivity.ProductGroup;
            pt[5].Value = themeActivity.Style;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddThemeActivity", pt));
        }

        public void DeleteThemeActivity(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteThemeActivity", pt);
        }

        public void PrepareThemeActivityModel(SqlDataReader dr, List<ThemeActivityInfo> themeActivityList)
        {
            while (dr.Read())
            {
                ThemeActivityInfo item = new ThemeActivityInfo();
                item.ID = dr.GetInt32(0);
                item.Name = dr[1].ToString();
                item.Photo = dr[2].ToString();
                item.Description = dr[3].ToString();
                item.Css = dr[4].ToString();
                item.ProductGroup = dr[5].ToString();
                item.Style = dr[6].ToString();
                themeActivityList.Add(item);
            }
        }

        public ThemeActivityInfo ReadThemeActivity(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ThemeActivityInfo info = new ThemeActivityInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadThemeActivity", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Name = reader[1].ToString();
                    info.Photo = reader[2].ToString();
                    info.Description = reader[3].ToString();
                    info.Css = reader[4].ToString();
                    info.ProductGroup = reader[5].ToString();
                    info.Style = reader[6].ToString();
                }
            }
            return info;
        }

        public List<ThemeActivityInfo> ReadThemeActivityList(int currentPage, int pageSize, ref int count)
        {
            List<ThemeActivityInfo> themeActivityList = new List<ThemeActivityInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "ThemeActivity";
            class2.Fields = "[ID],[Name],[Photo],[Description],[Css],[ProductGroup],[Style]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareThemeActivityModel(reader, themeActivityList);
            }
            return themeActivityList;
        }

        public void UpdateThemeActivity(ThemeActivityInfo themeActivity)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@name", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText), new SqlParameter("@css", SqlDbType.NText), new SqlParameter("@productGroup", SqlDbType.NVarChar), new SqlParameter("@style", SqlDbType.NVarChar) };
            pt[0].Value = themeActivity.ID;
            pt[1].Value = themeActivity.Name;
            pt[2].Value = themeActivity.Photo;
            pt[3].Value = themeActivity.Description;
            pt[4].Value = themeActivity.Css;
            pt[5].Value = themeActivity.ProductGroup;
            pt[6].Value = themeActivity.Style;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateThemeActivity", pt);
        }
    }
}

