namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class BrandDetail : CommonBasePage
    {
        protected List<MemberPriceInfo> memberPriceList = new List<MemberPriceInfo>();
        protected ProductBrandInfo productBrand = new ProductBrandInfo();
        protected List<ProductInfo> productList = new List<ProductInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.productBrand = ProductBrandBLL.ReadProductBrandCache(queryString);
            ProductSearchInfo productSearch = new ProductSearchInfo();
            productSearch.BrandID = queryString;
            productSearch.IsTop = 1;
            this.productList = ProductBLL.SearchProductList(productSearch);
            string strProductID = string.Empty;
            foreach (ProductInfo info2 in this.productList)
            {
                if (strProductID == string.Empty)
                    strProductID = info2.ID.ToString();
                else
                    strProductID = strProductID + "," + info2.ID.ToString();
            }
            if (strProductID != string.Empty) this.memberPriceList = MemberPriceBLL.ReadMemberPriceByProductGrade(strProductID, base.GradeID);
            base.Title = this.productBrand.Name;
        }
    }
}

