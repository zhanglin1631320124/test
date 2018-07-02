namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class ProductAjax : AjaxBasePage
    {
        protected AjaxPagerClass ajaxPagerClass = new AjaxPagerClass();
        protected bool countPriceSingle = false;
        protected List<MemberPriceInfo> memberPriceList = new List<MemberPriceInfo>();
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected int productShowWay = 1;

        protected override void PageLoad()
        {
            base.PageLoad();
            if (CookiesHelper.ReadCookieValue("ProductShowWay") != string.Empty) this.productShowWay = Convert.ToInt32(CookiesHelper.ReadCookieValue("ProductShowWay"));
            int queryString = RequestHelper.GetQueryString<int>("Page");
            if (queryString < 1) queryString = 1;
            int pageSize = 20;
            if (this.productShowWay == 2) pageSize = 10;
            int count = 0;
            ProductSearchInfo productSearch = new ProductSearchInfo();
            productSearch.IsSale = 1;
            productSearch.ProductOrderType = CookiesHelper.ReadCookieValue("ProductOrderType");
            if (RequestHelper.GetQueryString<int>("SearchType") == 2)
            {
                string str = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("ClassID"));
                int num5 = -2147483648;
                if (str == num5.ToString())
                    str = string.Empty;
                else
                    str = "|" + str + "|";
                productSearch.ClassID = str;
                productSearch.Key = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("ProductName"));
                productSearch.BrandID = RequestHelper.GetQueryString<int>("BrandID");
                productSearch.Tags = StringHelper.SearchSafe(RequestHelper.GetQueryString<string>("Tags"));
            }
            else
            {
                productSearch.IsNew = RequestHelper.GetQueryString<int>("IsNew");
                productSearch.IsHot = RequestHelper.GetQueryString<int>("IsHot");
                productSearch.IsSpecial = RequestHelper.GetQueryString<int>("IsSpecial");
                productSearch.IsTop = RequestHelper.GetQueryString<int>("IsTop");
            }
            if (productSearch.ProductOrderType == "MemberPrice1" || productSearch.ProductOrderType == "MemberPrice2")
            {
                UserGradeInfo info2 = UserGradeBLL.ReadUserGradeCache(base.GradeID);
                if (productSearch.ProductOrderType == "MemberPrice2") productSearch.OrderType = OrderType.Asc;
                this.productList = ProductBLL.SearchProductList(queryString, pageSize, productSearch, ref count, info2.ID, info2.Discount / 100M);
            }
            else
            {
                this.productList = ProductBLL.SearchProductList(queryString, pageSize, productSearch, ref count);
                this.countPriceSingle = true;
                string strProductID = string.Empty;
                foreach (ProductInfo info3 in this.productList)
                {
                    if (strProductID == string.Empty)
                        strProductID = info3.ID.ToString();
                    else
                        strProductID = strProductID + "," + info3.ID.ToString();
                }
                if (strProductID != string.Empty) this.memberPriceList = MemberPriceBLL.ReadMemberPriceByProductGrade(strProductID, base.GradeID);
            }
            this.ajaxPagerClass.CurrentPage = queryString;
            this.ajaxPagerClass.PageSize = pageSize;
            this.ajaxPagerClass.Count = count;
        }
    }
}

