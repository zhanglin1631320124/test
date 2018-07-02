namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class ProductReply : CommonBasePage
    {
        protected ProductInfo product = new ProductInfo();
        protected ProductCommentInfo productComment = new ProductCommentInfo();
        protected List<ProductCommentInfo> productCommentList = new List<ProductCommentInfo>();
        protected List<MemberPriceInfo> tempMemberPriceList = new List<MemberPriceInfo>();
        protected List<ProductInfo> tempProductList = new List<ProductInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("CommentID");
            this.productComment = ProductCommentBLL.ReadProductComment(queryString, 0);
            this.product = ProductBLL.ReadProduct(this.productComment.ProductID);
            int count = -2147483648;
            int currentPage = 1;
            int pageSize = 5;
            ProductCommentSearchInfo productComment = new ProductCommentSearchInfo();
            productComment.ProductID = this.product.ID;
            this.productCommentList = ProductCommentBLL.SearchProductCommentList(currentPage, pageSize, productComment, ref count);
            string strProductID = base.Server.UrlDecode(CookiesHelper.ReadCookieValue("HistoryProduct"));
            if (strProductID != string.Empty)
            {
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.InProductID = strProductID;
                this.tempProductList = ProductBLL.SearchProductList(productSearch);
                this.tempMemberPriceList = MemberPriceBLL.ReadMemberPriceByProductGrade(strProductID, base.GradeID);
            }
        }
    }
}

