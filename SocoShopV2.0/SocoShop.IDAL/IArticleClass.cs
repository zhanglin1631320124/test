namespace SocoShop.IDAL
{
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public interface IArticleClass
    {
        int AddArticleClass(ArticleClassInfo articleClass);
        void DeleteArticleClass(int id);
        void MoveDownArticleClass(int id);
        void MoveUpArticleClass(int id);
        List<ArticleClassInfo> ReadArticleClassAllList();
        void UpdateArticleClass(ArticleClassInfo articleClass);
    }
}

