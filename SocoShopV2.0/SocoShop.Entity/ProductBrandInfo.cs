namespace SocoShop.Entity
{
    using System;

    public sealed class ProductBrandInfo
    {
        private string description = string.Empty;
        private int id;
        private int isTop;
        private string logo = string.Empty;
        private string name = string.Empty;
        private int orderID;
        private int productCount;
        private string url = string.Empty;

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public int IsTop
        {
            get
            {
                return this.isTop;
            }
            set
            {
                this.isTop = value;
            }
        }

        public string Logo
        {
            get
            {
                return this.logo;
            }
            set
            {
                this.logo = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public int OrderID
        {
            get
            {
                return this.orderID;
            }
            set
            {
                this.orderID = value;
            }
        }

        public int ProductCount
        {
            get
            {
                return this.productCount;
            }
            set
            {
                this.productCount = value;
            }
        }

        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }
    }
}

