namespace SocoShop.Entity
{
    using SkyCES.EntLib;
    using System;

    public sealed class ProductSearchInfo
    {
        private string aliasName = string.Empty;
        private int brandID = -2147483648;
        private string classID = string.Empty;
        private DateTime endAddDate = DateTime.MinValue;
        private string inProductID = string.Empty;
        private int isHot = -2147483648;
        private int isNew = -2147483648;
        private int isSale = -2147483648;
        private int isSpecial = -2147483648;
        private int isTaobao = -2147483648;
        private int isTop = -2147483648;
        private string key = string.Empty;
        private string keywords = string.Empty;
        private string name = string.Empty;
        private string notInProductID = string.Empty;
        private SkyCES.EntLib.OrderType orderType = SkyCES.EntLib.OrderType.Desc;
        private string productNumber = string.Empty;
        private string productOrderType = string.Empty;
        private string spelling = string.Empty;
        private int standardType = -2147483648;
        private DateTime startAddDate = DateTime.MinValue;
        private int storageAnalyse = -2147483648;
        private string tags = string.Empty;

        public string AliasName
        {
            get
            {
                return this.aliasName;
            }
            set
            {
                this.aliasName = value;
            }
        }

        public int BrandID
        {
            get
            {
                return this.brandID;
            }
            set
            {
                this.brandID = value;
            }
        }

        public string ClassID
        {
            get
            {
                return this.classID;
            }
            set
            {
                this.classID = value;
            }
        }

        public DateTime EndAddDate
        {
            get
            {
                return this.endAddDate;
            }
            set
            {
                this.endAddDate = value;
            }
        }

        public string InProductID
        {
            get
            {
                return this.inProductID;
            }
            set
            {
                this.inProductID = value;
            }
        }

        public int IsHot
        {
            get
            {
                return this.isHot;
            }
            set
            {
                this.isHot = value;
            }
        }

        public int IsNew
        {
            get
            {
                return this.isNew;
            }
            set
            {
                this.isNew = value;
            }
        }

        public int IsSale
        {
            get
            {
                return this.isSale;
            }
            set
            {
                this.isSale = value;
            }
        }

        public int IsSpecial
        {
            get
            {
                return this.isSpecial;
            }
            set
            {
                this.isSpecial = value;
            }
        }

        public int IsTaobao
        {
            get
            {
                return this.isTaobao;
            }
            set
            {
                this.isTaobao = value;
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

        public string Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
            }
        }

        public string Keywords
        {
            get
            {
                return this.keywords;
            }
            set
            {
                this.keywords = value;
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

        public string NotInProductID
        {
            get
            {
                return this.notInProductID;
            }
            set
            {
                this.notInProductID = value;
            }
        }

        public SkyCES.EntLib.OrderType OrderType
        {
            get
            {
                return this.orderType;
            }
            set
            {
                this.orderType = value;
            }
        }

        public string ProductNumber
        {
            get
            {
                return this.productNumber;
            }
            set
            {
                this.productNumber = value;
            }
        }

        public string ProductOrderType
        {
            get
            {
                return this.productOrderType;
            }
            set
            {
                this.productOrderType = value;
            }
        }

        public string Spelling
        {
            get
            {
                return this.spelling;
            }
            set
            {
                this.spelling = value;
            }
        }

        public int StandardType
        {
            get
            {
                return this.standardType;
            }
            set
            {
                this.standardType = value;
            }
        }

        public DateTime StartAddDate
        {
            get
            {
                return this.startAddDate;
            }
            set
            {
                this.startAddDate = value;
            }
        }

        public int StorageAnalyse
        {
            get
            {
                return this.storageAnalyse;
            }
            set
            {
                this.storageAnalyse = value;
            }
        }

        public string Tags
        {
            get
            {
                return this.tags;
            }
            set
            {
                this.tags = value;
            }
        }
    }
}

