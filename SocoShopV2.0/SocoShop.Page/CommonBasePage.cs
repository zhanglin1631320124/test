namespace SocoShop.Page
{
    using SocoShop.Business;
    using SocoShop.Common;
    using System;
    using System.Collections.Generic;
    using SocoShop.Entity;

    public abstract class CommonBasePage : BasePage
    {
        protected List<ProductClassInfo> allProductClassList = new List<ProductClassInfo>();
        protected List<ArticleInfo> bottomList = new List<ArticleInfo>();
        private string description = string.Empty;
        protected List<ArticleClassInfo> helpClassList = new List<ArticleClassInfo>();
        protected string hotKeyword = string.Empty;
        private string keywords = string.Empty;
        protected List<ProductBrandInfo> productBrandList = new List<ProductBrandInfo>();
        protected List<ProductClassInfo> productClassList = new List<ProductClassInfo>();
        protected List<TagsInfo> tagsList = new List<TagsInfo>();
        private string title = string.Empty;
        protected List<ProductBrandInfo> topProductBrandList = new List<ProductBrandInfo>();

        protected CommonBasePage()
        {
        }

        protected override void PageLoad()
        {
            this.helpClassList = ArticleClassBLL.ReadArticleClassChildList(4);
            this.productClassList = ProductClassBLL.ReadProductClassRootList();
            this.allProductClassList = ProductClassBLL.ReadProductClassNamedList();
            this.topProductBrandList = ProductBrandBLL.ReadProductBrandIsTopCacheList();
            this.productBrandList = ProductBrandBLL.ReadProductBrandCacheList();
            this.hotKeyword = ShopConfig.ReadConfigInfo().HotKeyword;
            this.bottomList = ArticleBLL.ReadBottomList();
            this.tagsList = TagsBLL.ReadHotTagsList();
        }

        public string Author
        {
            get
            {
                return ShopConfig.ReadConfigInfo().Author;
            }
        }

        public string Copyright
        {
            get
            {
                return ShopConfig.ReadConfigInfo().Copyright;
            }
        }

        public string Description
        {
            get
            {
                string description = this.description;
                if (description == string.Empty) description = ShopConfig.ReadConfigInfo().Description;
                return description;
            }
            set
            {
                this.description = value;
            }
        }

        public string Keywords
        {
            get
            {
                string keywords = this.keywords;
                if (keywords == string.Empty) keywords = ShopConfig.ReadConfigInfo().Keywords;
                return keywords;
            }
            set
            {
                this.keywords = value;
            }
        }

        public string Title
        {
            get
            {
                string title = ShopConfig.ReadConfigInfo().Title;
                if (this.title != string.Empty) title = this.title + " - " + ShopConfig.ReadConfigInfo().Title;
                return title;
            }
            set
            {
                this.title = value;
            }
        }
    }
}

