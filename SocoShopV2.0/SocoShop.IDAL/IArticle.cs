namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IArticle
    {
        int AddArticle(ArticleInfo article);
        void DeleteArticle(string strID);
        ArticleInfo ReadArticle(int id);
        List<ArticleInfo> SearchArticleList(ArticleSearchInfo articleSearch);
        List<ArticleInfo> SearchArticleList(int currentPage, int pageSize, ArticleSearchInfo article, ref int count);
        void UpdateArticle(ArticleInfo article);
    }
}

