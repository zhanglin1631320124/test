namespace SocoShop.Page
{
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class Default : CommonBasePage
    {
        protected List<ProductInfo> hotProductList = new List<ProductInfo>();
        protected List<MemberPriceInfo> memberPriceList = new List<MemberPriceInfo>();
        protected List<ProductInfo> newProductList = new List<ProductInfo>();
        protected List<ArticleInfo> newsList = new List<ArticleInfo>();
        protected List<LinkInfo> pictureLinkList = new List<LinkInfo>();
        protected List<ProductInfo> specialProductList = new List<ProductInfo>();
        protected List<LinkInfo> textLinkList = new List<LinkInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            ArticleSearchInfo article = new ArticleSearchInfo();
            article.ClassID = "|" + 1.ToString() + "|";
            int count = -2147483648;
            this.newsList = ArticleBLL.SearchArticleList(1, 7, article, ref count);
            ProductSearchInfo product = new ProductSearchInfo();
            product.IsNew = 1;
            product.IsTop = 1;
            product.IsSale = 1;
            count = -2147483648;
            this.newProductList = ProductBLL.SearchProductList(1, 5, product, ref count);
            product = new ProductSearchInfo();
            product.IsHot = 1;
            product.IsTop = 1;
            product.IsSale = 1;
            count = -2147483648;
            this.hotProductList = ProductBLL.SearchProductList(1, 10, product, ref count);
            product = new ProductSearchInfo();
            product.IsSpecial = 1;
            product.IsTop = 1;
            product.IsSale = 1;
            count = -2147483648;
            this.specialProductList = ProductBLL.SearchProductList(1, 10, product, ref count);
            List<ProductInfo> list = new List<ProductInfo>();
            list.AddRange(this.newProductList);
            list.AddRange(this.hotProductList);
            list.AddRange(this.specialProductList);
            string strProductID = string.Empty;
            foreach (ProductInfo info3 in list)
            {
                if (strProductID == string.Empty)
                    strProductID = info3.ID.ToString();
                else
                    strProductID = strProductID + "," + info3.ID.ToString();
            }
            if (strProductID != string.Empty) this.memberPriceList = MemberPriceBLL.ReadMemberPriceByProductGrade(strProductID, base.GradeID);
            this.textLinkList = LinkBLL.ReadLinkCacheListByClass(1);
            this.pictureLinkList = LinkBLL.ReadLinkCacheListByClass(2);
        }
    }
}

