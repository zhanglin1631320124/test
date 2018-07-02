namespace SocoShop.Entity
{
    using System;

    public sealed class ProductInfo
    {
        private string accessory = string.Empty;
        private DateTime addDate = DateTime.Now;
        private int allowComment;
        private int attributeClassID;
        private int brandID;
        private string classID = string.Empty;
        private int collectCount;
        private string color = string.Empty;
        private int commentCount;
        private decimal currentMemberPrice;
        private string fontStyle = string.Empty;
        private int id;
        private int importActualStorageCount;
        private int importVirtualStorageCount;
        private string introduction = string.Empty;
        private int isHot;
        private int isNew;
        private int isSale;
        private int isSpecial;
        private int isTop;
        private string keywords = string.Empty;
        private int lowerCount;
        private decimal marketPrice;
        private string name = string.Empty;
        private int orderCount;
        private decimal perPoint;
        private string photo = string.Empty;
        private int photoCount;
        private string productNumber = string.Empty;
        private string relationArticle = string.Empty;
        private string relationProduct = string.Empty;
        private string remark = string.Empty;
        private int sendCount;
        private int sendPoint;
        private string spelling = string.Empty;
        private int standardType;
        private string summary = string.Empty;
        private int sumPoint;
        private long taobaoID;
        private int totalStorageCount;
        private int upperCount;
        private int viewCount;
        private int weight;

        public string Accessory
        {
            get
            {
                return this.accessory;
            }
            set
            {
                this.accessory = value;
            }
        }

        public DateTime AddDate
        {
            get
            {
                return this.addDate;
            }
            set
            {
                this.addDate = value;
            }
        }

        public int AllowComment
        {
            get
            {
                return this.allowComment;
            }
            set
            {
                this.allowComment = value;
            }
        }

        public int AttributeClassID
        {
            get
            {
                return this.attributeClassID;
            }
            set
            {
                this.attributeClassID = value;
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

        public int CollectCount
        {
            get
            {
                return this.collectCount;
            }
            set
            {
                this.collectCount = value;
            }
        }

        public string Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public int CommentCount
        {
            get
            {
                return this.commentCount;
            }
            set
            {
                this.commentCount = value;
            }
        }

        public decimal CurrentMemberPrice
        {
            get
            {
                return this.currentMemberPrice;
            }
            set
            {
                this.currentMemberPrice = value;
            }
        }

        public string FontStyle
        {
            get
            {
                return this.fontStyle;
            }
            set
            {
                this.fontStyle = value;
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

        public int ImportActualStorageCount
        {
            get
            {
                return this.importActualStorageCount;
            }
            set
            {
                this.importActualStorageCount = value;
            }
        }

        public int ImportVirtualStorageCount
        {
            get
            {
                return this.importVirtualStorageCount;
            }
            set
            {
                this.importVirtualStorageCount = value;
            }
        }

        public string Introduction
        {
            get
            {
                return this.introduction;
            }
            set
            {
                this.introduction = value;
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

        public int LowerCount
        {
            get
            {
                return this.lowerCount;
            }
            set
            {
                this.lowerCount = value;
            }
        }

        public decimal MarketPrice
        {
            get
            {
                return this.marketPrice;
            }
            set
            {
                this.marketPrice = value;
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

        public int OrderCount
        {
            get
            {
                return this.orderCount;
            }
            set
            {
                this.orderCount = value;
            }
        }

        public decimal PerPoint
        {
            get
            {
                return this.perPoint;
            }
            set
            {
                this.perPoint = value;
            }
        }

        public string Photo
        {
            get
            {
                return this.photo;
            }
            set
            {
                this.photo = value;
            }
        }

        public int PhotoCount
        {
            get
            {
                return this.photoCount;
            }
            set
            {
                this.photoCount = value;
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

        public string RelationArticle
        {
            get
            {
                return this.relationArticle;
            }
            set
            {
                this.relationArticle = value;
            }
        }

        public string RelationProduct
        {
            get
            {
                return this.relationProduct;
            }
            set
            {
                this.relationProduct = value;
            }
        }

        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                this.remark = value;
            }
        }

        public int SendCount
        {
            get
            {
                return this.sendCount;
            }
            set
            {
                this.sendCount = value;
            }
        }

        public int SendPoint
        {
            get
            {
                return this.sendPoint;
            }
            set
            {
                this.sendPoint = value;
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

        public string Summary
        {
            get
            {
                return this.summary;
            }
            set
            {
                this.summary = value;
            }
        }

        public int SumPoint
        {
            get
            {
                return this.sumPoint;
            }
            set
            {
                this.sumPoint = value;
            }
        }

        public long TaobaoID
        {
            get
            {
                return this.taobaoID;
            }
            set
            {
                this.taobaoID = value;
            }
        }

        public int TotalStorageCount
        {
            get
            {
                return this.totalStorageCount;
            }
            set
            {
                this.totalStorageCount = value;
            }
        }

        public int UpperCount
        {
            get
            {
                return this.upperCount;
            }
            set
            {
                this.upperCount = value;
            }
        }

        public int ViewCount
        {
            get
            {
                return this.viewCount;
            }
            set
            {
                this.viewCount = value;
            }
        }

        public int Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }
    }
}

