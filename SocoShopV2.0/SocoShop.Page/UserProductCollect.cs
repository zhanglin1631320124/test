namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class UserProductCollect : UserBasePage
    {
        protected CommonPagerClass commonPagerClass = new CommonPagerClass();
        protected List<ProductCollectInfo> productCollectList = new List<ProductCollectInfo>();
        protected List<ProductInfo> productList = new List<ProductInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            if (RequestHelper.GetQueryString<string>("Action") == "Delete")
            {
                ProductCollectBLL.DeleteProductCollect(RequestHelper.GetQueryString<string>("ID"), base.UserID);
                ResponseHelper.Write("ok");
                ResponseHelper.End();
            }
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 20;
            int count = 0;
            this.productCollectList = ProductCollectBLL.ReadProductCollectList(queryString, pageSize, ref count, base.UserID);
            this.commonPagerClass.CurrentPage = queryString;
            this.commonPagerClass.PageSize = pageSize;
            this.commonPagerClass.Count = count;
            string str3 = string.Empty;
            foreach (ProductCollectInfo info in this.productCollectList)
            {
                if (str3 == string.Empty)
                    str3 = info.ProductID.ToString();
                else
                    str3 = str3 + "," + info.ProductID.ToString();
            }
            if (str3 != string.Empty)
            {
                ProductSearchInfo productSearch = new ProductSearchInfo();
                productSearch.InProductID = str3;
                this.productList = ProductBLL.SearchProductList(productSearch);
            }
        }
    }
}

