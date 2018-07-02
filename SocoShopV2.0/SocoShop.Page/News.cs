namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class News : CommonBasePage
    {
        protected List<ArticleInfo> articleList = new List<ArticleInfo>();
        protected CommonPagerClass commonPagerClass = new CommonPagerClass();
        protected List<ArticleInfo> topArticleList = new List<ArticleInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            ArticleSearchInfo article = new ArticleSearchInfo();
            article.ClassID = "|" + 1 + "|";
            article.IsTop = 1;
            int count = -2147483648;
            this.topArticleList = ArticleBLL.SearchArticleList(1, 15, article, ref count);
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 20;
            count = 0;
            article.ClassID = "|" + 1 + "|";
            this.articleList = ArticleBLL.SearchArticleList(queryString, pageSize, article, ref count);
            this.commonPagerClass.CurrentPage = queryString;
            this.commonPagerClass.PageSize = pageSize;
            this.commonPagerClass.Count = count;
            base.Title = "新闻资讯";
        }
    }
}

