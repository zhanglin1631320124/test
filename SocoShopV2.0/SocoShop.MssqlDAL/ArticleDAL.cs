namespace SocoShop.MssqlDAL
{
    using SkyCES.EntLib;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ArticleDAL : IArticle
    {
        public int AddArticle(ArticleInfo article)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@classID", SqlDbType.NVarChar), new SqlParameter("@isTop", SqlDbType.Int), new SqlParameter("@author", SqlDbType.NVarChar), new SqlParameter("@resource", SqlDbType.NVarChar), new SqlParameter("@keywords", SqlDbType.NVarChar), new SqlParameter("@url", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@summary", SqlDbType.NText), new SqlParameter("@content", SqlDbType.NText), new SqlParameter("@date", SqlDbType.DateTime) };
            pt[0].Value = article.Title;
            pt[1].Value = article.ClassID;
            pt[2].Value = article.IsTop;
            pt[3].Value = article.Author;
            pt[4].Value = article.Resource;
            pt[5].Value = article.Keywords;
            pt[6].Value = article.Url;
            pt[7].Value = article.Photo;
            pt[8].Value = article.Summary;
            pt[9].Value = article.Content;
            pt[10].Value = article.Date;
            return Convert.ToInt32(ShopMssqlHelper.ExecuteScalar(ShopMssqlHelper.TablePrefix + "AddArticle", pt));
        }

        public void DeleteArticle(string strID)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@strID", SqlDbType.NVarChar) };
            pt[0].Value = strID;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "DeleteArticle", pt);
        }

        public void PrepareArticleModel(SqlDataReader dr, List<ArticleInfo> articleList)
        {
            while (dr.Read())
            {
                ArticleInfo item = new ArticleInfo();
                item.ID = dr.GetInt32(0);
                item.Title = dr[1].ToString();
                item.ClassID = dr[2].ToString();
                item.IsTop = dr.GetInt32(3);
                item.Author = dr[4].ToString();
                item.Resource = dr[5].ToString();
                item.Keywords = dr[6].ToString();
                item.Url = dr[7].ToString();
                item.Photo = dr[8].ToString();
                item.Summary = dr[9].ToString();
                item.Content = dr[10].ToString();
                item.Date = dr.GetDateTime(11);
                articleList.Add(item);
            }
        }

        public void PrepareCondition(MssqlCondition mssqlCondition, ArticleSearchInfo articleSearch)
        {
            mssqlCondition.Add("[Title]", articleSearch.Title, ConditionType.Like);
            mssqlCondition.Add("[ClassID]", articleSearch.ClassID, ConditionType.Like);
            mssqlCondition.Add("[IsTop]", articleSearch.IsTop, ConditionType.Equal);
            mssqlCondition.Add("[Author]", articleSearch.Author, ConditionType.Like);
            mssqlCondition.Add("[Resource]", articleSearch.Resource, ConditionType.Like);
            mssqlCondition.Add("[Keywords]", articleSearch.Keywords, ConditionType.Like);
            mssqlCondition.Add("[Date]", articleSearch.StartDate, ConditionType.MoreOrEqual);
            mssqlCondition.Add("[Date]", articleSearch.EndDate, ConditionType.LessOrEqual);
            mssqlCondition.Add("[ID]", articleSearch.InArticleID, ConditionType.In);
        }

        public ArticleInfo ReadArticle(int id)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar) };
            pt[0].Value = id;
            ArticleInfo info = new ArticleInfo();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "ReadArticle", pt))
            {
                if (reader.Read())
                {
                    info.ID = reader.GetInt32(0);
                    info.Title = reader[1].ToString();
                    info.ClassID = reader[2].ToString();
                    info.IsTop = reader.GetInt32(3);
                    info.Author = reader[4].ToString();
                    info.Resource = reader[5].ToString();
                    info.Keywords = reader[6].ToString();
                    info.Url = reader[7].ToString();
                    info.Photo = reader[8].ToString();
                    info.Summary = reader[9].ToString();
                    info.Content = reader[10].ToString();
                    info.Date = reader.GetDateTime(11);
                }
            }
            return info;
        }

        public List<ArticleInfo> SearchArticleList(ArticleSearchInfo articleSearch)
        {
            MssqlCondition mssqlCondition = new MssqlCondition();
            this.PrepareCondition(mssqlCondition, articleSearch);
            List<ArticleInfo> articleList = new List<ArticleInfo>();
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@condition", SqlDbType.NVarChar) };
            pt[0].Value = mssqlCondition.ToString();
            using (SqlDataReader reader = ShopMssqlHelper.ExecuteReader(ShopMssqlHelper.TablePrefix + "SearchArticleList", pt))
            {
                this.PrepareArticleModel(reader, articleList);
            }
            return articleList;
        }

        public List<ArticleInfo> SearchArticleList(int currentPage, int pageSize, ArticleSearchInfo articleSearch, ref int count)
        {
            List<ArticleInfo> articleList = new List<ArticleInfo>();
            ShopMssqlPagerClass class2 = new ShopMssqlPagerClass();
            class2.TableName = ShopMssqlHelper.TablePrefix + "Article";
            class2.Fields = "[ID],[Title],[ClassID],[IsTop],[Author],[Resource],[Keywords],[Url],[Photo],[Summary],[Content],[Date]";
            class2.CurrentPage = currentPage;
            class2.PageSize = pageSize;
            class2.OrderField = "[ID]";
            class2.OrderType = OrderType.Desc;
            this.PrepareCondition(class2.MssqlCondition, articleSearch);
            class2.Count = count;
            count = class2.Count;
            using (SqlDataReader reader = class2.ExecuteReader())
            {
                this.PrepareArticleModel(reader, articleList);
            }
            return articleList;
        }

        public void UpdateArticle(ArticleInfo article)
        {
            SqlParameter[] pt = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int), new SqlParameter("@title", SqlDbType.NVarChar), new SqlParameter("@classID", SqlDbType.NVarChar), new SqlParameter("@isTop", SqlDbType.Int), new SqlParameter("@author", SqlDbType.NVarChar), new SqlParameter("@resource", SqlDbType.NVarChar), new SqlParameter("@keywords", SqlDbType.NVarChar), new SqlParameter("@url", SqlDbType.NVarChar), new SqlParameter("@photo", SqlDbType.NVarChar), new SqlParameter("@summary", SqlDbType.NText), new SqlParameter("@content", SqlDbType.NText) };
            pt[0].Value = article.ID;
            pt[1].Value = article.Title;
            pt[2].Value = article.ClassID;
            pt[3].Value = article.IsTop;
            pt[4].Value = article.Author;
            pt[5].Value = article.Resource;
            pt[6].Value = article.Keywords;
            pt[7].Value = article.Url;
            pt[8].Value = article.Photo;
            pt[9].Value = article.Summary;
            pt[10].Value = article.Content;
            ShopMssqlHelper.ExecuteNonQuery(ShopMssqlHelper.TablePrefix + "UpdateArticle", pt);
        }
    }
}

