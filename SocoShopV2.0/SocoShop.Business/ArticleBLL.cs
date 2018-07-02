namespace SocoShop.Business
{
    using SkyCES.EntLib;
    using SocoShop.Common;
    using SocoShop.Entity;
    using SocoShop.IDAL;
    using System;
    using System.Collections.Generic;

    public sealed class ArticleBLL
    {
        private static readonly string cacheKey = CacheKey.ReadCacheKey("BottomList");
        private static readonly IArticle dal = FactoryHelper.Instance<IArticle>(Global.DataProvider, "ArticleDAL");
        public static readonly int TableID = UploadTable.ReadTableID("Article");

        public static int AddArticle(ArticleInfo article)
        {
            article.ID = dal.AddArticle(article);
            UploadBLL.UpdateUpload(TableID, 0, article.ID, Cookies.Admin.GetRandomNumber(false));
            if (article.ClassID.IndexOf("|" + 2 + "|") > -1) CacheHelper.Remove(cacheKey);
            return article.ID;
        }

        public static void DeleteArticle(string strID)
        {
            UploadBLL.DeleteUploadByRecordID(TableID, strID);
            dal.DeleteArticle(strID);
            CacheHelper.Remove(cacheKey);
        }

        public static ArticleInfo ReadArticle(int id)
        {
            return dal.ReadArticle(id);
        }

        public static List<ArticleInfo> ReadBottomList()
        {
            if (CacheHelper.Read(cacheKey) == null)
            {
                ArticleSearchInfo articleSearch = new ArticleSearchInfo();
                articleSearch.ClassID = "|" + 2 + "|";
                CacheHelper.Write(cacheKey, dal.SearchArticleList(articleSearch));
            }
            return (List<ArticleInfo>) CacheHelper.Read(cacheKey);
        }

        public static List<ArticleInfo> SearchArticleList(ArticleSearchInfo articleSearch)
        {
            return dal.SearchArticleList(articleSearch);
        }

        public static List<ArticleInfo> SearchArticleList(int currentPage, int pageSize, ArticleSearchInfo article, ref int count)
        {
            return dal.SearchArticleList(currentPage, pageSize, article, ref count);
        }

        public static void UpdateArticle(ArticleInfo article)
        {
            dal.UpdateArticle(article);
            CacheHelper.Remove(cacheKey);
            UploadBLL.UpdateUpload(TableID, 0, article.ID, Cookies.Admin.GetRandomNumber(false));
        }
    }
}

