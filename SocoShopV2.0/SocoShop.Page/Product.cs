namespace SocoShop.Page
{
    using SkyCES.EntLib;
    using SocoShop.Business;
    using SocoShop.Common;
    using SocoShop.Entity;
    using System;

    public class Product : CommonBasePage
    {
        protected string relationSearch = string.Empty;
        protected string searchCondition = string.Empty;
        protected int searchType = 1;
        protected string showCondition = string.Empty;
        protected string showTitle = string.Empty;

        protected override void PageLoad()
        {
            base.PageLoad();
            this.SearchCondition();
        }

        protected void SearchCondition()
        {
            object relationSearch;
            string str4;
            int queryString = RequestHelper.GetQueryString<int>("ClassID");
            string str = RequestHelper.GetQueryString<string>("Keyword");
            int id = RequestHelper.GetQueryString<int>("BrandID");
            string str2 = RequestHelper.GetQueryString<string>("Tags");
            int num3 = RequestHelper.GetQueryString<int>("IsNew");
            int num4 = RequestHelper.GetQueryString<int>("IsHot");
            int num5 = RequestHelper.GetQueryString<int>("IsSpecial");
            int num6 = RequestHelper.GetQueryString<int>("IsTop");
            this.searchCondition = "ClassID=" + queryString.ToString() + "&ProductName=" + str + "&BrandID=" + id.ToString() + "&Tags=" + str2;
            if (queryString > 0)
            {
                ProductClassInfo info = ProductClassBLL.ReadProductClassCache(queryString);
                this.showCondition = "分类：<span>" + info.ClassName + "</span>";
                this.showTitle = info.ClassName;
                this.searchType = 2;
                int iD = info.ID;
                if (ProductClassBLL.ReadProductClassChildList(info.ID).Count == 0) iD = info.FatherID;
                foreach (ProductClassInfo info2 in ProductClassBLL.ReadProductClassChildList(iD))
                {
                    relationSearch = this.relationSearch;
                    this.relationSearch = string.Concat(new object[] { relationSearch, "<a href=\"/Product-C", info2.ID, ".aspx\">", info2.ClassName, "</a>" });
                }
            }
            if (str != string.Empty)
            {
                this.showCondition = this.showCondition + "关键字：<span>" + str + "</span>";
                this.showTitle = this.showTitle + str;
                this.searchType = 2;
                foreach (string str3 in ShopConfig.ReadConfigInfo().HotKeyword.Split(new char[] { ',' }))
                {
                    str4 = this.relationSearch;
                    this.relationSearch = str4 + "<a href=\"/Product/Keyword/" + base.Server.UrlEncode(str3) + ".aspx\">" + str3 + "</a>";
                }
            }
            if (id > 0)
            {
                this.showCondition = "品牌：<span>" + ProductBrandBLL.ReadProductBrandCache(id).Name + "</span>";
                this.showTitle = ProductBrandBLL.ReadProductBrandCache(id).Name;
                this.searchType = 2;
                foreach (ProductBrandInfo info3 in base.topProductBrandList)
                {
                    relationSearch = this.relationSearch;
                    this.relationSearch = string.Concat(new object[] { relationSearch, "<a href=\"/Product-B", info3.ID, ".aspx\">", info3.Name, "</a>" });
                }
            }
            if (str2 != string.Empty)
            {
                this.showCondition = "标签：<span>" + str2 + "</span>";
                this.showTitle = str2;
                this.searchType = 2;
                foreach (TagsInfo info4 in base.tagsList)
                {
                    str4 = this.relationSearch;
                    this.relationSearch = str4 + "<a href=\"/Product/Tags/" + base.Server.UrlEncode(info4.Word) + ".aspx\"  style=\"font-size:" + info4.Size.ToString() + "px; color:" + info4.Color + "\">" + info4.Word + "</a>";
                }
            }
            if (num3 == 1)
            {
                this.showCondition = "新品上市";
                this.showTitle = "新品上市";
                this.searchCondition = "IsNew=1";
            }
            if (num4 == 1)
            {
                this.showCondition = "热销商品";
                this.showTitle = "热销商品";
                this.searchCondition = "IsHot=1";
            }
            if (num5 == 1)
            {
                this.showCondition = "特价商品";
                this.showTitle = "特价商品";
                this.searchCondition = "IsSpecial=1";
            }
            if (num6 == 1)
            {
                this.showCondition = "推荐商品";
                this.showTitle = "推荐商品";
                this.searchCondition = "IsTop=1";
            }
            if (this.searchType == 1)
            {
                if (this.showCondition != string.Empty)
                    this.showCondition = "首页 > " + this.showCondition;
                else
                {
                    this.showCondition = "首页 > 全部商品";
                    this.showTitle = "全部商品";
                }
            }
            else
                this.showCondition = "您搜索的" + this.showCondition;
            base.Title = this.showTitle + " - 商品展示";
        }
    }
}

