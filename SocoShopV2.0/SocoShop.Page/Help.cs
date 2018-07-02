namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class Help : CommonBasePage
    {
        protected List<ArticleInfo> articleList = new List<ArticleInfo>();
        protected int id = 0;

        protected override void PageLoad()
        {
            base.PageLoad();
            this.id = RequestHelper.GetQueryString<int>("ID");
            if (this.id == -2147483648 && base.helpClassList.Count > 0) this.id = ArticleClassBLL.ReadArticleClassChildList(base.helpClassList[0].ID)[0].ID;
            if (this.id > 0)
            {
                ArticleSearchInfo articleSearch = new ArticleSearchInfo();
                articleSearch.ClassID = "|" + this.id.ToString() + "|";
                this.articleList = ArticleBLL.SearchArticleList(articleSearch);
            }
            base.Title = "帮助中心";
        }
    }
}

