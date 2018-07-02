namespace SocoShop.MssqlDAL
{
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ArticleClassDAL : IArticleClass
    {
        public int AddArticleClass(ArticleClassInfo articleClass)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@className", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText), new SqlParameter("@isSystem", SqlDbType.Int) };
            pt[0].Value = articleClass.FatherID;
            pt[1].Value = articleClass.OrderID;
            pt[2].Value = articleClass.ClassName;
            pt[3].Value = articleClass.Description;
            pt[4].Value = articleClass.IsSystem;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddArticleClass", pt));
        }

        public void DeleteArticleClass(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteArticleClass", pt);
        }

        public void MoveDownArticleClass(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "MoveDownArticleClass", pt);
        }

        public void MoveUpArticleClass(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int) };
            pt[0].Value = id;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "MoveUpArticleClass", pt);
        }

        public void PrepareArticleClassModel(SqlDataReader dr, List<ArticleClassInfo> articleClassList)
        {
            while (dr.Read())
            {
                ArticleClassInfo item = new ArticleClassInfo();
                item.ID = dr.GetInt32(0);
                item.FatherID = dr.GetInt32(1);
                item.OrderID = dr.GetInt32(2);
                item.ClassName = dr[3].ToString();
                item.Description = dr[4].ToString();
                item.IsSystem = dr.GetInt32(5);
                articleClassList.Add(item);
            }
        }

        public List<ArticleClassInfo> ReadArticleClassAllList()
        {
            List<ArticleClassInfo> articleClassList = new List<ArticleClassInfo>();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadArticleClassAllList"))
            {
                this.PrepareArticleClassModel(reader, articleClassList);
            }
            return articleClassList;
        }

        public void UpdateArticleClass(ArticleClassInfo articleClass)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@fatherID", SqlDbType.Int), new SqlParameter("@orderID", SqlDbType.Int), new SqlParameter("@className", SqlDbType.NVarChar), new SqlParameter("@description", SqlDbType.NText) };
            pt[0].Value = articleClass.ID;
            pt[1].Value = articleClass.FatherID;
            pt[2].Value = articleClass.OrderID;
            pt[3].Value = articleClass.ClassName;
            pt[4].Value = articleClass.Description;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateArticleClass", pt);
        }
    }
}

