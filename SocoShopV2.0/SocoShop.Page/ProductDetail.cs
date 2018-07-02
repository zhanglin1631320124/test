namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;
    using System.Collections.Generic;

    public class ProductDetail : CommonBasePage
    {
        protected List<AttributeRecordInfo> attributeRecordList = new List<AttributeRecordInfo>();
        protected decimal currentMemberPrice = 0M;
        protected int leftStorageCount = 0;
        protected List<MemberPriceInfo> memberPriceList = new List<MemberPriceInfo>();
        protected ProductInfo product = new ProductInfo();
        protected List<ArticleInfo> productArticleList = new List<ArticleInfo>();
        protected List<ProductPhotoInfo> productPhotoList = new List<ProductPhotoInfo>();
        protected List<TagsInfo> productTagsList = new List<TagsInfo>();
        protected List<StandardInfo> standardList = new List<StandardInfo>();
        protected List<StandardRecordInfo> standardRecordList = new List<StandardRecordInfo>();
        protected string standardRecordValueList = "|";
        protected string strHistoryProduct = string.Empty;
        protected List<MemberPriceInfo> tempMemberPriceList = new List<MemberPriceInfo>();
        protected List<ProductInfo> tempProductList = new List<ProductInfo>();
        protected List<UserGradeInfo> userGradeList = new List<UserGradeInfo>();

        protected override void PageLoad()
        {
            base.PageLoad();
            int queryString = RequestHelper.GetQueryString<int>("ID");
            this.product = ProductBLL.ReadProduct(queryString);
            if (this.product.IsSale == 0) ScriptHelper.Alert("该产品未上市，不能查看");
            ProductBLL.ChangeProductViewCount(queryString, 1);
            this.userGradeList = UserGradeBLL.ReadUserGradeCacheList();
            this.memberPriceList = MemberPriceBLL.ReadMemberPriceByProduct(queryString);
            this.currentMemberPrice = this.product.MarketPrice * UserGradeBLL.ReadUserGradeCache(base.GradeID).Discount / 100M;
            foreach (MemberPriceInfo info in this.memberPriceList)
            {
                if (info.GradeID == base.GradeID)
                {
                    this.currentMemberPrice = info.Price;
                    break;
                }
            }
            this.currentMemberPrice = Math.Round(this.currentMemberPrice, 2);
            ProductPhotoInfo item = new ProductPhotoInfo();
            item.Name = this.product.Name;
            item.Photo = this.product.Photo;
            this.productPhotoList.Add(item);
            this.productPhotoList.AddRange(ProductPhotoBLL.ReadProductPhotoByProduct(queryString));
            this.strHistoryProduct = base.Server.UrlDecode(CookiesHelper.ReadCookieValue("HistoryProduct"));
            string strProductID = (this.product.RelationProduct + "," + this.product.Accessory + "," + this.strHistoryProduct).Replace(",,", ",");
            if (strProductID.StartsWith(",")) strProductID = strProductID.Substring(1);
            if (strProductID.EndsWith(",")) strProductID = strProductID.Substring(0, strProductID.Length - 1);
            ProductSearchInfo productSearch = new ProductSearchInfo();
            productSearch.InProductID = strProductID;
            this.tempProductList = ProductBLL.SearchProductList(productSearch);
            if (strProductID != string.Empty) this.tempMemberPriceList = MemberPriceBLL.ReadMemberPriceByProductGrade(strProductID, base.GradeID);
            this.attributeRecordList = AttributeRecordBLL.ReadAttributeRecordByProduct(queryString);
            TagsSearchInfo tags = new TagsSearchInfo();
            tags.ProductID = queryString;
            this.productTagsList = TagsBLL.SearchTagsList(tags);
            if (this.product.RelationArticle != string.Empty)
            {
                ArticleSearchInfo articleSearch = new ArticleSearchInfo();
                articleSearch.InArticleID = this.product.RelationArticle;
                this.productArticleList = ArticleBLL.SearchArticleList(articleSearch);
            }
            this.standardRecordList = StandardRecordBLL.ReadStandardRecordByProduct(this.product.ID, this.product.StandardType);
            if (this.standardRecordList.Count > 0 && this.product.StandardType == 1)
            {
                string[] strArray = this.standardRecordList[0].StandardIDList.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    StandardInfo info6 = StandardBLL.ReadStandardCache(Convert.ToInt32(strArray[i]));
                    string[] strArray2 = info6.ValueList.Split(new char[] { ',' });
                    string[] strArray3 = info6.PhotoList.Split(new char[] { ',' });
                    string str2 = string.Empty;
                    string str3 = string.Empty;
                    for (int j = 0; j < strArray2.Length; j++)
                    {
                        foreach (StandardRecordInfo info7 in this.standardRecordList)
                        {
                            string[] strArray4 = info7.ValueList.Split(new char[] { ',' });
                            if (strArray2[j] == strArray4[i])
                            {
                                str2 = str2 + strArray2[j] + ",";
                                str3 = str3 + strArray3[j] + ",";
                                goto Label_043B;
                            }
                        }
                    Label_043B:;
                    }
                    if (str2 != string.Empty)
                    {
                        str2 = str2.Substring(0, str2.Length - 1);
                        str3 = str3.Substring(0, str3.Length - 1);
                    }
                    info6.ValueList = str2;
                    info6.PhotoList = str3;
                    this.standardList.Add(info6);
                }
                foreach (StandardRecordInfo info7 in this.standardRecordList)
                {
                    object standardRecordValueList = this.standardRecordValueList;
                    this.standardRecordValueList = string.Concat(new object[] { standardRecordValueList, info7.ProductID, ",", info7.ValueList, "|" });
                }
            }
            if (ShopConfig.ReadConfigInfo().ProductStorageType == 1)
                this.leftStorageCount = this.product.TotalStorageCount - this.product.OrderCount;
            else
                this.leftStorageCount = this.product.ImportVirtualStorageCount;
            base.Title = this.product.Name;
            base.Keywords = (this.product.Keywords == string.Empty) ? this.product.Name : this.product.Keywords;
            base.Description = (this.product.Summary == string.Empty) ? StringHelper.Substring(StringHelper.KillHTML(this.product.Introduction), 200) : this.product.Summary;
        }
    }
}

