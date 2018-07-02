namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class GiftPackDetail : CommonBasePage
    {
        protected string[] countArray = new string[0];
        protected GiftPackInfo giftPack = new GiftPackInfo();
        protected string[] nameArray = new string[0];
        protected string[] productArray = new string[0];
        protected List<ProductInfo> productList = new List<ProductInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.giftPack = GiftPackBLL.ReadGiftPack(queryString);
            if (this.giftPack.GiftGroup != string.Empty)
            {
                string str = string.Empty;
                int length = this.giftPack.GiftGroup.Split(new char[] { '#' }).Length;
                this.nameArray = new string[length];
                this.countArray = new string[length];
                this.productArray = new string[length];
                for (int i = 0; i < length; i++)
                {
                    string[] strArray = this.giftPack.GiftGroup.Split(new char[] { '#' })[i].Split(new char[] { '|' });
                    this.nameArray[i] = strArray[0];
                    this.countArray[i] = strArray[1];
                    this.productArray[i] = strArray[2];
                    if (strArray[2] != string.Empty) str = str + strArray[2] + ",";
                }
                if (str != string.Empty)
                {
                    str = str.Substring(0, str.Length - 1);
                    ProductSearchInfo productSearch = new ProductSearchInfo();
                    productSearch.InProductID = str;
                    this.productList = ProductBLL.SearchProductList(productSearch);
                }
            }
            base.Title = this.giftPack.Name;
        }
    }
}

