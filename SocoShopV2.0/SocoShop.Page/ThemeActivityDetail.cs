namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class ThemeActivityDetail : CommonBasePage
    {
        protected List<MemberPriceInfo> memberPriceList = new List<MemberPriceInfo>();
        protected string[] productGroupArray = new string[0];
        protected List<ProductInfo> productList = new List<ProductInfo>();
        protected string[] styleArray;
        protected ThemeActivityInfo themeActivity = new ThemeActivityInfo();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.themeActivity = ThemeActivityBLL.ReadThemeActivity(queryString);
            this.styleArray = this.themeActivity.Style.Split(new char[] { '|' });
            if (this.themeActivity.ProductGroup != string.Empty)
            {
                string strProductID = string.Empty;
                this.productGroupArray = this.themeActivity.ProductGroup.Split(new char[] { '#' });
                for (int i = 0; i < this.productGroupArray.Length; i++)
                {
                    if (this.productGroupArray[i].Split(new char[] { '|' })[2] != string.Empty)
                    {
                        if (strProductID == string.Empty)
                            strProductID = this.productGroupArray[i].Split(new char[] { '|' })[2];
                        else
                            strProductID = strProductID + "," + this.productGroupArray[i].Split(new char[] { '|' })[2];
                    }
                }
                if (strProductID != string.Empty)
                {
                    ProductSearchInfo productSearch = new ProductSearchInfo();
                    productSearch.InProductID = strProductID;
                    this.productList = ProductBLL.SearchProductList(productSearch);
                    this.memberPriceList = MemberPriceBLL.ReadMemberPriceByProductGrade(strProductID, base.GradeID);
                }
            }
            base.Title = this.themeActivity.Name;
        }
    }
}

