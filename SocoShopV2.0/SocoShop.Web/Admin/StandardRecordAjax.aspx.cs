namespace SocoShop.Web.Admin
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using SocoShop.Page;
    using System;
    using System.Collections.Generic;

    public partial class StandardRecordAjax : AdminBasePage
    {
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected string selectStandard = string.Empty;
        protected List<StandardInfo> standardList = new List<StandardInfo>();
        protected List<StandardRecordInfo> standardRecordList = new List<StandardRecordInfo>();
        protected int standardType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ClearCache();
            this.standardType = RequestHelper.GetQueryString<int>("StandardType");
            int queryString = RequestHelper.GetQueryString<int>("ProductID");
            this.standardList = StandardBLL.ReadStandardCacheList();
            if (queryString > 0)
            {
                this.standardRecordList = StandardRecordBLL.ReadStandardRecordByProduct(queryString, this.standardType);
                if (this.standardRecordList.Count > 0)
                {
                    this.selectStandard = this.standardRecordList[0].StandardIDList;
                    if (this.standardType == 2)
                    {
                        string groupTag = this.standardRecordList[0].GroupTag;
                        ProductSearchInfo productSearch = new ProductSearchInfo();
                        productSearch.InProductID = groupTag;
                        this.productList = ProductBLL.SearchProductList(productSearch);
                    }
                }
            }
        }

        protected string ReadProductName(List<ProductInfo> productList, int productID)
        {
            string str = string.Empty;
            foreach (ProductInfo info in productList)
            {
                if (info.ID == productID) return info.Name;
            }
            return str;
        }
    }
}

