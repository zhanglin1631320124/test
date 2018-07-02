namespace SocoShop.Page.Controls
{
    using SocoShop.Common;
    using System;
    using System.Web.UI;

    public class Head : UserControl
    {
        private string description = string.Empty;
        private string keywords = string.Empty;
        private string title = string.Empty;

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

