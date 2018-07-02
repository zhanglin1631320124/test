namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class ArticleDetail : CommonBasePage
    {
        protected ArticleInfo article = new ArticleInfo();
        protected int articleClassID = 0;
        protected ProductInfo product = new ProductInfo();
        protected List<ArticleInfo> productArticleList = new List<ArticleInfo>();
        protected List<ArticleInfo> topArticleList = new List<ArticleInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.article = ArticleBLL.ReadArticle(queryString);
            if (this.article.ClassID != string.Empty)
            {
                this.article.ClassID = this.article.ClassID.Substring(1);
                this.articleClassID = Convert.ToInt32(this.article.ClassID.Substring(0, this.article.ClassID.IndexOf('|')));
            }
            ArticleSearchInfo article = new ArticleSearchInfo();
            switch (this.articleClassID)
            {
                case 1:
                {
                    article.ClassID = "|" + 1 + "|";
                    article.IsTop = 1;
                    int count = -2147483648;
                    this.topArticleList = ArticleBLL.SearchArticleList(1, 15, article, ref count);
                    break;
                }
                case 3:
                {
                    int id = RequestHelper.GetQueryString<int>("ProductID");
                    this.product = ProductBLL.ReadProduct(id);
                    if (this.product.RelationArticle != string.Empty)
                    {
                        article.InArticleID = this.product.RelationArticle;
                        this.productArticleList = ArticleBLL.SearchArticleList(article);
                    }
                    break;
                }
            }
            base.Title = this.article.Title;
            base.Keywords = (this.article.Keywords == string.Empty) ? this.article.Title : this.article.Keywords;
            base.Description = (this.article.Summary == string.Empty) ? StringHelper.Substring(StringHelper.KillHTML(this.article.Content), 200) : this.article.Summary;
        }
    }
}

